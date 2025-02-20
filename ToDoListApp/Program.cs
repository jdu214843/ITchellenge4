using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure; // Agar MySQL ulanishidan foydalanmoqchi bo'lsangiz
using ToDoListApp.Models;
 // AppDbContext joylashgan joy
// using Microsoft.EntityFrameworkCore.Sqlite; // Kerak bo'lsa

var builder = WebApplication.CreateBuilder(args);

// Konfiguratsiya qo'shish
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Agar MySQL ulanishidan foydalanmoqchi bo'lsangiz, quyidagicha:
//// builder.Services.AddDbContext<AppDbContext>(options =>
////     options.UseMySql(
////         builder.Configuration.GetConnectionString("DefaultConnection"),
////         ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

// SQLite ulanishi uchun
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SqlLiteConnection")));

var app = builder.Build();

// Muhitga qarab xatoliklar sahifasi va xavfsizlik sozlamalari
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
