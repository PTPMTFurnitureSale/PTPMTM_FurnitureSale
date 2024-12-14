using DoAn.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

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
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var userName = User.Identity.Name; // Lấy tên người dùng từ Claims sau khi đăng nhập

            // Lấy thông tin khách hàng từ bảng KhachHangs
            var khachHang = await _context.KhachHangs
                .FirstOrDefaultAsync(k => k.TaiKhoan == userName);

            if (khachHang == null)
            {
                return RedirectToAction("Login"); // Chuyển hướng về trang đăng nhập nếu không tìm thấy khách hàng
            }

            // Lọc hóa đơn của khách hàng từ bảng HoaDons
            var orders = await _context.HoaDons
                .Where(h => h.IdKhachHang == khachHang.IdKhachHang)  // Lọc theo IdKhachHang
                .Include(h => h.ChiTietHoaDons) // Nạp chi tiết hóa đơn
                .ThenInclude(ct => ct.SanPham) // Nạp sản phẩm trong chi tiết hóa đơn
                .ToListAsync();

            // Kiểm tra và chuyển kiểu dữ liệu nếu cần
            foreach (var order in orders)
            {
                // Chuyển TongTien từ kiểu floatsang float hoặc decimal
                order.TongTien = (double)order.TongTien;  // Sử dụng Convert.ToSingle() nếu muốn sử dụng float

                // In giá trị để kiểm tra
                Debug.WriteLine($"Order ID: {order.IdHoaDon}, Total Amount: {order.TongTien}");
            }

            // Truyền danh sách hóa đơn vào ViewBag
            ViewBag.Orders = orders;

            return View(khachHang); // Trả về view và hiển thị thông tin tài khoản của người dùng
        }



        // Đổi mật khẩu
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                ModelState.AddModelError("", "Vui lòng điền đầy đủ thông tin.");
                return RedirectToAction("Profile");
            }

            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("confirmPassword", "Mật khẩu xác nhận không khớp.");
                return RedirectToAction("Profile");
            }

            var userName = User.Identity.Name;
            var user = await _context.KhachHangs.FirstOrDefaultAsync(k => k.TaiKhoan == userName);

            if (user == null || user.MatKhau != oldPassword)
            {
                ModelState.AddModelError("oldPassword", "Mật khẩu cũ không đúng.");
                return RedirectToAction("Profile");
            }

            user.MatKhau = newPassword;
            _context.KhachHangs.Update(user);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";
            return RedirectToAction("Profile");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAddress(string province, string district, string specificAddress)
        {
            var userName = User.Identity.Name;
            var user = await _context.KhachHangs.FirstOrDefaultAsync(k => k.TaiKhoan == userName);

            if (user == null)
            {
                return RedirectToAction("Login");
            }

            // Gộp địa chỉ lại thành chuỗi
            var newAddress = $"{province}, {district}, {specificAddress}";

            // Cập nhật địa chỉ giao hàng
            user.DiaChiGiaoHang = newAddress;

            _context.KhachHangs.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Profile");
        }




    }
}
