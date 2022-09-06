using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TigerBackEnd5.Data;
using TigerBackEnd9.DataTransferModels;
//using TigerBackEnd9.Data;
//using TigerBackEnd9.DataTransferModels;
using TigerBackEnd9.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TigerBackEnd9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TigerController : ControllerBase
    {
        private readonly ILogger<TigerController> _logger;
        private readonly TigerContext _context;
        // GET: api/<TigerController>

        public TigerController(ILogger<TigerController> logger, TigerContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("Users")]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return await _context.Users
                .Include(u => u.Plans)
                .ToListAsync();
        }

        [HttpGet("Plans")]
        public async Task<ActionResult<IEnumerable<Plan>>> GetAllPlans()
        {
            //if (_context.Plan == null)
            //{
            //    return NotFound();
            //}
            return await _context
                .Plans
                .ToListAsync();
        }

        [HttpGet("Devices")]
        public async Task<ActionResult<IEnumerable<Device>>> GetAllDevices()
        {
            if (_context.Devices == null)
            {
                return NotFound();
            }
            return await _context
                .Devices
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserInfo(int userId)
        {
            if (_context.Users == null) { return NotFound(); }

            User? TargetUser = await _context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Plans)
                .FirstAsync();

            if (TargetUser == null) { return NotFound(); }

            return TargetUser;
        }

        [HttpGet("{id}/Plans")]
        public async Task<ActionResult<IEnumerable<PlanInfo>>> GetUserPlans(int userId)
        {
            var plans = await _context
                .Plans
                .Where(p => p.UserId == userId)
                .Include(p => p.Devices)
                .ToListAsync();
            var result = new List<PlanInfo>();
            foreach (var plan in plans)
            {
                var profile = await _context.Plans.FindAsync(plan.PlanId);
                result.Add(new PlanInfo(plan));
            }

            return result;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateNewUser(CreateUser newUser)
        {
            if (_context == null) { return Problem("New user could not be created."); }

            User user = newUser.ToDataModel();
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        [HttpPost("{id}/Plan")]
        public async Task<ActionResult<User>> CreatePlanForUser(int userId, AddPlan newPlan)
        {
            User? user = await _context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Plans)
                .FirstOrDefaultAsync();
            if (user == null) { return NotFound(); }

            Plan plan = await newPlan.ToDataModel(_context);
            _context.Plans.Add(plan);
            user.Plans.Add(plan);

            await _context.SaveChangesAsync();

            return user;
        }

        [HttpPost("{id}/Device")]
        public async Task<ActionResult<User>> CreateDeviceForUserWithPlan(int userId, int planId, Device device)
        {
            User? user = await _context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Plans)
                .FirstOrDefaultAsync();
            Plan? plan = await _context.Plans
                .Where(p => p.Id == planId)
                .Include(p => p.Devices)
                //.Include(p => p.PlanProfileId)
                .FirstOrDefaultAsync();
            if (user == null || plan == null) { return NotFound(); }

            var profile = await _context.Plans.FindAsync(plan.PlanId);
            if (profile == null) { return NotFound(); }
            //if (profile.DeviceLimit >= plan.Devices.Count)
            //{
            //    return Problem("This plan cannot support any more devices.");
            //}

            _context.Devices.Add(device);
            plan.Devices.Add(device);
            await _context.SaveChangesAsync();

            return await GetUserInfo(userId);
        }





        //// POST api/<TigerController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<TigerController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<TigerController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
