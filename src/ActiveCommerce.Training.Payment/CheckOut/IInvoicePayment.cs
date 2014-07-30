using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveCommerce.Training.Payment.CheckOut
{
    interface IInvoicePayment
    {
        string PurchaseOrderNumber { get; set; }
    }
}
