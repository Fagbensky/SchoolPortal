using School_Portal.BE.Core.Application.Interfaces;
using School_Portal.BE.Core.Domain.Entities;
using School_Portal.BE.Core.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Portal.BE.Core.Infrastructure.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly SchoolContext _context;

        public SubjectRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
        {
            return await _context.Subjects.AsNoTracking().ToListAsync();
        }

        public async Task CreateSubjectAsync(Subject subject)
        {
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
        }
        public async Task<Subject?> GetSubjectWithStudentsGradeAsync(int subjectId)
        {
            return await _context.Subjects
                .Include(s => s.StudentSubjects)
                .ThenInclude(ss => ss.Student.Grades.Where(g => g.SubjectId == subjectId))
                .Where(s => s.Id == subjectId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CheckSubjectExistByIdAsync(int studentId)
        {
            var gradesCount = await _context.Students
                .Where(g => g.Id == studentId)
                .AsNoTracking()
                .CountAsync();

            return gradesCount > 0;
        }
    }
}
