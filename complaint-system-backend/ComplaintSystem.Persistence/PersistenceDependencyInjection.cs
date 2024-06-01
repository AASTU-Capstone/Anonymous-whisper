using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ComplaintSystem.Application.Persistence.Contracts;
using ComplaintSystem.Persistence.Repositories;

namespace ComplaintSystem.Persistence;
public static class PersistenceDependencyInjection
{
    public static IServiceCollection ConfigurePersitenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        DotNetEnv.Env.Load("../.env");
        string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        services.AddDbContext<ComplaintSystemAppDbContext>(opt => opt.UseNpgsql(connectionString));
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOtpRepository, OtpRepository>();
        services.AddScoped<IComplaintRepository, ComplaintRepository>();
        services.AddScoped<IComplaintLogRepository, ComplaintLogRepository>();
        services.AddScoped<IManagerRepository, ManagerRepository>();
        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<ISubordinateRepository, SubordinateRepository>();
        services.AddScoped<ICorruptionTrendRepository, CorruptionTrendRepository>();

        return services;
    }
}
