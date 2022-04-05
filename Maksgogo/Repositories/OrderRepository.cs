using Maksgogo.Interfaces;
using Maksgogo.Models;

namespace Maksgogo.Repositories
{
    public class OrderRepository : IOrders
    {
        private readonly MaksgogoContext _context;

        private readonly OrderCart _orderCart;

        public OrderRepository(MaksgogoContext context, OrderCart orderCart)
        {
            _context = context;
            _orderCart = orderCart;
        }
        public void CreateOrder(Order order)
        {
            order.OrderTime = DateTime.Now;
            _context.Orders.Add(order);
            _context.SaveChanges();

            var films = _orderCart.listCartItems;
            Order realOrder = _context.Orders.OrderBy(x=>x.IdOrder).Last(x => x.IdOrder > 0);  //_context.Orders.FirstOrDefault(x => x.OrderTime == order.OrderTime);
            foreach (var i in films)
            {
                var info = new OrderInfo()
                {
                    IdFilm = (int)i.IdFilm,
                    IdOrder = realOrder.IdOrder
                };
                _context.OrderInfos.Add(info);
                _context.SaveChanges();

            }
            _orderCart.Clear();
        }

        
    }
}
