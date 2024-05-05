using  ComplaintSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using  ComplaintSystem.Application.Persistence.Contracts.Auth;
using  ComplaintSystem.Application.Persistence.Contracts.Common;
using  ComplaintSystem.Application.Persistence.Contracts.Cloudinary;
using  ComplaintSystem.Infrastructure.Authentication;
using  ComplaintSystem.Infrastructure.Mail;
using  ComplaintSystem.Infrastructure.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplaintSystem.Application.Persistence.Contracts.APIs;

namespace  ComplaintSystem.Infrastructure;  
public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddAuth(configuration);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        //services.AddScoped<IInvestorRepository, InvetstorRepository>();
        //services.AddScoped<IStartupRepository, StartupRepository>();

        return services;
    }
    public static IServiceCollection AddAuth(this IServiceCollection services,ConfigurationManager configuration)
    {
 
        var jwtSettings = new Jwtsettings();
        var openAi = new OpenAi();


        configuration.GetSection(Jwtsettings.SectionName).Bind(jwtSettings);
        configuration.GetSection(OpenAi.SectionName).Bind(openAi);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton(Options.Create(openAi));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IStringValidator, StringValidator>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddScoped<IJwtTokenValidation, JwtTokenValidation>();
        services.AddSingleton<IOpenAiServices, OpenAiService>();
        services.AddSingleton<IPdfReaderService, PdfReaderService>();
        services.AddSingleton<IImaggaService, ImaggaService>();

        services.Configure<CloudinarySetting>(configuration.GetSection(CloudinarySetting.SectionName));
        services.AddTransient<ICloudinaryService, CloudinaryService>();


        services.AddAuthentication(defaultScheme:JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Environment.GetEnvironmentVariable("Issuer"),
            ValidAudience = Environment.GetEnvironmentVariable("Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET"))),
        });

        return services;
    }
}
