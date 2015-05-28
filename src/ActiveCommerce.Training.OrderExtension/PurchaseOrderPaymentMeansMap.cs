using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace ActiveCommerce.Training.OrderExtension
{
    public class PurchaseOrderPaymentMeansMap : SubclassMap<PurchaseOrderPaymentMeans>
    {
        public PurchaseOrderPaymentMeansMap()
        {
            Map(x => x.PurchaseOrderNumber);
        }
    }
}