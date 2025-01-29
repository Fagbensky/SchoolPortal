using School_Portal.BE.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Portal.BE.Core.Application.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllStudentsAsync(int? subjectId);
        Task CreateStudentAsync(Student student);
        Task EnrollStudentInSubjectAsync(int studentId, int subjectId);
        Task<bool> CheckStudentExistByIdAsync(int studentId);
    }
}
