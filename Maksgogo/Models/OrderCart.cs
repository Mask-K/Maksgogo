using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;

namespace Maksgogo
{
    public partial class OrderCart
    {
        private readonly MaksgogoContext _context;
        private readonly IHttpContextAccessor httpContextAccessor;
        public OrderCart(MaksgogoContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public string Session { get; set; }
        public List<CartItem> listCartItems { get; set; }

        public static OrderCart GetCart(IServiceProvider services)
        {
            ISession _session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<MaksgogoContext>();
            var httpContext = services.GetService<IHttpContextAccessor>();
            string __session = _session.GetString("CartId")?? Guid.NewGuid().ToString();

            _session.SetString("CartId", __session);
            return new OrderCart(context, httpContext) { Session = __session };
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

        public void Clear()
        {
            var items = new List<CartItem>();

            items = _context.CartItems.Where(c => c.Session == Session).ToList();

            foreach (var i in items)
            {
                _context.CartItems.Remove(i);
            }
            _context.SaveChanges();
            this.listCartItems.Clear();
        }

        public void DeleteItem(string name)
        {
            var film = _context.Films.FirstOrDefault(i => i.Name == name).IdFilm;

            var temp = _context.CartItems.FirstOrDefault(i => i.IdFilm == film && i.Session == Session);
            _context.CartItems.Remove(temp);
            _context.SaveChanges();

        }

    }
}
