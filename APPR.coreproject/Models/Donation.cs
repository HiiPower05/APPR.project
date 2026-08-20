using System.ComponentModel.DataAnnotations;

namespace APPR.coreproject.Models
{
    public class Donation
    {
        public int Id { get; set; }
        // The [Required] is an attribute for validation.
        // Needs to be filled out.
        [Required]
        [Range(1, 1000000)]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(3)]
        public string Currency { get; set; } = string.Empty;

        [Required]
        public string DonationType { get; set; } = string.Empty;

        [Required]
        public string DonorName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string DonorEmail { get; set; } = string.Empty;

        public DateTime DonationDate { get; set; }
    }
}
