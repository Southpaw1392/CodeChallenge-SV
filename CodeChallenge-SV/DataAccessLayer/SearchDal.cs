using CodeChallenge_SV.Constants;
using CodeChallenge_SV.Data;
using CodeChallenge_SV.DataAccessLayerInteraces;
using CodeChallenge_SV.Models;
using Newtonsoft.Json;

namespace CodeChallenge_SV.DataAccessLayer
{
    public class SearchDal : ISearchDal
    {
        public async Task<List<Building>> GetBuildingsBySearchInput(string[] searchWords)
        {
            var data = JsonConvert.DeserializeObject<SearchDataHolder>(File.ReadAllText(FilePath.Path));
            var buildings = new List<Building>();
            if(data != null && data.Buildings != null)
            {
                buildings = data.Buildings.Where(b => searchWords.Any( w => b.Name.Contains(w)) || searchWords.Any(w => b.ShortCut.Contains(w)) || searchWords.Any(w => b.Description.Contains(w))).ToList();
            }
            return buildings;
        }

        public async Task<List<Lock>> GetLocksBySearchInput(string[] searchWords)
        {
            var data = JsonConvert.DeserializeObject<SearchDataHolder>(File.ReadAllText(FilePath.Path));
            var locks = new List<Lock>();
            if (data != null && data.Locks != null)
            {
                locks = data.Locks.Where(l => searchWords.Any(w => l.Name.Contains(w)) || searchWords.Any(w => l.SerialNumber.Contains(w)) || l.Floor != null && searchWords.Any(w => l.Floor.Contains(w)) || searchWords.Any(w => l.RoomNumber.Contains(w)) || l.Description != null && searchWords.Any(w => l.Description.Contains(w)) || searchWords.Any(w => l.Type.Contains(w))).ToList();
            }
            return locks;
        }

        public async Task<List<Group>> GetGroupsBySearchInput(string[] searchWords)
        {
            var data = JsonConvert.DeserializeObject<SearchDataHolder>(File.ReadAllText(FilePath.Path));
            var groups = new List<Group>();
            if (data != null && data.Groups != null)
            {
                groups = data.Groups.Where(g => searchWords.Any(w => g.Name.Contains(w)) || g.Description != null && searchWords.Any(w => g.Description.Contains(w))).ToList();
            }
            return groups;
        }

        public async Task<List<Medium>> GetMediaBySearchInput(string[] searchWords)
        {
            var data = JsonConvert.DeserializeObject<SearchDataHolder>(File.ReadAllText(FilePath.Path));
            var media = new List<Medium>();
            if (data != null && data.Media != null)
            {
                media = data.Media.Where(m => searchWords.Any(w => m.Owner.Contains(w)) || searchWords.Any(w => m.SerialNumber.Contains(w)) || m.Description != null && searchWords.Any(w => m.Description.Contains(w)) || searchWords.Any(w => m.Type.Contains(w))).ToList();
            }
            return media;
        }
    }
}
