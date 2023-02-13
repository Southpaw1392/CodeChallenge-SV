using CodeChallenge_SV.DataAccessLayer;
using CodeChallenge_SV.DataAccessLayerInteraces;
using CodeChallenge_SV.Dtos;
using CodeChallenge_SV.Models;

namespace CodeChallenge_SV.BusinessLogicLayer
{
    public class SearchBll
    {
        private readonly ISearchDal _dataAccessLayer;

        public SearchBll(ISearchDal dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }

        public async Task<List<SearchResult>> Search(string searchInput)
        {
            var buildings = await _dataAccessLayer.GetBuildingsBySearchInput(searchInput);
            var groups = await _dataAccessLayer.GetGroupsBySearchInput(searchInput);
            var locks = await _dataAccessLayer.GetLocksBySearchInput(searchInput);
            var media = await _dataAccessLayer.GetMediaBySearchInput(searchInput);

            return await FilterAndSortData(searchInput, buildings, groups, locks, media);
        }

        private async Task<List<SearchResult>> FilterAndSortData(string searchInput, IEnumerable<Building> buildings, IEnumerable<Group> groups, IEnumerable<Lock> locks, IEnumerable<Medium> media)
        {
            List<SearchResult> results = new List<SearchResult>();
            //TODO: Implement algorithm to calculate weight
            return results;
        }
    }
}
