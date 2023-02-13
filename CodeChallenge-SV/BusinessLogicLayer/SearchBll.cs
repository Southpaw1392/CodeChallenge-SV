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
            ProcessBuildings(searchInput, buildings, ref results);
            ProcessLocks(searchInput, locks, ref results);
            ProcessGroups(searchInput, groups, ref results);
            ProcessMedia(searchInput, media, ref results);

            return results.OrderByDescending(x => x.Weight).ThenBy(n => n.Name).ToList();
        }

        private static void ProcessBuildings(string searchInput, List<Building> buildings, ref List<SearchResult> results)
        {
            if (buildings != null && buildings.Any())
            {
                foreach (var building in buildings)
                {
                    CalculateBuildingWeight(searchInput, building, ref results);
                }
            }
        }

        private static void CalculateBuildingWeight(string searchInput, Building building, ref List<SearchResult> results)
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

        private static void ProcessLocks(string searchInput, List<Lock> locks, ref List<SearchResult> results)
        {
            if (locks != null && locks.Any())
            {
                foreach (var lockEntity in locks)
                {
                    CalculateLockWeight(searchInput, lockEntity, ref results);
                }
            }
        }

        private static void CalculateLockWeight(string searchInput, Lock lockEntity, ref List<SearchResult> results)
        {
            int weight = 0;
            if (lockEntity.Name.Contains(searchInput))
            {
                weight += (lockEntity.Name == searchInput) ? 10 * 10 : 10;
            }

            if (lockEntity.Type.Contains(searchInput))
            {
                weight += (lockEntity.Type == searchInput) ? 3 * 10 : 3;
            }

            if (lockEntity.SerialNumber.Contains(searchInput))
            {
                weight += (lockEntity.SerialNumber == searchInput) ? 8 * 10 : 8;
            }

            if (lockEntity.Floor != null && lockEntity.Floor.Contains(searchInput))
            {
                weight += (lockEntity.Floor == searchInput) ? 6 * 10 : 6;
            }

            if (lockEntity.RoomNumber.Contains(searchInput))
            {
                weight += (lockEntity.RoomNumber == searchInput) ? 6 * 10 : 6;
            }

            if (lockEntity.Description != null && lockEntity.Description.Contains(searchInput))
            {
                weight += (lockEntity.Description == searchInput) ? 6 * 10 : 6;
            }

            if (lockEntity.Building != null)
            {
                if (lockEntity.Building.Name.Contains(searchInput))
                {
                    weight += (lockEntity.Building.Name == searchInput) ? 8 * 10 : 8;
                }

                if (lockEntity.Building.ShortCut.Contains(searchInput))
                {
                    weight += (lockEntity.Building.ShortCut == searchInput) ? 5 * 10 : 5;
                }
            }

            results.Add(new SearchResult
            {
                Weight = weight,
                EntityType = "Lock",
                Name = lockEntity.Name,
                Type = lockEntity.Type,
                SerialNumber = lockEntity.SerialNumber,
                Floor = lockEntity.Floor,
                RoomNumber = lockEntity.RoomNumber,
                Description = lockEntity.Description
            });
        }

        private static void ProcessGroups(string searchInput, List<Group> groups, ref List<SearchResult> results)
        {
            if (groups != null && groups.Any())
            {
                foreach (var group in groups)
                {
                    CalculateGroupWeight(searchInput, group, ref results);
                }
            }
        }

        private static void CalculateGroupWeight(string searchInput, Group group, ref List<SearchResult> results)
        {
            int weight = 0;
            if (group.Name.Contains(searchInput))
            {
                weight += (group.Name == searchInput) ? 9 * 10 : 9;
            }

            if (group.Description != null && group.Description.Contains(searchInput))
            {
                weight += (group.Description == searchInput) ? 5 * 10 : 5;
            }

            if (weight > 0)
            {
                results.Add(new SearchResult
                {
                    Weight = weight,
                    EntityType = "Group",
                    Name = group.Name,
                    Description = group.Description
                });
            }
        }

        private static void ProcessMedia(string searchInput, List<Medium> media, ref List<SearchResult> results)
        {
            if (media != null && media.Any())
            {
                foreach (var medium in media)
                {
                    CalculateMediumWeight(searchInput, results, medium);
                }
            }
        }

        private static void CalculateMediumWeight(string searchInput, List<SearchResult> results, Medium medium)
        {
            int weight = 0;
            if (medium.Owner.Contains(searchInput))
            {
                weight += (medium.Owner == searchInput) ? 10 * 10 : 10;
            }

            if (medium.SerialNumber.Contains(searchInput))
            {
                weight += (medium.SerialNumber == searchInput) ? 8 * 10 : 8;
            }

            if (medium.Description != null && medium.Description.Contains(searchInput))
            {
                weight += (medium.Description == searchInput) ? 6 * 10 : 6;
            }

            if (medium.Type.Contains(searchInput))
            {
                weight += (medium.Type == searchInput) ? 3 * 10 : 3;
            }

            if (medium.Group != null)
            {
                if (medium.Group.Name.Contains(searchInput))
                {
                    weight += (medium.Group.Name == searchInput) ? 8 * 10 : 8;
                }
            }

            results.Add(new SearchResult
            {
                Weight = weight,
                EntityType = "Medium",
                Owner = medium.Owner,
                Type = medium.Type,
                SerialNumber = medium.SerialNumber,
                Description = medium.Description
            });
        }
    }
}
