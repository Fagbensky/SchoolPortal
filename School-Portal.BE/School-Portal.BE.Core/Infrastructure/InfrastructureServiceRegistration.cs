using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using School_Portal.BE.Core.Application.Interfaces;
using School_Portal.BE.Core.Infrastructure.Persistence;
using School_Portal.BE.Core.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Portal.BE.Core.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddApiInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IGradeRepository, GradeRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();

            return services;
        }

        public static IServiceCollection AddSchoolDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SchoolContext>(option =>
              option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
