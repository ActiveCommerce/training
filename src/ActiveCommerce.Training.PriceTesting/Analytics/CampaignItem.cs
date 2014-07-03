using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace ActiveCommerce.Training.PriceTesting.Analytics
{
    public class CampaignItem : Sitecore.Analytics.Data.Items.CampaignItem
    {
        public string GoogleCampaign
        {
            get
            {
                return InnerItem[FieldIDs.GoogleCampaign];
            }
            set
            {
                InnerItem[FieldIDs.GoogleCampaign] = value;
            }
        }

        public string GoogleSource
        {
            get
            {
                return InnerItem[FieldIDs.GoogleSource];
            }
            set
            {
                InnerItem[FieldIDs.GoogleSource] = value;
            }
        }

        public string GoogleMedium
        {
            get
            {
                return InnerItem[FieldIDs.GoogleMedium];
            }
            set
            {
                InnerItem[FieldIDs.GoogleMedium] = value;
            }
        }

        public CampaignItem(Item item) : base(item)
        {
        }

        public static explicit operator CampaignItem(Item item)
        {
            if (item == null)
            {
                return null;
            }
            return new CampaignItem(item);
        }

        public static implicit operator Item(CampaignItem item)
        {
            if (item == null)
            {
                return null;
            }
            return item.InnerItem;
        }

        public static new class FieldIDs
        {
            public static readonly ID GoogleCampaign;
            public static readonly ID GoogleSource;
            public static readonly ID GoogleMedium;

            static FieldIDs()
            {
                GoogleCampaign = new ID("{2523D555-96B2-470C-8215-5260DE1D377B}");
                GoogleSource = new ID("{E4D40336-C370-41CF-A94F-31A233518859}");
                GoogleMedium = new ID("{F4F38E46-8B8A-4C5C-85DF-73F78633C0E6}");
            }
        }
    }
}