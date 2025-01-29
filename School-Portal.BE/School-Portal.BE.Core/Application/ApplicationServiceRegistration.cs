using Microsoft.Extensions.DependencyInjection;
using School_Portal.BE.Core.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Portal.BE.Core.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddAApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<GradeService>();
            services.AddScoped<SubjectService>();
            services.AddScoped<StudentService>();

            return services;
        }
    }
}
