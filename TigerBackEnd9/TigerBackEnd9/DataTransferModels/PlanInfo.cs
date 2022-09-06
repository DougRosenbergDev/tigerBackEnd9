using TigerBackEnd9.Models;

namespace TigerBackEnd9.DataTransferModels
{
    public class PlanInfo
    {
        public int Id { get; }
        public int ProfileId { get; }
        public string PlanName { get; }
        public int PlanPrice { get; }
        //public int DeviceLimit { get; }
        public ICollection<Device> Devices { get; set; }
        public PlanInfo(Plan plan)
        {
            Id = plan.Id;
            PlanName = plan.PlanName;
            PlanPrice = plan.PlanPrice;
            //DeviceLimit = profile.DeviceLimit;
            Devices = plan.Devices;
        }
    }
}
