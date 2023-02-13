namespace CodeChallenge_SV.Models
{
    public class Building
    {
        public Guid Id { get; set; }
        public String ShortCut { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }

        public virtual ICollection<Lock> Locks { get; set; }
    }
}
