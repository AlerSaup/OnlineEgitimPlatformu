using _231103015_Ali_Yahya_Atasever.DAL;
using Microsoft.EntityFrameworkCore; // DbContext ve UseSqlServer i�in

var builder = WebApplication.CreateBuilder(args);


// Authentication i�in gerekli olan servisler




builder.Services.AddHttpContextAccessor();  // IHttpContextAccessor



// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EgitimContext>();

builder.Services.AddAuthentication()
       .AddCookie(options =>
       {
           options.LoginPath = "/Login/Index";
           options.LogoutPath = "/Login/Logout";
           options.AccessDeniedPath = "/Login/Index"; // Rol yetmezse y�nlendirme
       });

var app = builder.Build();///////////////////�ENMEL� 

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Kimlik do�rulama middleware
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
