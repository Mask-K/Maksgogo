using Maksgogo.Interfaces;
using Maksgogo.Models;
using Maksgogo.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Maksgogo.Controllers
{
    public class OrderCartController : Controller
    {
        private readonly IFilms _filmRep;
        private readonly OrderCart _orderCart;
        private readonly MaksgogoContext _context;
        private readonly UserManager<User> _userManager;


        public OrderCartController(IFilms filmRep, OrderCart orderCart, MaksgogoContext context, UserManager<User> userManager)
        {
            _filmRep = filmRep;
            _orderCart = orderCart;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var items = _orderCart.GetItems();
            _orderCart.listCartItems = items;

            var obj = new OrderCartViewModel
            {
                OrderCart = this._orderCart,
            };
            if(_orderCart.listCartItems.Count == 0)
            {
                return RedirectToAction("Error", "Order", new RouteValueDictionary(new
                {
                    action = "ErrorOrder",
                    controller = "Order",
                    error = "Пусто..."
                }));
            }
            return View(obj);
        }

        public async Task<RedirectToActionResult> AddToCartAsync(int filmId)
        {
            //var filmIds 
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Error", "Order", new RouteValueDictionary(new
                {
                    action = "ErrorOrder",
                    controller = "Order",
                    error = "Увійдіть спочатку в акаунт!"
                }));
            }
            var curr_user = await _userManager.GetUserAsync(User);
            var userFilms = from f in _context.User_has_films
                            where f.Username == curr_user.UserName
                            select f.IdFilm;
            if (userFilms.Contains(filmId))
            {
                return RedirectToAction("Error", "Order", new RouteValueDictionary(new
                {
                    action = "ErrorOrder",
                    controller = "Order",
                    error = "Ви вже купили цей фільм!"
                }));
            }
            var item = _filmRep.AllFilms.FirstOrDefault(i => i.IdFilm == filmId);
            if (item != null && (_orderCart.GetItems().Count() == 0 || _orderCart.GetItems().Where(x => x.IdFilm == filmId).Count() == 0))
                _orderCart.AddToCart(item);
            

            return RedirectToAction("Index");
        }

    }
}
