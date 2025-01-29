using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Portal.BE.Core.Application.Models.DTOs
{
    public class SubjectWithStudentsGradeDto: SubjectDto
    {
        public List<StudentWithGradeDto> Students { get; set; } = new List<StudentWithGradeDto>();
    }
}
