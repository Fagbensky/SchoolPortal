using System.ComponentModel.DataAnnotations;

namespace School_Portal.API.ViewModels
{
    public class EnrollStudentRequestBody
    {
        [Required]
        public int SubjectId { get; set; }
    }
}
