using Microsoft.AspNetCore.Mvc;
using School_Portal.API.ViewModels;
using School_Portal.BE.Core.Application.Models.DTOs;
using School_Portal.BE.Core.Application.UseCases;

namespace School_Portal.API.Endpoints
{
    public static class SubjectEndpoints
    {
        public static RouteGroupBuilder RegisterSubjectRoutes(this RouteGroupBuilder group)
        {
            var gradeRoute = group.MapGroup("/subject");

            gradeRoute.MapGet("", getSubjects)
                .WithName("getSubjects")
                .Produces<BaseResponse<List<SubjectDto>>>(200)
                .Produces<BaseResponse>(400)
                .Produces<BaseResponse>(500);

            gradeRoute.MapGet("{id}/students/grade", getSubjectWithStudents)
                .WithName("getSubjectWithStudents")
                .Produces<BaseResponse<SubjectWithStudentsGradeDto>>(200)
                .Produces<BaseResponse>(400)
                .Produces<BaseResponse>(500);

            gradeRoute.MapPost("", CreateSubject)
                .WithName("CreateSubject")
                .Produces<BaseResponse>(200)
                .Produces<BaseResponse>(400)
                .Produces<BaseResponse>(500);

            return group;
        }

        /// <summary>
        /// Get all Subjects
        /// </summary>
        /// <returns>List of Subjects</returns>
        public static async Task<IResult> getSubjects(SubjectService subjectService)
        {
            var response = await subjectService.GetAllSubject();

            return response.Status ? Results.Ok(response) : Results.BadRequest(response);
        }

        /// <summary>
        /// Get specific subjects and enrolled students
        /// </summary>
        /// <param name="id">Course Id</param>
        /// <returns>List of Subjects</returns>
        public static async Task<IResult> getSubjectWithStudents(int id, SubjectService subjectService)
        {
            var response = await subjectService.GetSubjectWithStudents(id);

            return response.Status ? Results.Ok(response) : Results.BadRequest(response);
        }

        /// <summary>
        /// Create a subject
        /// </summary>
        /// <param name="requestBody">Full subject request</param>
        /// <returns>BaseResponse</returns>
        public static async Task<IResult> CreateSubject([FromBody] SubjectRequestBody requestBody, SubjectService subjectService)
        {
            var subjectDto = new SubjectDto
            {
                Name = requestBody.Name,
                IsRequired = requestBody.IsRequired,
            };
            var response = await subjectService.CreateSubject(subjectDto);

            return response.Status ? Results.Ok(response) : Results.BadRequest(response);
        }
    }
}
