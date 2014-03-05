using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace ActiveCommerce.GiftMessage.Orders
{
    [SitecoreType]
    public class OrderMappingRule : ActiveCommerce.Data.OrderMappingRule
    {
        protected new Order MyMappingObject
        {
            get { return base.MappingObject as Order; }
        }

        [SitecoreField(FieldName = "Gift Message")]
        public string ExternalOrderId
        {
            get
            {
                return MyMappingObject.GiftMessage;
            }
            set
            {
                MyMappingObject.GiftMessage = value;
            }
        }
    }
}