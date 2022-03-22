namespace Maksgogo.Interfaces
{
    public interface IGenres
    {
        IEnumerable<Genre> AllGenres { get; }
        Genre getGenre(int idGenre);
    }
}
