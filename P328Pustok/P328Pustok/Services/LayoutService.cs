using P328Pustok.DAL;
using P328Pustok.Models;

namespace P328Pustok.Services
{
    public class LayoutService
    {
        private readonly PustokContext _context;

        public LayoutService(PustokContext context)
        {
            _context = context;
        }
        public Dictionary<string,string> GetSettings()
        {
            return _context.Settings.ToDictionary(x=>x.Key,x=>x.Value);
        }

        public List<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }
    }
}
