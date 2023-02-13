namespace CodeChallenge_SV.Models
{
    public class Group
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String? Description { get; set; }

        public ICollection<Medium> Media { get; set; }
    }
}
