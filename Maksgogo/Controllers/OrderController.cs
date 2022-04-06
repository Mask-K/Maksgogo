using Maksgogo.Interfaces;
using Maksgogo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Maksgogo.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrders _orders;
        private readonly OrderCart _orderCart;
        private readonly UserManager<User> _userManager;

        public OrderController(IOrders orders, OrderCart orderCart, UserManager<User> userManager)
        {
            _orders = orders;
            _orderCart = orderCart;
            _userManager = userManager;
        }

        public async Task<IActionResult> CheckOut()
        {
            _orderCart.listCartItems = _orderCart.GetItems();
            Order order = new Order();
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var curr_user = await _userManager.GetUserAsync(User);
                    order.Name = curr_user.UserName;
                    order.Email = curr_user.Email;
                    _orders.CreateOrder(order);
                    return RedirectToAction("Complete");
                }
                return RedirectToAction("Error", "Order", new RouteValueDictionary(new
                {
                    action = "ErrorOrder",
                    controller = "Order",
                    error = "Увійдіть в акаунт для покупки!"
                }));

            }
            return View(order);
        }

        /*[HttpPost]
        public IActionResult CheckOut(Order order)
        {
            _orderCart.listCartItems = _orderCart.GetItems();
            
            
        }*/

        public IActionResult Complete()
        {
            ViewBag.Mess = "Фільми успішно куплено! Гарного перегляду!";
            return View();
        }

        public IActionResult Error(string error)
        {
            ViewBag.Message = error;
            return View();
        }
    }
}
