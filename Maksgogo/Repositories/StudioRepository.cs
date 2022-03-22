using Maksgogo.Interfaces;

namespace Maksgogo.Repositories
{
    public class StudioRepository : IStudios
    {
        private readonly MaksgogoContext _context;

        public  StudioRepository(MaksgogoContext context)
        {
            _context = context;
        }

        public IEnumerable<Studio> AllStudios => _context.Studios;

        public Studio getStudio(int idStudio)
        {
            return _context.Studios.First(s => s.IdStudio == idStudio);
        }
    }
}
