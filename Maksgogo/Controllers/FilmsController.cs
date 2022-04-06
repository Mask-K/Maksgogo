using Maksgogo.Interfaces;
using Maksgogo.Models;
using Maksgogo.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Maksgogo.Controllers
{
    public class FilmsController : Controller
    {
        
        private readonly IFilms _allFilms;
        private readonly MaksgogoContext _context;
        private readonly UserManager<User> _userManager;

        public FilmsController(IFilms allFilms, MaksgogoContext context, UserManager<User> userManager)
        {
            _allFilms = allFilms;
            _context = context;
            _userManager = userManager;
        }

        [Route("Films/List")]
       
        public IActionResult List()
        {
            FilmListViewModel obj = new FilmListViewModel();
            obj.getAllFilms = _allFilms.AllFilms;
            
            return View(obj);
        }
        public IActionResult Search()
        {
            FilmListViewModel obj = new FilmListViewModel();
            obj.getAllFilms = _allFilms.AllFilms;
            
            return View(obj);
        }


        public PartialViewResult SearchFilms(string searchtext, string id)
        {
            var obj = new FilmListViewModel();
            try
            {
                if (int.Parse(id) == 1)
                {
                    obj.getAllFilms = from f in _allFilms.AllFilms
                                      where f.Name.ToLower().Contains(searchtext?.ToLower())
                                      select f;
                }
                else if (int.Parse(id) == 2)
                {
                    var context = new MaksgogoContext();
                    obj.getAllFilms = from f in _allFilms.AllFilms
                                      join c in context.Genres on f.IdGenre equals c.IdGenre
                                      where c.GenreName.ToLower().Contains(searchtext?.ToLower())
                                      select f;
                }
                else
                {
                    var context = new MaksgogoContext();
                    obj.getAllFilms = from f in _allFilms.AllFilms
                                      join c in context.Studios on f.IdStudio equals c.IdStudio
                                      where c.StudioName.ToLower().Contains(searchtext?.ToLower())
                                      select f;
                }
            }
            catch (Exception ex) { }
            return PartialView("_GridView", obj);
            
        }

        public IActionResult BestFilms()
        {
            var obj = new FilmListViewModel();
            obj.getAllFilms = from f in _allFilms.AllFilms
                            where f.IsFav == true
                            select f;
            return View(obj);
        }

        public async Task<IActionResult> Profile()
        {
            var obj = new FilmListViewModel();
            var curr_user = await _userManager.GetUserAsync(User);
            var userFilms = from f in _context.User_has_films
                            where f.Username == curr_user.UserName
                            select f.IdFilm;


            obj.getAllFilms = from f in _allFilms.AllFilms
                              where userFilms.Contains(f.IdFilm)
                              select f;
            return View(obj);
        }
    }
}
