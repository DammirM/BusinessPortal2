using BusinessPortal2.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;
using WebApplicationBusinessPortal2.Models.ConfigurationModels;
using WebApplicationBusinessPortal2.Services;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddTransient<IEmailSender, EmailSenderService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddLogging(builder => builder.AddConsole());

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
builder.Services.AddSingleton<IHttpClientService, HttpClientService>();
builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
builder.Services.AddScoped<ILeaveRequestAdminService, LeaveRequestAdminService>();
builder.Services.AddScoped<IGetSelectListService, GetSelectListService>();
builder.Services.AddScoped<IAccessService, AccessService>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "https://localhost:7259//Access/Login"; 
});
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
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  // Add this
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
