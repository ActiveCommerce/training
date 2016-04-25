using System;
using System.Collections.Generic;
using System.Linq;
using ActiveCommerce.GiftCards.Data;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.GiftCards.sitecore.admin
{
    public partial class giftcardutility : Sitecore.sitecore.admin.AdminPage
    {
        public IGiftCardRepository GiftCardRepository
        {
            get
            {
                return Sitecore.Ecommerce.Context.Entity.Resolve<IGiftCardRepository>();
            }
        }

        protected virtual IList<GiftCard> GiftCards
        {
            get
            {
                return new List<GiftCard>
                {
                    new GiftCard
                    {
                        Number = "NOPIN25",
                        Balance = 25m
                    },
                    new GiftCard
                    {
                        Number = "NOPIN50",
                        Balance = 50m
                    },
                    new GiftCard
                    {
                        Number = "PIN100",
                        Pin = "100",
                        Balance = 100m
                    },
                    new GiftCard
                    {
                        Number = "PIN500",
                        Pin = "500",
                        Balance = 500m
                    }
                };
            }
        }

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            base.CheckSecurity(true);
            BindResults();
        }

        protected virtual void BindResults()
        {
            var repo = GiftCardRepository;
            lvResults.DataSource = repo.GetAll().ToList();
            lvResults.DataBind();
        }

        protected virtual void btnGenerate_Click(object sender, EventArgs e)
        {
            var repo = GiftCardRepository;
            foreach (var card in GiftCards)
            {
                repo.Add(card);
            }
            repo.Flush();
            BindResults();
        }

        protected virtual void btnDelete_Click(object sender, EventArgs e)
        {
            var repo = GiftCardRepository;
            var cards = GiftCardRepository.GetAll();
            foreach (var card in cards)
            {
                repo.Delete(card);
            }
            repo.Flush();
            BindResults();
        }
    }
}