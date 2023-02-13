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

        private async Task<List<SearchResult>> FilterAndSortData(string searchInput, List<Building> buildings, List<Group> groups, List<Lock> locks, List<Medium> media)
        {
            List<SearchResult> results = new List<SearchResult>();
            ProcessBuildings(searchInput, buildings, results);

            return results.OrderByDescending(x => x.Weight).ThenBy(n => n.Name).ToList();
        }

        private static void ProcessBuildings(string searchInput, List<Building> buildings, List<SearchResult> results)
        {
            if (buildings != null && buildings.Any())
            {
                foreach (var building in buildings)
                {
                    int weight = 0;
                    if (building.Name.Contains(searchInput))
                    {
                        weight += (building.Name == searchInput) ? 9 * 10 : 9;
                    }

                    if (building.ShortCut.Contains(searchInput))
                    {
                        weight += (building.ShortCut == searchInput) ? 7 * 10 : 7;
                    }

                    if (building.Description != null && building.Description.Contains(searchInput))
                    {
                        weight += (building.Description == searchInput) ? 5 * 10 : 5;
                    }

                    if (weight > 0)
                    {
                        results.Add(new SearchResult
                        {
                            Weight = weight,
                            EntityType = "Building",
                            Name = building.Name,
                            ShortCut = building.ShortCut,
                            Description = building.Description
                        });
                    }
                }
            }
        }
    }
}
