using APPR.coreproject.Data;
using APPR.coreproject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace APPR.coreproject.Pages.Employee
{
    [Authorize(Roles = "Employee")]
    public class CreateProjectUpdateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        // Constructor and the daatabase connect. ApplicationDbContext is connection to the database.
        public CreateProjectUpdateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // connecting the form to ProjectUpdate.
        [BindProperty]
        public ProjectUpdate ProjectUpdate { get; set; } = new();

        public void OnGet()
        {
        }
        // When an employee submits a form. OnPostAsync runs.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ProjectUpdate.DatePosted = DateTime.Now;

            // _context lets us access and save our data.
            _context.ProjectUpdate.Add(ProjectUpdate);
            // saves.
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
