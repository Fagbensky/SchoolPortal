using School_Portal.BE.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Portal.BE.Core.Application.Interfaces
{
    public interface IGradeRepository
    {
        Task AddAsync(Grade grade);
        Task<Grade?> GetGradesByIdAsync(int id, bool asNoTrancking = true);
        Task<Grade?> GetFullGradesByIdAsync(int id, bool asNoTrancking = true);
        Task<IEnumerable<Grade>> GetGradesByStudentAndSubjectAsync(int? studentId, int? subjectId);
        Task UpdateAsync(Grade grade);
        Task<Grade?> GetGradeByStudentAndSubjectAsync(int studentId, int subjectId);
        Task<bool> CheckGradeExistByStudentAndSubjectAsync(int studentId, int subjectId);
    }
}
