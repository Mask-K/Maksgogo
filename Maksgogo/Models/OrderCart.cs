using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Maksgogo
{
    public partial class OrderCart
    {
        private readonly MaksgogoContext _context;

        public OrderCart(MaksgogoContext context)
        {
            _context = context;
        }

        public string Session { get; set; }
        public List<CartItem> listCartItems { get; set; }

        public static OrderCart GetCart(IServiceProvider services)
        {
            ISession _session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<MaksgogoContext>();
            string __session = _session.GetString("CartId")?? Guid.NewGuid().ToString();

            _session.SetString("CartId", __session);
            return new OrderCart(context) { Session = __session };
        }

        public void AddToCart(Film film)
        {
            _context.Add(new CartItem
            {
                IdFilm = film.IdFilm,
                Session = this.Session,
            });
            _context.SaveChanges();
        }

        public List<CartItem> GetItems()
        {
            return _context.CartItems.Where(c => c.Session == this.Session).ToList();
        }

    }
}
