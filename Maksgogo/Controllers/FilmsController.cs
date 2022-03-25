using Maksgogo.Interfaces;
using Maksgogo.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Maksgogo.Controllers
{
    public class FilmsController : Controller
    {
        private readonly IGenres _allGenres;
        private readonly IStudios _allStudios;
        private readonly IFilms _allFilms;


        public FilmsController(IFilms allFilms, IGenres allGenres, IStudios allStudios)
        {
            _allFilms = allFilms;
            _allGenres = allGenres;
            _allStudios = allStudios;
        }


        public ViewResult List()
        {
            FilmListViewModel obj = new FilmListViewModel();
            obj.getAllFilms = _allFilms.AllFilms;
            //obj.CurrGenre = "супергеройський";
            //obj.CurrStudio = "Sony";
            return View(obj);
        }

        public ViewResult BestFilms()
        {
            BestFilmsViewModel obj = new BestFilmsViewModel();
            obj.BestFilms = from f in _allFilms.AllFilms
                            where f.IsFav == true
                            select f;
            return View(obj);
        }
    }
}
