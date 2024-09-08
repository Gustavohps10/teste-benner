using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using microwave_benner.Application.Mappings;
using microwave_benner.Application.Services;
using microwave_benner.Application.UseCases;
using microwave_benner.Domain.Interfaces;
using microwave_benner.Infra.Data.Context;
using microwave_benner.Infra.Data.Repositories;

namespace microwave_benner.Infra.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config) {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString("DefaultConnection"),
                    b=>b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            });

            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            /*
             * Repositories
             */
            services.AddScoped<IHeatingTaskRepository, HeatingTaskRepository>();
            services.AddScoped<IHeatingProgramRepository, HeatingProgramRepository>();

            /*
             * Services - Use Cases
             */

            //Tasks
            services.AddScoped<IStartHeatingTaskUseCase, StartHeatingTaskService>();
            services.AddScoped<IAddTimeToHeatingTaskUseCase, AddTimeToHeatingTaskService>();
            services.AddScoped<IPauseOrCancelHeatingTaskUseCase, PauseOrCancelHeatingTaskService>();
            services.AddScoped<IResumeHeatingTaskUseCase, ResumeHeatingTaskService>();
            services.AddScoped<IReadHeatingTaskByIdUseCase, ReadHeatingTaskByIdService>();

            //Heating Programs
            services.AddScoped<ICreateHeatingProgramUseCase, CreateHeatingProgramService>();
            services.AddScoped<IReadAllHeatingProgramsUseCase, ReadAllHeatingProgramsService>();
            services.AddScoped<IUpdateHeatingProgramUseCase, UpdateHeatingProgramService>();
            services.AddScoped<IDeleteHeatingProgramUseCase, DeleteHeatingProgramService>();

            return services;
        }
    }
}
