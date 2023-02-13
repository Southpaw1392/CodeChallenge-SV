namespace CodeChallenge_SV.Models
{
    public class Lock
    {
        public Guid Id { get; set; }
        public Guid BuildingId { get; set; }
        public String Type { get; set; }
        public String Name { get; set; }
        public String SerialNumber { get; set; }
        public String? Floor { get; set; }
        public String RoomNumber { get; set; }
        public String? Description { get; set; }

        public virtual Building Building { get; set; }
    }
}
