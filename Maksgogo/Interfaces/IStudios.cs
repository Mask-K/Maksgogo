namespace Maksgogo.Interfaces
{
    public interface IStudios
    {
        IEnumerable<Studio> AllStudios { get; }
        Studio getStudio(int idStudio);
    }
}
