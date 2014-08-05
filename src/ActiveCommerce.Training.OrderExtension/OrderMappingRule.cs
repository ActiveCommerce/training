using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace ActiveCommerce.Training.OrderExtension
{
    [SitecoreType]
    public class OrderMappingRule : ActiveCommerce.Data.OrderMappingRule
    {
        protected Order TrainingMappingObject
        {
            get { return base.MappingObject as Order; }
        }

        [SitecoreField(FieldName = "External Order Id")]
        public string ExternalOrderId
        {
            get
            {
                return TrainingMappingObject.ExternalOrderId.ToString();
            }
            set
            {
                var guid = default(Guid);
                Guid.TryParse(value, out guid);
                TrainingMappingObject.ExternalOrderId = guid;
            }
        }

        [SitecoreField(FieldName = "Purchase Order Number")]
        public string PurchaseOrderNumber
        {
            get
            {
                return TrainingMappingObject.PurchaseOrderNumber;
            }
            set
            {
                TrainingMappingObject.PurchaseOrderNumber = value;
            }
        }
    }
}