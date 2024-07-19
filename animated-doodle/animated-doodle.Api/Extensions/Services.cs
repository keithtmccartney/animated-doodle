using animated_doodle.Data;
using animated_doodle.Data.Interfaces;
using animated_doodle.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace animated_doodle.Api.Extensions;

public static class Services
{
    public static void AddCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("Any", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });
    }

    public static void AddSql(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddDbContext<SchoolContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<ICourseRepository, CourseRepository>();

        services.AddTransient<IStudentRepository, StudentRepository>();

        services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        });

        services.AddSwaggerGen();
    }
}
