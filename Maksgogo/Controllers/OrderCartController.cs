using Maksgogo.Interfaces;
using Maksgogo.Repositories;
using Maksgogo.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Maksgogo.Controllers
{
    public class OrderCartController : Controller
    {
        private readonly IFilms _filmRep;
        private readonly OrderCart _orderCart;

        public OrderCartController(IFilms filmRep, OrderCart orderCart)
        {
            _filmRep = filmRep;
            _orderCart = orderCart;
        }

        public ViewResult Index()
        {
            var items = _orderCart.GetItems();
            _orderCart.listCartItems = items;

            var obj = new OrderCartViewModel
            {
                OrderCart = this._orderCart,
            };

            

            return View(obj);
        }

        public RedirectToActionResult AddToCart(int filmId)
        {
            var item = _filmRep.AllFilms.FirstOrDefault(i => i.IdFilm == filmId);
            if (item != null)
            {
                _orderCart.AddToCart(item);
            }
            return RedirectToAction("Index");
        }

    }
}
