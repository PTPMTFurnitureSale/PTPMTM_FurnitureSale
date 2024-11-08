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
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-CSRF-TOKEN";  // Đảm bảo tên của token khớp với tên trong request header
});

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
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "account",
    pattern: "{controller=Account}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "SanPham",
    pattern: "{controller=SanPham}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "Cart",
    pattern: "{controller=Cart}/{action=Index}/{id?}");


app.Run();
