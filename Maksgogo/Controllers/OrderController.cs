using Maksgogo.Interfaces;
using Maksgogo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Maksgogo.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrders _orders;
        private readonly OrderCart _orderCart;

        public OrderController(IOrders orders, OrderCart orderCart)
        {
            _orders = orders;
            _orderCart = orderCart;
        }

        public IActionResult CheckOut()
        {
            _orderCart.listCartItems = _orderCart.GetItems();
            return View();
        }

        [HttpPost]
        public IActionResult CheckOut(Order order)
        {
            _orderCart.listCartItems = _orderCart.GetItems();
            if(_orderCart.listCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Немає товарів");
            }
            if (ModelState.IsValid)
            {
                _orders.CreateOrder(order);
                return RedirectToAction("Complete");
            }
            return View(order);
        }

        public IActionResult Complete()
        {
            ViewBag.Mess = "Фільми успішно куплено! Гарного перегляду!";
            return View();
        }
    }
}
