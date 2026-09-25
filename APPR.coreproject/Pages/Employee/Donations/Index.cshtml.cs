using APPR.coreproject.Data;
using APPR.coreproject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace APPR.coreproject.Pages.Employee.Donations
{
    [Authorize(Roles = "Employee")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
    public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Donation> Donations { get; set; } = new();

        public async Task OnGetAsync()
        {
            Donations = await _context.Donations
                .AsNoTracking()
                .OrderByDescending(d => d.DonationDate)
                .ToListAsync();
        }
    }

}
