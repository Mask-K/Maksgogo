using Maksgogo.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Maksgogo.Repositories
{
    public class FilmRepository : IFilms
    {
        private readonly MaksgogoContext _context;

        public FilmRepository(MaksgogoContext context)
        {
            _context = context;
        }

        public IEnumerable<Film> AllFilms => _context.Films;

        public IEnumerable<Film> getBestFilms => _context.Films.Where(f => (bool)f.IsFav);//.Include(f => f.IdGenre) мб треба інклуди
                                                                                         //.Include(f => f.IdStudio);

        public Film getFilm(int idFilm)
        {
            return _context.Films.FirstOrDefault(f => f.IdFilm == idFilm);
        }
    }
}
