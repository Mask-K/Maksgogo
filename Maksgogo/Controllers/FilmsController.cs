using Maksgogo.Interfaces;
using Maksgogo.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Maksgogo.Controllers
{
    public class FilmsController : Controller
    {
        
        private readonly IFilms _allFilms;


        public FilmsController(IFilms allFilms)
        {
            _allFilms = allFilms;
            
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
    }
}
