namespace Maksgogo.Interfaces
{
    public interface IFilms
    {
        IEnumerable<Film> AllFilms { get; }
        IEnumerable<Film> getBestFilms { get; }
        Film getFilm(int idFilm);
    }
}
