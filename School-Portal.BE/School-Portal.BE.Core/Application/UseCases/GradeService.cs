using School_Portal.BE.Core.Application.Interfaces;
using School_Portal.BE.Core.Application.Models.DTOs;
using School_Portal.BE.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Portal.BE.Core.Application.UseCases
{
    public class GradeService
    {
        private readonly IGradeRepository _gradeRepository;

        public GradeService(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public async Task<BaseResponse<GradeDto>> GetGradeById(int gradeId)
        {
            var grade = await _gradeRepository.GetGradesByIdAsync(gradeId);
            if (grade == null)
            {
                return new BaseResponse<GradeDto>(false, "Grade not found.");
            }

            var mappedGrade = new GradeDto
            {
                Id = grade.Id,
                Value = grade.Value,
                Note = grade.Note,
                StudentId = grade.StudentId,
                SubjectId = grade.SubjectId,
            };

            return new BaseResponse<GradeDto>(true, "Grade found successfully", mappedGrade);
        }

        public async Task<BaseResponse<IEnumerable<GradeDto>>> GetGradesByStudentAndSubject(int? studentId, int? subjectId)
        {
            var grades = await _gradeRepository.GetGradesByStudentAndSubjectAsync(studentId, subjectId);
            var gradesDtos = grades.Select(s => new GradeDto
            {
                Id = s.Id,
                Value = s.Value,
                Note = s.Note,
                SubjectId = s.SubjectId,
                StudentId = s.StudentId,
            }).ToList();
            return new BaseResponse<IEnumerable<GradeDto>>(true, "Grades fetched successfully", gradesDtos);
        }

        public async Task<BaseResponse<GradeDto>> GetGradeByStudentAndSubject(int studentId, int subjectId)
        {
            var grade = await _gradeRepository.GetGradeByStudentAndSubjectAsync(studentId, subjectId);
            if (grade == null)
            {
                return new BaseResponse<GradeDto>(false, "Grade not found.");
            }

            var mappedGrade = new GradeDto
            {
                Id = grade.Id,
                Value = grade.Value,
                Note = grade.Note,
                StudentId = grade.StudentId,
                SubjectId = grade.SubjectId,
            }; 
            return new BaseResponse<GradeDto>(true, "Grade found successfully", mappedGrade);
        }

        public async Task<BaseResponse> AssignGrade(GradeDto gradeDto)
        {
            var gradeExists = await _gradeRepository.CheckGradeExistByStudentAndSubjectAsync(Convert.ToInt32(gradeDto.StudentId), Convert.ToInt32(gradeDto.SubjectId));
            
            if (gradeExists)
            {
                return new BaseResponse(false, "Grade already assigned.");
            }

            var grade = new Grade
            {
                Value = Convert.ToInt32(gradeDto.Value),
                Note = gradeDto.Note,
                StudentId = Convert.ToInt32(gradeDto.StudentId),
                SubjectId = Convert.ToInt32(gradeDto.SubjectId)
            };

            await _gradeRepository.AddAsync(grade);
            return new BaseResponse(true, "Grade assigned successfully");
        }

        public async Task<BaseResponse> EditGrade(int gradeId, GradeDto gradeDto)
        {
            var grade = await _gradeRepository.GetGradesByIdAsync(gradeId, false);
            if (grade == null)
            {
                return new BaseResponse(false, "Grade not found.");
            }

            if(gradeDto.Value != null) grade.Value = Convert.ToInt32(gradeDto.Value);
            if(gradeDto.Value != null) grade.Note = gradeDto.Note;

            await _gradeRepository.UpdateAsync(grade);

            return new BaseResponse(true, "Grade updated successfully");
        }
    }
}
