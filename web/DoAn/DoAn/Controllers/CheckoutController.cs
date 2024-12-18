using DoAn.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoAn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Diagnostics;
namespace DoAn.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly QuanLyBanHangContext _context;

        public CheckoutController(QuanLyBanHangContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            // Lấy thông tin khách hàng đăng nhập
            var userName = User.Identity.Name;
            var customer = await _context.KhachHangs.FirstOrDefaultAsync(c => c.TaiKhoan == userName);
            if (customer == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Lấy giỏ hàng từ session
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");
            if (cartItems == null || !cartItems.Any())
            {
                return RedirectToAction("DanhSach", "SanPham");
            }

            // Tính tổng tiền
            var totalAmount = cartItems.Sum(item => item.Price * item.Quantity);  // Nếu Price là float hoặc decimal


            // Tạo view model
            var viewModel = new CheckoutViewModel
            {
                Customer = customer,
                CartItems = cartItems,
                TotalAmount = totalAmount,
                PaymentMethod = 1
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(CheckoutViewModel model)
        {
            model.CartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");
            if (model.CartItems == null || !model.CartItems.Any())
            {
                // Nếu giỏ hàng trống hoặc null, chuyển hướng về trang sản phẩm hoặc trang giỏ hàng
                return RedirectToAction("DanhSach", "SanPham");
            }

            if (model.Customer == null || string.IsNullOrEmpty(model.Customer.IdKhachHang))
            {
                // Tải lại thông tin khách hàng từ cơ sở dữ liệu nếu thiếu
                var userName = User.Identity.Name;
                var customer = await _context.KhachHangs.FirstOrDefaultAsync(c => c.TaiKhoan == userName);
                if (customer == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                model.Customer = customer;
            }

            // Lưu hóa đơn và chi tiết hóa đơn vào database
            var order = new HoaDon
            {
                IdHoaDon = Guid.NewGuid().ToString(),
                IdKhachHang = model.Customer.IdKhachHang,
                NgayDat = DateTime.Now,
                TinhTrangThanhToan = 0,
                PhuongThucThanhToan = model.PaymentMethod,
                TongTien = model.TotalAmount  // Đảm bảo kiểu là float
            };

            
            _context.HoaDons.Add(order);

            foreach (var item in model.CartItems)
            {
                var orderDetail = new ChiTietHoaDon
                {
                    IdHoaDon = order.IdHoaDon,
                    IdSanPham = item.ProductId,
                    SoLuong = (int)item.Quantity,
                    DonGia = item.Price,  // Đảm bảo kiểu là float
                    ThanhTien =item.Price * item.Quantity // Đảm bảo kiểu là float
                };
                _context.ChiTietHoaDons.Add(orderDetail);
            }

            await _context.SaveChangesAsync();
            HttpContext.Session.Remove("Cart"); // Xóa giỏ hàng sau khi đặt hàng
            return RedirectToAction("OrderConfirmation", "Checkout");
        }

        public async Task<IActionResult> OrderConfirmation()
        {
            var userName = User.Identity.Name;
            var customer = await _context.KhachHangs.FirstOrDefaultAsync(c => c.TaiKhoan == userName);

            if (customer == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Lấy hóa đơn của khách hàng mới nhất
            var order = await _context.HoaDons
                                       .Where(o => o.IdKhachHang == customer.IdKhachHang)
                                       .OrderByDescending(o => o.NgayDat)
                                       .FirstOrDefaultAsync();

            if (order == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Tạo CheckoutViewModel
            var viewModel = new CheckoutViewModel
            {
                Customer = customer,
                CartItems = new List<CartItem>(),  // Nếu cần lấy giỏ hàng cho đơn hàng
                TotalAmount = (float)order.TongTien,      // Gán tổng tiền từ hóa đơn
                PaymentMethod = order.PhuongThucThanhToan,
            };

            // Truyền model vào view
            return View(viewModel);
        }



    }
}
