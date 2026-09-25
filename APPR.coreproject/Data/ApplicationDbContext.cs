using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using APPR.coreproject.Models;

namespace APPR.coreproject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)

        {
        }
        // tells entity FW where the model Donation & Volunteer is.
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Volunteers> Volunteer { get; set; }
        public DbSet<ProjectUpdate> ProjectUpdate { get; set; }
    }
}
