using System.ComponentModel.DataAnnotations;

namespace APPR.coreproject.Models
{
    public class ProjectUpdate
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public DateTime DatePosted { get; set; } = DateTime.Now;
    }
}
