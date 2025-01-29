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
    public class GradeRepository: IGradeRepository
    {
        private readonly SchoolContext _context;

        public GradeRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Grade grade)
        {
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();
        }
        public async Task<Grade?> GetGradesByIdAsync(int id, bool asNoTrancking = true)
        {
            var query = _context.Grades;

            if (asNoTrancking)
            {
                query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Grade?> GetFullGradesByIdAsync(int id, bool asNoTrancking = true)
        {
            var query = _context.Grades
                .Include(g => g.Student)
                .Include(g => g.Subject);
            
            if (asNoTrancking)
            {
                query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Grade>> GetGradesByStudentAndSubjectAsync(int? studentId, int? subjectId)
        {
            var query = _context.Grades
                .AsNoTracking()
                .AsQueryable();

            if (studentId.HasValue)
            {
                query = query.Where(g => g.StudentId == studentId.Value);
            }

            if (subjectId.HasValue)
            {
                query = query.Where(g => g.SubjectId == subjectId.Value);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task UpdateAsync(Grade grade)
        {
            _context.Grades.Update(grade);
            await _context.SaveChangesAsync();
        }

        public async Task<Grade?> GetGradeByStudentAndSubjectAsync(int studentId, int subjectId)
        {
            return await _context.Grades
                .Where(g => g.StudentId == studentId && g.SubjectId == subjectId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CheckGradeExistByStudentAndSubjectAsync(int studentId, int subjectId)
        {
            var gradesCount = await _context.Grades
                .Where(g => g.StudentId == studentId && g.SubjectId == subjectId)
                .AsNoTracking()
                .CountAsync();

            return gradesCount > 0;
        }
    }
}
