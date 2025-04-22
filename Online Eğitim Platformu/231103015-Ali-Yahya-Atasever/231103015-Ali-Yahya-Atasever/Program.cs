using _231103015_Ali_Yahya_Atasever.DAL;
using Microsoft.EntityFrameworkCore; // DbContext ve UseSqlServer için

var builder = WebApplication.CreateBuilder(args);


// Authentication için gerekli olan servisler




builder.Services.AddHttpContextAccessor();  // IHttpContextAccessor



// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EgitimContext>();

builder.Services.AddAuthentication()
       .AddCookie(options =>
       {
           options.LoginPath = "/Login/Index";
           options.LogoutPath = "/Login/Logout";
           options.AccessDeniedPath = "/Login/Index"; // Rol yetmezse yönlendirme
       });

var app = builder.Build();///////////////////ÖENMELÝ 

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Kimlik doðrulama middleware
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
