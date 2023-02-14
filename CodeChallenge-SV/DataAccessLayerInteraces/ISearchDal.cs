using CodeChallenge_SV.Models;

namespace CodeChallenge_SV.DataAccessLayerInteraces
{
    public interface ISearchDal
    {
        Task<List<Building>> GetBuildingsBySearchInput(string[] searchWords);
        Task<List<Lock>> GetLocksBySearchInput(string[] searchWords);
        Task<List<Group>> GetGroupsBySearchInput(string[] searchWords);
        Task<List<Medium>> GetMediaBySearchInput(string[] searchWords);
    }
}
