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
            var searchWords = searchInput.Split();
            var buildings = await _dataAccessLayer.GetBuildingsBySearchInput(searchWords);
            var groups = await _dataAccessLayer.GetGroupsBySearchInput(searchWords);
            var locks = await _dataAccessLayer.GetLocksBySearchInput(searchWords);
            var media = await _dataAccessLayer.GetMediaBySearchInput(searchWords);

            return await FilterAndSortData(searchInput, searchWords, buildings, groups, locks, media);
        }

        public async Task<List<SearchResult>> FilterAndSortData(string searchInput, string[] searchWords, List<Building> buildings, List<Group> groups, List<Lock> locks, List<Medium> media)
        {
            List<SearchResult> results = new List<SearchResult>();
            ProcessBuildings(searchInput, searchWords, buildings, ref results);
            ProcessLocks(searchInput, searchWords, locks, ref results);
            ProcessGroups(searchInput, searchWords, groups, ref results);
            ProcessMedia(searchInput, searchWords, media, ref results);

            return results.OrderByDescending(x => x.Weight).ThenBy(n => n.Name).ToList();
        }

        public static void ProcessBuildings(string searchInput, string[] searchWords, List<Building> buildings, ref List<SearchResult> results)
        {
            if (buildings != null && buildings.Any())
            {
                foreach (var building in buildings)
                {
                    CalculateBuildingWeight(searchInput, searchWords, building, ref results);
                }
            }
        }

        public static void CalculateBuildingWeight(string searchInput, string[] searchWords, Building building, ref List<SearchResult> results)
        {
            int weight = 0;
            if (searchWords.Any(w => building.Name.Contains(w)))
            {
                weight += (building.Name == searchInput) ? 9 * 10 : 9;
            }

            if (searchWords.Any(w => building.ShortCut.Contains(w)))
            {
                weight += (building.ShortCut == searchInput) ? 7 * 10 : 7;
            }

            if (building.Description != null && searchWords.Any(w => building.Description.Contains(w)))
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

        public static void ProcessLocks(string searchInput, string[] searchWords, List<Lock> locks, ref List<SearchResult> results)
        {
            if (locks != null && locks.Any())
            {
                foreach (var lockEntity in locks)
                {
                    CalculateLockWeight(searchInput, searchWords, lockEntity, ref results);
                }
            }
        }

        public static void CalculateLockWeight(string searchInput, string[] searchWords, Lock lockEntity, ref List<SearchResult> results)
        {
            int weight = 0;
            if (searchWords.Any(w => lockEntity.Name.Contains(w)))
            {
                weight += (lockEntity.Name == searchInput) ? 10 * 10 : 10;
            }

            if (searchWords.Any(w => lockEntity.Type.Contains(w)))
            {
                weight += (lockEntity.Type == searchInput) ? 3 * 10 : 3;
            }

            if (searchWords.Any(w => lockEntity.SerialNumber.Contains(w)))
            {
                weight += (lockEntity.SerialNumber == searchInput) ? 8 * 10 : 8;
            }

            if (lockEntity.Floor != null && searchWords.Any(w => lockEntity.Floor.Contains(w)))
            {
                weight += (lockEntity.Floor == searchInput) ? 6 * 10 : 6;
            }

            if (searchWords.Any(w => lockEntity.RoomNumber.Contains(w)))
            {
                weight += (lockEntity.RoomNumber == searchInput) ? 6 * 10 : 6;
            }

            if (lockEntity.Description != null && searchWords.Any(w => lockEntity.Description.Contains(w)))
            {
                weight += (lockEntity.Description == searchInput) ? 6 * 10 : 6;
            }

            if (lockEntity.Building != null)
            {
                if (searchWords.Any(w => lockEntity.Building.Name.Contains(w)))
                {
                    weight += (lockEntity.Building.Name == searchInput) ? 8 * 10 : 8;
                }

                if (searchWords.Any(w => lockEntity.Building.ShortCut.Contains(w)))
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

        public static void ProcessGroups(string searchInput, string[] searchWords, List<Group> groups, ref List<SearchResult> results)
        {
            if (groups != null && groups.Any())
            {
                foreach (var group in groups)
                {
                    CalculateGroupWeight(searchInput, searchWords, group, ref results);
                }
            }
        }

        public static void CalculateGroupWeight(string searchInput, string[] searchWords, Group group, ref List<SearchResult> results)
        {
            int weight = 0;
            if (searchWords.Any(w => group.Name.Contains(w)))
            {
                weight += (group.Name == searchInput) ? 9 * 10 : 9;
            }

            if (group.Description != null && searchWords.Any(w => group.Description.Contains(w)))
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

        public static void ProcessMedia(string searchInput, string[] searchWords, List<Medium> media, ref List<SearchResult> results)
        {
            if (media != null && media.Any())
            {
                foreach (var medium in media)
                {
                    CalculateMediumWeight(searchInput, searchWords, medium, ref results);
                }
            }
        }

        public static void CalculateMediumWeight(string searchInput, string[] searchWords, Medium medium, ref List<SearchResult> results)
        {
            int weight = 0;
            if (searchWords.Any(w => medium.Owner.Contains(w)))
            {
                weight += (medium.Owner == searchInput) ? 10 * 10 : 10;
            }

            if (searchWords.Any(w => medium.SerialNumber.Contains(w)))
            {
                weight += (medium.SerialNumber == searchInput) ? 8 * 10 : 8;
            }

            if (medium.Description != null && searchWords.Any(w => medium.Description.Contains(w)))
            {
                weight += (medium.Description == searchInput) ? 6 * 10 : 6;
            }

            if (searchWords.Any(w => medium.Type.Contains(w)))
            {
                weight += (medium.Type == searchInput) ? 3 * 10 : 3;
            }

            if (medium.Group != null)
            {
                if (searchWords.Any(w => medium.Group.Name.Contains(w)))
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
