using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PersonalFinanceManagement.Api;
using PersonalFinanceManagement.Api.Middlewares;
using PersonalFinanceManagement.Infra.Data.Contexts;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

AddServices(builder);
ConfigureServices(builder);

var app = builder.Build();

await ConfigureApplication(app);

app.Run();

void AddServices(WebApplicationBuilder builder)
{
    DefaultDBContext.Configure(builder.Services, builder.Configuration.GetConnectionString("Default"));
    IoCService.Configure(builder.Services);
}

void ConfigureServices(WebApplicationBuilder builder)
{
    var jwtSecret = Encoding.ASCII.GetBytes(
        builder.Configuration.GetValue<string>("Jwt:Secret")
    );

    builder.Services.AddCors();
    builder.Services.AddControllers();
    builder.Services.AddAuthentication(a =>
    {
        a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(j =>
    {
        j.RequireHttpsMetadata = false;
        j.SaveToken = true;
        j.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(jwtSecret),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(option =>
    {
        option.SwaggerDoc("v1", new OpenApiInfo { Title = "Personal Finance Management API", Version = "v1" });
        option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        option.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    });
}

async Task ConfigureApplication(WebApplication app)
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors(c => c
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    );

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();

    DefaultDBContext.InitializeDatabase(app.Services);
    await DefaultDBContext.Seed(app.Services, app.Configuration);
}