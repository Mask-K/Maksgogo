using Maksgogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Maksgogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly MaksgogoContext _context;


        public ChartController(MaksgogoContext maksgogo)
        {
            _context = maksgogo;
        }

        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var genres = _context.Genres.ToList();
            List<object> genFilms = new List<object>();
            genFilms.Add(new[] { "Жанр", "Кількість фільмів" });
            foreach(var c in genres)
            {
                int am = _context.Films.Count(x => x.IdGenre == c.IdGenre);
                genFilms.Add(new object[] {c.GenreName, am});
            }

            return new JsonResult(genFilms);
        }

        [HttpGet("Films")]

        public JsonResult Films()
        {
            List<object> filmsBuy = new List<object>();
            filmsBuy.Add(new[] { "Фільм", "Кількість покупок" });
            foreach (var u in _context.Films)
                filmsBuy.Add(new object[] {u.Name, u.AmountBougth});

            return new JsonResult(filmsBuy);

        }
    }
}
