using DoAn.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DoAn.Controllers
{
    public class AccountController : Controller
    {
        private readonly QuanLyBanHangContext _context;

        public AccountController(QuanLyBanHangContext context)
        {
            _context = context;
        }

        // GET: /Account/Register
        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(KhachHang khachHang, string confirmPassword)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem tài khoản đã tồn tại hay chưa
                var existingUser = await _context.KhachHangs
                    .FirstOrDefaultAsync(k => k.TaiKhoan == khachHang.TaiKhoan);

                if (existingUser != null)
                {
                    ModelState.AddModelError("TaiKhoan", "Tài khoản đã tồn tại.");
                }

                // Kiểm tra mật khẩu xác nhận
                if (khachHang.MatKhau != confirmPassword)
                {
                    ModelState.AddModelError("confirmPassword", "Mật khẩu xác nhận không đúng.");
                }

                // Nếu có bất kỳ lỗi nào trong ModelState, trả về view với lỗi
                if (!ModelState.IsValid)
                {
                    return View(khachHang);
                }

                // Gán IdKhachHang tự động
                khachHang.IdKhachHang = Guid.NewGuid().ToString();

                // Thêm khách hàng vào cơ sở dữ liệu
                _context.KhachHangs.Add(khachHang);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";
                return RedirectToAction("Login");
            }

            return View(khachHang);
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string taiKhoan, string matKhau)
        {
            if (string.IsNullOrEmpty(taiKhoan))
            {
                ModelState.AddModelError("taiKhoan", "Tên tài khoản không được để trống.");
            }
            if (string.IsNullOrEmpty(matKhau))
            {
                ModelState.AddModelError("matKhau", "Mật khẩu không được để trống.");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            // Kiểm tra thông tin đăng nhập
            var user = await _context.KhachHangs
                .FirstOrDefaultAsync(k => k.TaiKhoan == taiKhoan && k.MatKhau == matKhau);

            if (user == null)
            {
                ModelState.AddModelError("matKhau", "Tên tài khoản hoặc mật khẩu không đúng.");
                return View();
            }

            // Đăng nhập thành công
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.TaiKhoan) };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            TempData["SuccessMessage"] = "Đăng nhập thành công!";
            return View();
        }



        // GET: /Account/Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}
