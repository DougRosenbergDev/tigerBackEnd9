namespace TigerBackEnd9.Models
{
    public class Device
    {
        public int Id { get; set; }

        public string Type { get; set; }
        public string Model { get; set; }
        public int DevicePrice { get; set; }

        public int PhoneNumberId { get; set; }
        public virtual PhoneNumber PhoneNumber { get; set; }
    }
}
