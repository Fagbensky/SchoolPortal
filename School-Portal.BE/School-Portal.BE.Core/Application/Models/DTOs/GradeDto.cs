using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Portal.BE.Core.Application.Models.DTOs
{
    public class GradeDto
    {
        public int? Id { get; set; }
        public int? Value { get; set; }
        public string? Note { get; set; }
        public int? StudentId { get; set; }
        public int? SubjectId { get; set; }
    }
}
