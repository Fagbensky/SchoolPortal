using School_Portal.BE.Core.Application.Interfaces;
using School_Portal.BE.Core.Application.Models.DTOs;
using School_Portal.BE.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Portal.BE.Core.Application.UseCases
{
    public class SubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<BaseResponse<List<SubjectDto>>> GetAllSubject() 
        {
            var subjects = await _subjectRepository.GetAllSubjectsAsync();
            var subjectDtos = subjects.Select(s => new SubjectDto
            {
                Id = s.Id,
                Name = s.Name, 
                MinimumPassMark = s.MinimumPassMark,
                IsRequired = s.IsRequired
            }).ToList(); 

            return new BaseResponse<List<SubjectDto>>(true, "Subject retrieved successfully.", subjectDtos);
        }

        public async Task<BaseResponse> CreateSubject(SubjectDto subjectDto)
        {
            var subject = new Subject
            {
                Name = subjectDto.Name,
                IsRequired = Convert.ToBoolean(subjectDto.IsRequired)
            };
            await _subjectRepository.CreateSubjectAsync(subject);

            return new BaseResponse(true, "Subject created successfully.");
        }

        public async Task<BaseResponse<SubjectWithStudentsGradeDto>> GetSubjectWithStudents(int subjectId)
        {
            var subject = await _subjectRepository.GetSubjectWithStudentsGradeAsync(subjectId);

            if (subject == null)
            {
                return new BaseResponse<SubjectWithStudentsGradeDto>(false, "Subject not found"); 
            }

            var subjectDto = new SubjectWithStudentsGradeDto
            {
                Id = subject.Id,
                Name = subject.Name,
                MinimumPassMark = subject.MinimumPassMark,
                IsRequired = subject.IsRequired,
                Students = subject.StudentSubjects.Select(ss => new StudentWithGradeDto
                {
                    Id = ss.Student.Id,
                    Name = ss.Student.Name,
                    Grade = ss.Student.Grades.FirstOrDefault() != null ? new GradeDto{
                        Id = (ss.Student.Grades.FirstOrDefault()).Id,
                        Value = (ss.Student.Grades.FirstOrDefault()).Value,
                        Note = (ss.Student.Grades.FirstOrDefault()).Note,
                        StudentId = (ss.Student.Grades.FirstOrDefault()).StudentId,
                        SubjectId = (ss.Student.Grades.FirstOrDefault()).SubjectId,
                    } : null
                }).ToList()
            };

            return new BaseResponse<SubjectWithStudentsGradeDto>(true, "Subject retrieved successfully", subjectDto);
        }
    }
}
