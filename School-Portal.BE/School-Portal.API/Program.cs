
using School_Portal.API.Endpoints;
using School_Portal.API.Middleware;
using School_Portal.BE.Core.Application;
using School_Portal.BE.Core.Infrastructure;
using System.Diagnostics;

namespace School_Portal.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddAApplicationServices();
            builder.Services.AddApiInfrastructureServices();
            builder.Services.AddSchoolDbContext(configuration);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                );
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                context.Response?.Headers?.Append("RequestId", Activity.Current?.TraceId.ToString());
                await next();
            });

            app.MapGroup("api/v1")
            .RegisterGradesRoutes()
            .RegisterSubjectRoutes()
            .RegisterStudentRoutes();

            app.UseMiddleware<ExceptionMiddleware>();
            app.Run();
        }
    }
}
