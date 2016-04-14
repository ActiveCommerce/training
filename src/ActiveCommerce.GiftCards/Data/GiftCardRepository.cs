using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.Data;
using NHibernate;
using NHibernate.Linq;
using Sitecore.Diagnostics;

namespace ActiveCommerce.GiftCards.Data
{
    public class GiftCardRepository : IGiftCardRepository
    {
        private readonly ISessionBuilder _sessionBuilder;
        private ISession _session;

        //session will be closed for us when Unity container is disposed on request end
        protected ISession Session
        {
            get
            {
                return _session == null || !_session.IsOpen ? (_session = _sessionBuilder.OpenWriteSession()) : _session;
            }
        }

        public GiftCardRepository(ISessionBuilder sessionBuilder)
        {
            Assert.ArgumentNotNull(sessionBuilder, "sessionBuilder");
            _sessionBuilder = sessionBuilder;
        }

        public virtual GiftCard Get(int id)
        {
            Assert.ArgumentNotNull(id, "id");
            return Session.Get<GiftCard>(id);
        }
        
        public virtual IQueryable<GiftCard> GetAll()
        {
            return Session.Query<GiftCard>();
        }

        public virtual void Add(GiftCard giftCard)
        {
            Assert.ArgumentNotNull(giftCard, "giftCard");
            Session.SaveOrUpdate(giftCard);
        }

        public virtual void Delete(GiftCard giftCard)
        {
            Assert.ArgumentNotNull(giftCard, "giftCard");
            Session.Delete(giftCard);
        }

        public virtual void Flush()
        {
            Session.Flush();
        }
    }
}