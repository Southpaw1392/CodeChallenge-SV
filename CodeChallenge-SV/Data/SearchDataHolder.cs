using CodeChallenge_SV.Models;

namespace CodeChallenge_SV.Data
{
    public class SearchDataHolder
    {
        public List<Building> Buildings { get; set; } = new List<Building>();
        public List<Group> Groups { get; set; } = new List<Group>();
        public List<Lock> Locks { get; set; } = new List<Lock>();
        public List<Medium> Media { get; set; } = new List<Medium>();
    }
}
