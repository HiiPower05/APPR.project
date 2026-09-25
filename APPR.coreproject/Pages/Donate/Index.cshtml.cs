using System.Net.Http.Json;
using APPR.coreproject.Data;
using APPR.coreproject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GiftOfTheGivers.Helpers;

namespace APPR.coreproject.Pages.Donate
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(
            ApplicationDbContext context,
            IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
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

            decimal calculatedAmount =
    DonationCalculator.CalculateTotal(new[] { Donation.Amount });

            Donation.DonationDate = DateTime.Now;

            // Generate a unique transaction reference.
            Donation.TransactionReference =
                $"DON-{Guid.NewGuid().ToString("N").Substring(0, 12).ToUpper()}";

            // Save the donation to SQL Server first.
            _context.Donations.Add(Donation);

            await _context.SaveChangesAsync();

            // Prepare the information that will be sent
            // to the Azure Function.
            var donationRequest = new
            {
                transactionReference = Donation.TransactionReference,
                donorName = Donation.DonorName,
                donorEmail = Donation.DonorEmail,
                amount = calculatedAmount,
                currency = Donation.Currency,
                donationType = Donation.DonationType
            };

            string? certificateNumber = null;

            try
            {
                var client = _httpClientFactory.CreateClient();

                var response = await client.PostAsJsonAsync(
                    "http://localhost:7175/api/GenerateTaxCertificate",
                    donationRequest);

                if (response.IsSuccessStatusCode)
                {
                    var certificate =
                        await response.Content
                            .ReadFromJsonAsync<TaxCertificateResponse>();

                    certificateNumber =
                        certificate?.CertificateNumber;
                }
            }
            catch (HttpRequestException)
            {
                // The donation has already been saved.
                // Continue to the confirmation page even if
                // the local Azure Function is unavailable.
            }

            return RedirectToPage(
                "./Confirmation",
                new
                {
                    reference = Donation.TransactionReference,
                    certificate = certificateNumber
                });
        }
    }

    public class TaxCertificateResponse
    {
        public string CertificateNumber { get; set; } = string.Empty;

        public string TransactionReference { get; set; } = string.Empty;

        public string DonorName { get; set; } = string.Empty;

        public string DonorEmail { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public string Currency { get; set; } = string.Empty;

        public string DonationType { get; set; } = string.Empty;

        public DateTime GeneratedDate { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}