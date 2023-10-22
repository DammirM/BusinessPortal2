using BusinessPortal2.Data;
using BusinessPortal2.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace BusinessPortal2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            builder.Services.AddScoped<IPersonalRepo, PersonalRepo>();
            builder.Services.AddScoped<ILeaveTypeRepo, LeaveTypeRepo>();
            builder.Services.AddScoped<ILeaveRequestRepo, LeaveRequestRepo>();
            builder.Services.AddScoped<LeaveRequestAdminRepo, LeaveRequestAdminRepo>();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddCookie(x =>
            {
                x.Cookie.Name = "AuthToken";
            }).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                            builder.Configuration.GetSection("Appsettings:Token").Value)),
                        ValidateIssuer = true,
                        ValidIssuer = "YourIssuer",   
                        ValidateAudience = true,
                        ValidAudience = "YourAudience", 
                        RequireExpirationTime = true,
                        ClockSkew = TimeSpan.Zero // disables the default clock skew
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["AuthToken"];
                            return Task.CompletedTask;
                        }
                    };
                });

            // Service for Mapping
            builder.Services.AddAutoMapper(typeof(MappingConfig));

            // Service for Validation
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();

            builder.Services.AddDbContext<PersonaldataContext>(options => options
            .UseSqlServer(builder.Configuration.GetConnectionString("ConnectionFilip")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}