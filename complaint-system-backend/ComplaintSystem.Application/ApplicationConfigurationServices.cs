using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace  ComplaintSystem.Application;
public static class ApplicationConfigurationServices
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(config=>config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
}
