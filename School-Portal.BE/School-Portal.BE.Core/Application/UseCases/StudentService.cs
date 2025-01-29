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
    public class StudentService
    {

        private readonly IStudentRepository _studentRepository;
        private readonly ISubjectRepository _subjectRepository;

        public StudentService(IStudentRepository studentRepository, ISubjectRepository subjectRepository)
        {
            _studentRepository = studentRepository;
            _subjectRepository = subjectRepository;
        }

        public async Task<BaseResponse<List<StudentDto>>> GetAllStudents(int? subjectId)
        {
            var students = await _studentRepository.GetAllStudentsAsync(subjectId);

            var studentDtos = students.Select(s => new StudentDto
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();

            return new BaseResponse<List<StudentDto>>(true, "Students fetched successfully.", studentDtos);
        }

        public async Task<BaseResponse> CreateStudent(StudentDto studentDto)
        {
            var student = new Student
            {
                Name = studentDto.Name,
            };

            await _studentRepository.CreateStudentAsync(student);

            return new BaseResponse(true, "Student created successfully.");
        }

        public async Task<BaseResponse> EnrollStudent(int studentId, int subjectId)
        {
            var studentExists = await _studentRepository.CheckStudentExistByIdAsync(studentId);
            if (!studentExists)
            {
                return new BaseResponse(false, "Student doesn't exist.");
            }

            var subjecExistst = await _subjectRepository.CheckSubjectExistByIdAsync(subjectId);
            if (!subjecExistst)
            {
                return new BaseResponse(false, "Subject doesn't exist.");
            }

            await _studentRepository.EnrollStudentInSubjectAsync(studentId, subjectId);

            return new BaseResponse(true, "Student enrolled successfully.");
        }
    }
}
