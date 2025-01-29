using System.ComponentModel.DataAnnotations;

namespace School_Portal.API.ViewModels
{
    public class SubjectRequestBody
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [Range(1, 100, ErrorMessage = "Value must be between {1} and {2}.")]
        public int MinimumPassMark { get; set; }
        [Required]
        public bool IsRequired { get; set; }
    }
}
