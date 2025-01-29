using Microsoft.AspNetCore.Mvc;
using School_Portal.API.ViewModels;
using School_Portal.BE.Core.Application.Models.DTOs;
using School_Portal.BE.Core.Application.UseCases;

namespace School_Portal.API.Endpoints
{
    public static class GradeEndpoints
    {
        public static RouteGroupBuilder RegisterGradesRoutes(this RouteGroupBuilder group)
        {
            var gradeRoute = group.MapGroup("/grade");

            gradeRoute.MapGet("{id}", GetGradeById)
                .WithName("GetGradeById")
                .Produces<BaseResponse<GradeDto>>(200)
                .Produces<BaseResponse>(400);

            gradeRoute.MapGet("{studentId}/{subjectId}", GetGradeByStudentOrSubject)
                .WithName("GetGradeByStudentOrSubject")
                .Produces<BaseResponse<List<GradeDto>>>(200)
                .Produces<BaseResponse>(400)
                .Produces<BaseResponse>(500);

            gradeRoute.MapGet("", GetGradesByStudentOrSubject)
                .WithName("GetGradesByStudentOrSubject")
                .Produces<BaseResponse<List<GradeDto>>>(200)
                .Produces<BaseResponse>(400)
                .Produces<BaseResponse>(500);

            gradeRoute.MapPost("", AssignGrade)
                .WithName("AssignGrade")
                .Produces<BaseResponse>(200)
                .Produces<BaseResponse>(400)
                .Produces<BaseResponse>(500);

            gradeRoute.MapPatch("{id}", UpdateGrade)
                .WithName("UpdateGrade")
                .Produces<BaseResponse>(200)
                .Produces<BaseResponse>(400)
                .Produces<BaseResponse>(500);

            return group;
        }

        /// <summary>
        /// Get Grade using Id
        /// </summary>
        /// <param name="id">Specific grade identifier</param>
        /// <returns>Information about a specific grade</returns>
        public static async Task<IResult> GetGradeById(int id, GradeService gradeService)
        {
            var response = await gradeService.GetGradeById(id);

            return response.Status ? Results.Ok(response) : Results.NotFound(response);
        }

        /// <summary>
        /// Get Grade using Student or Subject Id
        /// </summary>
        /// <param name="StudentId">Specific Student identifier</param>
        /// <param name="SubjectId">Specific Subject identifier</param>
        /// <returns>List of grades</returns>
        public static async Task<IResult> GetGradeByStudentOrSubject(int studentId,int subjectId,
            GradeService gradeService)
        {
            var response = await gradeService.GetGradeByStudentAndSubject(studentId, subjectId);

            return response.Status ? Results.Ok(response) : Results.BadRequest(response);
        }

        /// <summary>
        /// Get Grades using Student or Subject Id
        /// </summary>
        /// <param name="StudentId">Specific Student identifier</param>
        /// <param name="SubjectId">Specific Subject identifier</param>
        /// <returns>List of grades</returns>
        public static async Task<IResult> GetGradesByStudentOrSubject([AsParameters] StudentAndSubjectQuery query,
            GradeService gradeService)
        {
            var response = await gradeService.GetGradesByStudentAndSubject(query.StudentId, query.SubjectId);

            return response.Status ? Results.Ok(response) : Results.BadRequest(response);
        }

        /// <summary>
        /// Assign a student a grade
        /// </summary>
        /// <param name="requestBody">Full grade request</param>
        /// <returns>List of grades</returns>
        public static async Task<IResult> AssignGrade([FromBody] AssignGradeRequestBody requestBody, GradeService gradeService)
        {
            var gradeDto = new GradeDto
            {
                Value = requestBody.Value,
                Note = requestBody.Note,
                SubjectId = requestBody.SubjectId,
                StudentId = requestBody.StudentId,
            };
            var response = await gradeService.AssignGrade(gradeDto);

            return response.Status ? Results.Ok(response) : Results.BadRequest(response);
        }

        /// <summary>
        /// Edit a specific grade
        /// </summary>
        /// <param name="id">Specific grade identifier</param>
        /// <param name="requestBody">Grade request</param>
        /// <returns>List of grades</returns>
        public static async Task<IResult> UpdateGrade(int id, [FromBody] EditGradeRequestBody requestBody, GradeService gradeService)
        {
            var gradeDto = new GradeDto
            {
                Value = requestBody.Value,
                Note = requestBody.Note,
            };
            var response = await gradeService.EditGrade(id, gradeDto);

            return response.Status ? Results.Ok(response) : Results.BadRequest(response);
        }
    }
}
