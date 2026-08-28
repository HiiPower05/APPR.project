using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using APPR.coreproject.Data;
using APPR.coreproject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
namespace APPR.coreproject.Pages.Employee
{
    [Authorize(Roles = "Employee")]
    public class VolunteersModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public VolunteersModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Volunteers> VolunteerList { get; set; } = new();

        public async Task OnGetAsync()
        {
            VolunteerList = await _context.Volunteer
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
