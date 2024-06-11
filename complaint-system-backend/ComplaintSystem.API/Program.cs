using ComplaintSystem.Application;
using ComplaintSystem.Persistence;
using ComplaintSystem.Infrastructure;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using ComplaintSystem.Infrastructure.services;
using System.Net.WebSockets;
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

// Add Environment Variables
builder.Configuration.AddEnvironmentVariables();
DotNetEnv.Env.Load("../.env");

// initialize firebase service
if (FirebaseApp.DefaultInstance == null)
{
    FirebaseApp.Create(new AppOptions()
    {
        Credential = GoogleCredential.FromJson(Environment.GetEnvironmentVariable("FIREBASE_CONFIG"))
    });
}


// set cors policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "frontend",
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                      });
});

//add policies for authorization


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("User", policy => policy.RequireClaim(JwtRegisteredClaimNames.Typ, "user"));
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim(JwtRegisteredClaimNames.Typ, "admin"));
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Manager", policy => policy.RequireClaim(JwtRegisteredClaimNames.Typ, "manager"));
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Subordinate", policy => policy.RequireClaim(JwtRegisteredClaimNames.Typ, "subordinate"));
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Worker", policy => policy.RequireClaim(JwtRegisteredClaimNames.Typ, "subordinate", "manager", "admin"));
});

//for user and admin
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Usin", policy => policy.RequireClaim(JwtRegisteredClaimNames.Typ, "user", "admin"));
});

//Date now works with this for east african time
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);



var app = builder.Build();
// var app = builder.Build();

app.UseRouting();
app.UseWebSockets();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("frontend");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.Map("/notification", async context =>
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var socket = await context.WebSockets.AcceptWebSocketAsync();
                var userId = context.Request.Query["userId"].ToString();
                NotificationService.AddSocket(userId, socket);

                await Echo(socket, userId);
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        });
    });
    // }

async Task Echo(WebSocket socket, string userId)
{
    var buffer = new byte[1024 * 4];
    WebSocketReceiveResult result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
    while (!result.CloseStatus.HasValue)
    {
        result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
    }
    await NotificationService.RemoveSocket(userId);
}
// }
// app.MapControllers();


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
