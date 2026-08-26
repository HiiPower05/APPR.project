using System.ComponentModel.DataAnnotations;

namespace APPR.coreproject.Models
{
    // database model
    public class Volunteers
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Skills { get; set; } = string.Empty;

        [Required]
        public string Availability { get; set; } = string.Empty;

    }
}
