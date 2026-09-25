using APPR.coreproject.Data;
using APPR.coreproject.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace APPR.coreproject.Pages.Updates
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ProjectUpdate> Updates { get; set; } = new();

        public async Task OnGetAsync()
        {
            Updates = await _context.ProjectUpdate
                .AsNoTracking()
                .OrderByDescending(u => u.DatePosted)
                .ToListAsync();
        }
    }
}