using  ComplaintSystem.Application;
using  ComplaintSystem.Persistence;
using  ComplaintSystem.Infrastructure;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
AddSwaggerDoc(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigurePersitenceServices(builder.Configuration);
builder.Services.AddApplication().AddInfrastructure(builder.Configuration);

// initialize firebase service

FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromJson(Environment.GetEnvironmentVariable("FIREBASE_CONFIG"))
});

//add policies for authorization

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("User", policy => policy.RequireClaim(JwtRegisteredClaimNames.Typ, "user"));
});



//Date now works with this for east african time
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Add Environment Variables
builder.Configuration.AddEnvironmentVariables();
DotNetEnv.Env.Load("../.env");

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseHttpsRedirection();
app.UseAuthentication();

app.MapControllers();

app.Run();

// Add swagger documentation
void AddSwaggerDoc(IServiceCollection services)
{
    services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Compliant System",
        });
    });
}

public partial class Program { }