using BusinessPortal2.Data;
using BusinessPortal2.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BusinessPortal2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IPersonalRepo, PersonalRepo>();
            builder.Services.AddScoped<ILeaveTypeRepo, LeaveTypeRepo>();
            builder.Services.AddScoped<ILeaveRequestRepo, LeaveRequestRepo>();
            builder.Services.AddScoped<LeaveRequestAdminRepo, LeaveRequestAdminRepo>();

            // Service for Mapping
            builder.Services.AddAutoMapper(typeof(MappingConfig));

            // Service for Validation
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();

            builder.Services.AddDbContext<PersonaldataContext>(options => options
            .UseSqlServer(builder.Configuration.GetConnectionString("ConnectionMaxLaptop")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}