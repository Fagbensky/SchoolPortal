using School_Portal.BE.Core.Application.Models.DTOs;
using School_Portal.BE.Core.Application.UseCases;
using School_Portal.BE.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Portal.BE.Core.Application.Interfaces
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>> GetAllSubjectsAsync();
        Task CreateSubjectAsync(Subject subject);
        Task<Subject?> GetSubjectWithStudentsGradeAsync(int subjectId);
        Task<bool> CheckSubjectExistByIdAsync(int subjectId);
    }
}
