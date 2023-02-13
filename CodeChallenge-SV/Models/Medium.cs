namespace CodeChallenge_SV.Models
{
    public class Medium
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public String Type { get; set; }
        public String Owner { get; set; }
        public String SerialNumber { get; set; }
        public String? Description { get; set; }

        public virtual Group Group { get; set; }
    }
}
