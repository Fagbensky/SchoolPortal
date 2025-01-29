using Microsoft.AspNetCore.Mvc;
using School_Portal.API.ViewModels;
using School_Portal.BE.Core.Application.Models.DTOs;
using School_Portal.BE.Core.Application.UseCases;

namespace School_Portal.API.Endpoints
{
    public static class StudentEndpoints
    {
        public static RouteGroupBuilder RegisterStudentRoutes(this RouteGroupBuilder group)
        {
            var gradeRoute = group.MapGroup("/student");

            gradeRoute.MapGet("", getStudents)
                .WithName("getStudents")
                .Produces<BaseResponse<List<StudentDto>>>(200)
                .Produces<BaseResponse>(400)
                .Produces<BaseResponse>(500);



            gradeRoute.MapPost("", CreateStudent)
                .WithName("CreateStudent")
                .Produces<BaseResponse>(200)
                .Produces<BaseResponse>(400)
                .Produces<BaseResponse>(500);

            gradeRoute.MapPatch("{id}/enroll", enrollStudent)
                .WithName("enrollStudent")
                .Produces<BaseResponse>(200)
                .Produces<BaseResponse>(400)
                .Produces<BaseResponse>(500);

            return group;
        }

        /// <summary>
        /// Get all students
        /// </summary>
        /// <returns>List of Students</returns>
        public static async Task<IResult> getStudents([FromQuery] int? subjectId, StudentService studentService)
        {
            var response = await studentService.GetAllStudents(subjectId);

            return response.Status ? Results.Ok(response) : Results.BadRequest(response);
        }

        /// <summary>
        /// Assign a student a grade
        /// </summary>
        /// <param name="requestBody">Full student request</param>
        /// <returns>BaseResponse</returns>
        public static async Task<IResult> CreateStudent([FromBody] CreateStudentRequestBody requestBody, StudentService studentService)
        {
            var studentDto = new StudentDto
            {
                Name = requestBody.Name,
            };
            var response = await studentService.CreateStudent(studentDto);

            return response.Status ? Results.Ok(response) : Results.BadRequest(response);
        }

        /// <summary>
        /// Enroll student in a subject
        /// </summary>
        /// <param name="id">Specific student identifier</param>
        /// <param name="requestBody">enrollment request</param>
        /// <returns>BaseResponse</returns>
        public static async Task<IResult> enrollStudent(int id, [FromBody] EnrollStudentRequestBody requestBody, StudentService studentService)
        {
            var response = await studentService.EnrollStudent(id, requestBody.SubjectId);

            return response.Status ? Results.Ok(response) : Results.BadRequest(response);
        }
    }
}
