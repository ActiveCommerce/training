using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Sitecore.Data;
using Sitecore.Data.Events;
using Sitecore.Diagnostics;

namespace ActiveCommerce.Training.ProductDataProvider
{
    public class MongoOplogCacheClearer
    {
        private const string LOCAL = "local";
        private const string OPLOG = "oplog.$main"; //could be different depending on replica configuration

        private readonly IList<Database> _databases;
        private readonly string _connectionString;
        private readonly string _mongoDatabase;
        private readonly string _mongoCollection;

        public MongoOplogCacheClearer(string connectionString, string mongoDatabase, string mongoCollection)
        {
            _connectionString = connectionString;
            _mongoDatabase = mongoDatabase;
            _mongoCollection = mongoCollection;
            _databases = new List<Database>();
        }

        public void AddDatabase(Database database)
        {
            Assert.ArgumentNotNull(database, "database");
            Sitecore.Diagnostics.Log.Info(string.Format("MongoOplogCacheClearer: Monitoring {0}", database.Name), this);
            _databases.Add(database);
        }

        public void Start()
        {
            try
            {
                Sitecore.Diagnostics.Log.Info("MongoOplogCacheClearer: Starting", this);
                var client = new MongoClient(_connectionString);
                var server = client.GetServer();
                var mongoDatabase = server.GetDatabase(LOCAL);
                var opLog = mongoDatabase.GetCollection(OPLOG);
                var queryCollection = string.Format("{0}.{1}", _mongoDatabase, _mongoCollection);
                var queryDoc = new QueryDocument("ns", queryCollection);
                var query = opLog.Find(queryDoc)
                                .SetFlags(QueryFlags.AwaitData | QueryFlags.NoCursorTimeout | QueryFlags.TailableCursor);
                var cursor = new MongoCursorEnumerator<BsonDocument>(query);
                while (true)
                {
                    if (cursor.MoveNext())
                    {
                        var document = cursor.Current;
                        if (document["op"].AsString == "d")
                        {
                            //need to find the update that set the sitecore id
                            Sitecore.Diagnostics.Log.Info("MongoOplogCacheClearer: Product deleted, seeking Sitecore id", this);
                            var deleted = document["o"].AsBsonDocument;
                            var findDeleted = Query.And(new[]
                            {
                                Query.EQ("op", "u"),
                                Query.EQ("ns", queryCollection),
                                Query.EQ("o2._id", deleted["_id"].AsObjectId),
                                Query.Exists("o.$set.sitecoreId")
                            });

                            var deletedResults = opLog.Find(findDeleted).SetSortOrder(SortBy.Descending("ts"));
                            if (!deletedResults.Any())
                            {
                                continue;
                            }
                            document = deletedResults.First();
                            document = document["o"].AsBsonDocument;
                            document = document["$set"].AsBsonDocument;
                        }
                        else
                        {
                            document = document["o"].AsBsonDocument;
                        }

                        if (!document.Contains("sitecoreId"))
                        {
                            continue;
                        }
                        var id = Sitecore.Data.ShortID.Parse(document["sitecoreId"].AsString).ToID();
                        foreach (var database in _databases)
                        {
                            var item = database.GetItem(id);
                            if (item == null)
                            {
                                continue;
                            }
                            Sitecore.Diagnostics.Log.Info(
                                string.Format("MongoOplogCacheClearer: Product {0}://{1} updated", database.Name, id), this);
                            database.Caches.ItemCache.RemoveItem(id);
                            database.Caches.DataCache.RemoveItemInformation(id);
                            database.Engines.DataEngine.RaiseSavedItem(item, true);
                            var args = new ItemSavedEventArgs(item);
                            Sitecore.Events.Event.RaiseItemSaved(this, args);
                            //TODO: Something to cause reindex in web, new indexing strategy?
                            //TODO: Clear HTML cache?
                        }
                    }
                    else if (cursor.IsDead)
                    {
                        Sitecore.Diagnostics.Log.Info("MongoOplogCacheClearer: Dead", this);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Sitecore.Diagnostics.Log.Error("Exception starting MongoDb oplog monitoring", e, this);
            }
        }
    }
}