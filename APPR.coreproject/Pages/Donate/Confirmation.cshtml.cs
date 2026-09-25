using APPR.coreproject.Data;
using APPR.coreproject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace APPR.coreproject.Pages.Donate
{
    [AllowAnonymous]
    public class ConfirmationModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ConfirmationModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Donation? Donation { get; set; }

        public string? CertificateNumber { get; set; }

        public async Task<IActionResult> OnGetAsync(
            string reference,
            string? certificate)
        {
            if (string.IsNullOrWhiteSpace(reference))
            {
                return NotFound();
            }

            Donation = await _context.Donations
                .AsNoTracking()
                .FirstOrDefaultAsync(d =>
                    d.TransactionReference == reference);

            if (Donation == null)
            {
                return NotFound();
            }

            CertificateNumber = certificate;

            return Page();
        }
    }
}