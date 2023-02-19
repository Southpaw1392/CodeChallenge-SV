using CodeChallenge_SV.Constants;
using CodeChallenge_SV.Data;
using CodeChallenge_SV.DataAccessLayerInteraces;
using CodeChallenge_SV.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CodeChallenge_SV.DataAccessLayer
{
    public class SearchDal : ISearchDal
    {
        private readonly SearchContext _context;

        public SearchDal(SearchContext context)
        {
            _context = context;
        }

        public async Task<List<Building>> GetBuildingsBySearchInput(string[] searchWords)
        {
            IEnumerable<Building> buildings = _context.Buildings
                .AsEnumerable()
                .Where(b => searchWords.Any(w => b.Name.Contains(w))
                         || searchWords.Any(w => b.ShortCut.Contains(w))
                         || searchWords.Any(w => b.Description.Contains(w)));

            return buildings.ToList();
        }

        public async Task<List<Lock>> GetLocksBySearchInput(string[] searchWords)
        {
            IEnumerable<Lock> locks = _context.Locks
                .Include(l => l.Building)
                .AsEnumerable()
                .Where(l => searchWords.Any(w => l.Name.Contains(w))
                         || searchWords.Any(w => l.SerialNumber.Contains(w))
                         || l.Floor != null && searchWords.Any(w => l.Floor.Contains(w))
                         || searchWords.Any(w => l.RoomNumber.Contains(w))
                         || l.Description != null && searchWords.Any(w => l.Description.Contains(w))
                         || searchWords.Any(w => l.Type.Contains(w)));

            return locks.ToList();
        }

        public async Task<List<Group>> GetGroupsBySearchInput(string[] searchWords)
        {
            IEnumerable<Group> groups = _context.Groups
                .AsEnumerable()
                .Where(g => searchWords.Any(w => g.Name.Contains(w))
                         || (g.Description != null && searchWords.Any(w => g.Description.Contains(w))));

            return groups.ToList();
        }

        public async Task<List<Medium>> GetMediaBySearchInput(string[] searchWords)
        {
            IEnumerable<Medium> media = _context.Media
                .Include(m => m.Group)
                .AsEnumerable()
                .Where(m => searchWords.Any(w => m.Owner.Contains(w))
                         || searchWords.Any(w => m.SerialNumber.Contains(w))
                         || (m.Description != null && searchWords.Any(w => m.Description.Contains(w)))
                         || searchWords.Any(w => m.Type.Contains(w)));

            return media.ToList();
        }
    }
}
