using CodeChallenge_SV.Constants;
using CodeChallenge_SV.Data;
using CodeChallenge_SV.DataAccessLayerInteraces;
using CodeChallenge_SV.Models;
using Newtonsoft.Json;

namespace CodeChallenge_SV.DataAccessLayer
{
    public class SearchDal : ISearchDal
    {
        public async Task<List<Building>> GetBuildingsBySearchInput(string searchInput)
        {
            var data = JsonConvert.DeserializeObject<SearchDataHolder>(File.ReadAllText(FilePath.Path));
            var buildings = new List<Building>();
            if(data != null && data.Buildings != null)
            {
                buildings = data.Buildings.Where(b => b.Name.Contains(searchInput) || b.ShortCut.Contains(searchInput) || b.Description.Contains(searchInput)).ToList();
            }
            return buildings;
        }

        public async Task<List<Lock>> GetLocksBySearchInput(string searchInput)
        {
            var data = JsonConvert.DeserializeObject<SearchDataHolder>(File.ReadAllText(FilePath.Path));
            var locks = new List<Lock>();
            if (data != null && data.Locks != null)
            {
                locks = data.Locks.Where(l => l.Name.Contains(searchInput) || l.SerialNumber.Contains(searchInput) || l.Floor != null && l.Floor.Contains(searchInput) || l.RoomNumber.Contains(searchInput) || l.Description != null && l.Description.Contains(searchInput) || l.Type.Contains(searchInput)).ToList();
            }
            return locks;
        }

        public async Task<List<Group>> GetGroupsBySearchInput(string searchInput)
        {
            var data = JsonConvert.DeserializeObject<SearchDataHolder>(File.ReadAllText(FilePath.Path));
            var groups = new List<Group>();
            if (data != null && data.Groups != null)
            {
                groups = data.Groups.Where(g => g.Name.Contains(searchInput) || g.Description != null && g.Description.Contains(searchInput)).ToList();
            }
            return groups;
        }

        public async Task<List<Medium>> GetMediaBySearchInput(string searchInput)
        {
            var data = JsonConvert.DeserializeObject<SearchDataHolder>(File.ReadAllText(FilePath.Path));
            var media = new List<Medium>();
            if (data != null && data.Media != null)
            {
                media = data.Media.Where(m => m.Owner.Contains(searchInput) || m.SerialNumber.Contains(searchInput) || m.Description != null && m.Description.Contains(searchInput) || m.Type.Contains(searchInput)).ToList();
            }
            return media;
        }
    }
}
