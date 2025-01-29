using Microsoft.EntityFrameworkCore;
using School_Portal.BE.Core.Application.Interfaces;
using School_Portal.BE.Core.Domain.Entities;
using School_Portal.BE.Core.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Portal.BE.Core.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolContext _context;

        public StudentRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetAllStudentsAsync(int? subjectId)
        {
            IQueryable<Student> query = _context.Students;

            if (subjectId.HasValue)
            {
                query = query.Where(s => s.StudentSubjects.Any(ss => ss.SubjectId == subjectId.Value));
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<bool> CheckStudentExistByIdAsync(int studentId)
        {
            var gradesCount = await _context.Students
                .Where(g => g.Id == studentId)
                .AsNoTracking()
                .CountAsync();

            return gradesCount > 0;
        }

        public async Task CreateStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task EnrollStudentInSubjectAsync(int studentId, int subjectId)
        {
            var studentSubject = new StudentSubject { StudentId = studentId, SubjectId = subjectId };
            _context.StudentSubjects.Add(studentSubject);
            await _context.SaveChangesAsync();
        }
    }
}
