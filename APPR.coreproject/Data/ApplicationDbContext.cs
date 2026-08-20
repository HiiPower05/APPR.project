using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using APPR.coreproject.Models;

namespace APPR.coreproject.Data
{
    public class ApplicationDbContext : IdentityDbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)

        {
        }
        // tells entity FW where the model Donation is.
        public DbSet<Donation> Donations { get; set; }
    }
}
