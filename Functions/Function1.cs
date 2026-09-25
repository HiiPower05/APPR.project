using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace APPR.coreproject.Functions
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function("GenerateTaxCertificate")]
        public async Task<IActionResult> Run(
            [HttpTrigger(
                AuthorizationLevel.Anonymous,
                "post",
                Route = "GenerateTaxCertificate")] HttpRequest req)
        {
            _logger.LogInformation(
                "Tax certificate generation function received a request.");

            DonationRequest? donation;

            try
            {
                donation = await JsonSerializer.DeserializeAsync<DonationRequest>(
                    req.Body,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
            catch (JsonException)
            {
                return new BadRequestObjectResult(
                    "Invalid donation data.");
            }

            if (donation == null)
            {
                return new BadRequestObjectResult(
                    "Donation data is required.");
            }

            if (string.IsNullOrWhiteSpace(donation.TransactionReference) ||
                string.IsNullOrWhiteSpace(donation.DonorName) ||
                string.IsNullOrWhiteSpace(donation.DonorEmail) ||
                donation.Amount <= 0 ||
                string.IsNullOrWhiteSpace(donation.Currency))
            {
                return new BadRequestObjectResult(
                    "Required donation information is missing.");
            }

            var certificateNumber =
                $"TAX-{donation.TransactionReference}";

            var certificate = new TaxCertificateResponse
            {
                CertificateNumber = certificateNumber,
                TransactionReference = donation.TransactionReference,
                DonorName = donation.DonorName,
                DonorEmail = donation.DonorEmail,
                Amount = donation.Amount,
                Currency = donation.Currency,
                DonationType = donation.DonationType,
                GeneratedDate = DateTime.UtcNow,
                Message = "Dummy tax certificate generated successfully."
            };

            _logger.LogInformation(
                "Dummy tax certificate {CertificateNumber} generated for donation {TransactionReference}.",
                certificate.CertificateNumber,
                certificate.TransactionReference);

            return new OkObjectResult(certificate);
        }
    }

    public class DonationRequest
    {
        public string TransactionReference { get; set; } = string.Empty;

        public string DonorName { get; set; } = string.Empty;

        public string DonorEmail { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public string Currency { get; set; } = string.Empty;

        public string DonationType { get; set; } = string.Empty;
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