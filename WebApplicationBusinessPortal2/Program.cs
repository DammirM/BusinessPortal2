using WebApplicationBusinessPortal2.Models.ConfigurationModels;
using WebApplicationBusinessPortal2.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddLogging(builder => builder.AddConsole());

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
builder.Services.AddSingleton<IHttpClientService, HttpClientService>();
builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
builder.Services.AddScoped<ILeaveRequestAdminService, LeaveRequestAdminService>();


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

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Enable attribute routing
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=LeaveRequestAdmin}/{action=AdminIndex}/{id?}");

app.Run();
