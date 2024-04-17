using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using  ComplaintSystem.Application.Persistence.Contracts;
using  ComplaintSystem.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  ComplaintSystem.Persistence;

namespace  ComplaintSystem.Persistence;
public static class PersistenceDependencyInjection
{
    public static IServiceCollection ConfigurePersitenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        DotNetEnv.Env.Load("../.env");
        string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        services.AddDbContext< ComplaintSystemAppDbContext>(opt => opt.UseNpgsql(connectionString));
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOtpRepository, OtpRepository>();



        return services;
    }
}
