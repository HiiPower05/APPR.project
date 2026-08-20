using APPR.coreproject.Data;
using APPR.coreproject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APPR.coreproject.Pages.Donate
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
            public Donation Donation { get; set; } = new Donation();
            public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Donation.DonationDate = DateTime.Now;

            _context.Donations.Add(Donation);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
