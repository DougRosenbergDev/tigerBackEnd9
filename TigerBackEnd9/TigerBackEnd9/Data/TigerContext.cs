using Microsoft.EntityFrameworkCore;
using TigerBackEnd9.Models;

namespace TigerBackEnd5.Data
{
    public class TigerContext : DbContext
    {
        public TigerContext(DbContextOptions<TigerContext> options)
            : base(options) { }

        // Primary data sets
        public DbSet<User> Users { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Device> Devices { get; set; }

        // Support tables
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        
    }
}
