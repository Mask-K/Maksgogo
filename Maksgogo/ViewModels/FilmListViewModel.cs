using System.Linq;

namespace Maksgogo.ViewModels
{
    public class FilmListViewModel
    {
        public IEnumerable<Film> getAllFilms { get; set; }
        public string CurrGenre { get; set; }
        public string CurrStudio { get; set; }


    }
}
