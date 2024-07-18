using animated_doodle.Data;
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
}
