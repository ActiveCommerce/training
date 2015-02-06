using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ActiveCommerce.Data;
using NHibernate;
using NHibernate.Linq;
using Sitecore.Diagnostics;

namespace ActiveCommerce.Training.NHibernate
{
    public class ProductReviewRepository : IProductReviewRepository
    {
        private readonly ISessionBuilder _sessionBuilder;
        private ISession _session;
        private readonly Sitecore.Ecommerce.ShopContext _shopContext;

        //session will be closed for us when Unity container is disposed on request end
        protected ISession Session
        {
            get { return _session ?? (_session = _sessionBuilder.OpenWriteSession()); }
        }

        //session builder and shop context will be injected by Unity container
        public ProductReviewRepository(ISessionBuilder sessionBuilder, Sitecore.Ecommerce.ShopContext shopContext)
        {
            Assert.ArgumentNotNull(sessionBuilder, "sessionBuilder");
            Assert.ArgumentNotNull(shopContext, "shopContext");
            _sessionBuilder = sessionBuilder;
            _shopContext = shopContext;
        }

        //retrieving by primary key should always use Session.Get<>
        public ProductReview GetById(int reviewId)
        {
            return Session.Get<ProductReview>(reviewId);
        }

        //queries can be done via LINQ with Session.Query<>
        public IQueryable<ProductReview> GetAll()
        {
            return Session.Query<ProductReview>().Where(review => review.ShopContext == _shopContext.InnerSite.Name);
        }

        public IQueryable<ProductReview> GetByProduct(string productCode)
        {
            Assert.ArgumentNotNullOrEmpty(productCode, "productCode");
            return GetAll().Where(review => review.ProductCode == productCode);
        }

        //must be called for newly constructed objects not queried from NHibernate
        public void Add(ProductReview review)
        {
            Assert.ArgumentNotNull(review, "review");
            review.ShopContext = _shopContext.InnerSite.Name;
            Session.SaveOrUpdate(review);
        }

        public void Delete(ProductReview review)
        {
            Session.Delete(review);
        }

        //persists any changes to the database
        public void Flush()
        {
            Session.Flush();
        }

    }
}