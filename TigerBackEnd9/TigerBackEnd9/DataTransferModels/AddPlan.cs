using TigerBackEnd5.Data;
using TigerBackEnd9.Models;

namespace TigerBackEnd9.DataTransferModels
{
    public class AddPlan
    {
        public int UserId { get; set; }
        public int PlanId { get; set; }

        public async Task<Plan> ToDataModel(TigerContext context)
        {
            return new Plan
            {
                UserId = UserId,
                User = await context.Users.FindAsync(UserId),
                PlanId = PlanId,
                //Plan = await context.Plans.FindAsync(PlanId),
                Devices = new List<Device>()
            };
        }
    }
}
