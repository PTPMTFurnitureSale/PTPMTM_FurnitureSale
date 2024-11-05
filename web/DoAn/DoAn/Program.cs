using DoAn.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký DbContext với DI container
builder.Services.AddDbContext<QuanLyBanHangContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultContext"))); // Thay đổi tên connection string

// Thêm dịch vụ xác thực cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Đường dẫn tới trang đăng nhập
        options.LogoutPath = "/Account/Logout"; // Đường dẫn tới trang đăng xuất
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Sử dụng middleware xác thực
app.UseAuthentication(); // Thêm dòng này để sử dụng xác thực

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "account",
    pattern: "{controller=Account}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
