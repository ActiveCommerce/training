using System;
using System.Linq;
using ActiveCommerce.Data;
using ActiveCommerce.Data.Extensions;
using ActiveCommerce.Orders;
using NHibernate;
using NHibernate.Linq;

namespace ActiveCommerce.Training.OrderRepository
{
    public class OrderRepositoryExtended<T> : ActiveCommerce.Data.Orders.NHibernateOrderRepository<T> where T: Order
    {
        public OrderRepositoryExtended(ISessionBuilder sessionBuilder) : base(sessionBuilder)
        {
        }

        public void DeleteWhere(Func<Order,bool> where)
        {
            using (var session = SessionBuilder.OpenWriteSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var orders = session.Query<Order>().Where(where);
                        foreach (var order in orders)
                        {
                            session.Delete(order);
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.SafeRollback();
                        throw;
                    }
                }
            }
        }
    }
}