using Microsoft.AspNetCore.Mvc;

namespace Maksgogo.Controllers
{
    public class GenresController : Controller
    {
        private readonly MaksgogoContext _context;

        public GenresController(MaksgogoContext maksgogo)
        {
            _context = maksgogo;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
