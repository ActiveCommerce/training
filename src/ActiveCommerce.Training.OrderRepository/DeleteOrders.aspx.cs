using System;
using System.Linq;
using ActiveCommerce.Orders;
using ActiveCommerce.Orders.Management;
using Microsoft.Practices.Unity;

namespace ActiveCommerce.Training.OrderRepository
{
    public partial class DeleteOrders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["confirm"] != "y")
            {
                return;

            }
            var orderRepository = Sitecore.Ecommerce.Context.Entity.Resolve<IOrderRepository<Orders.Order>>() as OrderRepositoryExtended<Order>;
            orderRepository.DeleteWhere(x => x.IssueDate <= DateTime.Parse("06/08/2015"));
        }
    }
}