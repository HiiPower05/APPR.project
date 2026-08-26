using APPR.coreproject.Data;
using APPR.coreproject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APPR.coreproject.Pages.Volunteer
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        // Gives access to database.
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Volunteers Volunteers { get; set; } = new Volunteers();
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Volunteer.Add(Volunteers);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

