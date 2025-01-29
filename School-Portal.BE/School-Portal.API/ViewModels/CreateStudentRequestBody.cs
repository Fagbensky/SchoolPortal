using System.ComponentModel.DataAnnotations;

namespace School_Portal.API.ViewModels
{
    public class CreateStudentRequestBody
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
