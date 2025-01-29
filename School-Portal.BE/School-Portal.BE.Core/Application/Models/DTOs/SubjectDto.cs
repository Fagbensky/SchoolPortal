using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Portal.BE.Core.Application.Models.DTOs
{
    public class SubjectDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? MinimumPassMark { get; set; }
        public bool? IsRequired { get; set; }
    }
}
