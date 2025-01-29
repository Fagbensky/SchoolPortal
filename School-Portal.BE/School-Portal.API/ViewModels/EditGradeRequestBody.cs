using System.ComponentModel.DataAnnotations;

namespace School_Portal.API.ViewModels
{
    public class EditGradeRequestBody
    {
        [Required]
        [Range(1, 100, ErrorMessage = "Value must be between {1} and {2}.")]
        public int? Value { get; set; }
        [Required]
        [MaxLength(500)]
        public string? Note { get; set; }
    }
}
