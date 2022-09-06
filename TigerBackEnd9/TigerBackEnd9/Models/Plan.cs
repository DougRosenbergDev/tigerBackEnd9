using System.Text.Json.Serialization;

namespace TigerBackEnd9.Models
{
    public class Plan
    {
        public int Id { get; set; }

        public string PlanName { get; set; }
        public int PlanPrice { get; set; }

        public virtual int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }

        public virtual ICollection<Device> Devices { get; set; }
    }
}
