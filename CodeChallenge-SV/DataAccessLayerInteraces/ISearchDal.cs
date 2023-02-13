using CodeChallenge_SV.Models;

namespace CodeChallenge_SV.DataAccessLayerInteraces
{
    public interface ISearchDal
    {
        Task<List<Building>> GetBuildingsBySearchInput(string searchInput);
        Task<List<Lock>> GetLocksBySearchInput(string searchInput);
        Task<List<Group>> GetGroupsBySearchInput(string searchInput);
        Task<List<Medium>> GetMediaBySearchInput(string searchInput);
    }
}
