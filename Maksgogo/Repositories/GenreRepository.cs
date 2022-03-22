using Maksgogo.Interfaces;

namespace Maksgogo.Repositories
{
    public class GenreRepository : IGenres
    {
        private readonly MaksgogoContext _context;

        public GenreRepository(MaksgogoContext context)
        {
            this._context = context;
        }

        public IEnumerable<Genre> AllGenres => _context.Genres;

        public Genre getGenre(int idGenre)
        {
            return _context.Genres.First(el => el.IdGenre == idGenre);
        }
    }
}
