using DoAn.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace DoAn.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly QuanLyBanHangContext _context;

        public SanPhamController(QuanLyBanHangContext context)
        {
            _context = context;
        }

        // GET: SanPham/DanhSach

        public IActionResult Detail(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                // In thông tin các claim của người dùng để kiểm tra
                foreach (var claim in User.Claims)
                {
                    Console.WriteLine($"{claim.Type}: {claim.Value}");
                }
            }
            else
            {
                // Xử lý khi người dùng chưa đăng nhập
                return RedirectToAction("Login", "Account");
            }
            // Lấy thông tin sản phẩm từ ID
            var product = _context.SanPhams
                .Include(p => p.KhuyenMais)
                .FirstOrDefault(p => p.IdSanPham == id);

            if (product == null)
            {
                return NotFound();
            }

            // Lấy các đánh giá từ bảng phản hồi và bao gồm KhachHang
            var reviews = _context.DanhGias
                .Where(r => r.IdSanPham == id)
                .Include(r => r.KhachHang)  // Ensure KhachHang is loaded
                .Include(r => r.SanPham)    // Also load the associated SanPham if needed
                .ToList();

            // Lấy các chương trình khuyến mãi (nếu có)
            var promotions = _context.KhuyenMais
                .Where(k => k.IdSanPham == id && k.PhanTramKhuyenMai > 0)
                .ToList();

            // Tính giá giảm nếu có khuyến mãi
            var discountPrice = product.DonGia;
            if (promotions.Any())
            {
                var promotion = promotions.First(); // Giả sử lấy chương trình khuyến mãi đầu tiên
                discountPrice = product.DonGia - (product.DonGia * promotion.PhanTramKhuyenMai / 100);
            }

            // Tạo view model
            var viewModel = new ProductDetailViewModel
            {
                Product = product,
                DiscountPrice = (decimal)discountPrice,
                Promotions = promotions,
                Reviews = reviews
            };

            return View("~/Views/SanPham/Detail.cshtml", viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> SearchAjax(string keyword, int page = 1)
        {
            const int pageSize = 12; // Số sản phẩm trên mỗi trang

            if (string.IsNullOrEmpty(keyword))
            {
                return RedirectToAction("DanhSach", new { page }); // Nếu không có từ khóa, trả về trang danh sách đầy đủ
            }

            // Tìm sản phẩm có tên chứa từ khóa
            var productsQuery = _context.SanPhams
                .Include(sp => sp.KhuyenMais)
                .Where(p => p.TenSanPham.Contains(keyword));

            // Lấy tổng số sản phẩm phù hợp với từ khóa
            var totalProducts = await productsQuery.CountAsync();

            // Lấy sản phẩm cho trang hiện tại
            var products = await productsQuery
                .Skip((page - 1) * pageSize) // Bỏ qua sản phẩm trước đó
                .Take(pageSize) // Lấy sản phẩm cho trang hiện tại
                .ToListAsync();

            // Tính số trang
            var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

            // Tạo view model
            var model = new ProductViewModel
            {
                Products = products,
                ProductTypes = await _context.LoaiSanPhams.ToListAsync(),
                Promotions = await _context.KhuyenMais.ToListAsync(),
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View("DanhSach", model); // Trả về trang danh sách với kết quả tìm kiếm và phân trang
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> SubmitReview(string reviewContent, int rating, string productId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { success = false, message = "Người dùng chưa đăng nhập." });
            }

            // Lấy tên người dùng từ User.Identity.Name
            var userName = User.Identity.Name;
            // Tìm khách hàng trong bảng KhachHang theo tên người dùng (hoặc email)
            var customer = await _context.KhachHangs
                .FirstOrDefaultAsync(k => k.TaiKhoan == userName); // Giả sử tên người dùng là email, nếu là tên khác thì thay đổi điều kiện tìm kiếm

            if (customer == null)
            {
                return Json(new { success = false, message = "Người dùng không xác định." });
            }

            // Lấy thông tin sản phẩm
            var product = await _context.SanPhams.FindAsync(productId);
            if (product == null)
            {
                return Json(new { success = false, message = "Sản phẩm không tồn tại." });
            }

            // Tạo mới đánh giá
            var review = new DanhGia
            {
                IdSanPham = productId,
                NoiDungDanhGia = reviewContent,
                Diem = rating,
                IdKhachHang = customer.IdKhachHang,  // Sử dụng ID của khách hàng
                NgayDanhGia = DateTime.Now
            };

            _context.DanhGias.Add(review);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }




        public async Task<IActionResult> DanhSach(int page = 1)
        {
            const int pageSize = 12; // Số sản phẩm trên mỗi trang

            // Lấy tổng số sản phẩm
            var totalProducts = await _context.SanPhams.CountAsync();

            // Lấy sản phẩm cho trang hiện tại
            var products = await _context.SanPhams
                .Include(sp => sp.KhuyenMais) // Bao gồm khuyến mãi nếu có
                .Skip((page - 1) * pageSize) // Bỏ qua sản phẩm trước đó
                .Take(pageSize) // Lấy sản phẩm cho trang hiện tại
                .ToListAsync();

            // Tính số trang
            var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

            // Tạo view model
            var model = new ProductViewModel
            {
                Products = products,
                ProductTypes = await _context.LoaiSanPhams.ToListAsync(),
                Promotions = await _context.KhuyenMais.ToListAsync(),
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(model);
        }


        public IActionResult FilterProducts(List<string> types, decimal? minPrice, decimal? maxPrice, string sortOrder)
        {
            var query = _context.SanPhams.Include(sp => sp.KhuyenMais).AsQueryable();

            // Lọc theo loại sản phẩm
            if (types != null && types.Any())
            {
                query = query.Where(sp => types.Contains(sp.LoaiSanPham.TenLoai));
            }

            // Lọc theo mức giá
            if (minPrice.HasValue)
            {
                query = query.Where(sp => sp.DonGia >= (double)minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                query = query.Where(sp => sp.DonGia <= (double)maxPrice.Value);
            }

            // Sắp xếp sản phẩm
            if (!string.IsNullOrEmpty(sortOrder))
            {
                if (sortOrder == "name-asc")
                {
                    query = query.OrderBy(sp => sp.TenSanPham);
                }
                else if (sortOrder == "name-desc")
                {
                    query = query.OrderByDescending(sp => sp.TenSanPham);
                }
            }

            var products = query.ToList();

            // Tạo view model và trả lại partial view
            var model = new ProductViewModel
            {
                Products = products,
                ProductTypes = _context.LoaiSanPhams.ToList(),
                Promotions = _context.KhuyenMais.ToList()
            };

            return PartialView("_ProductGrid", model);  // Trả về toàn bộ model
        }

        public async Task<IActionResult> Index()
        {
            // Lấy các dữ liệu cần thiết từ các bảng
            var productTypes = await _context.LoaiSanPhams.ToListAsync();
            var promotions = await _context.KhuyenMais.Include(km => km.SanPham).ToListAsync();
            var products = await _context.SanPhams.ToListAsync();

            // Tạo view model và gán dữ liệu vào
            var viewModel = new ProductViewModel
            {
                Products = products,
                ProductTypes = productTypes,
                Promotions = promotions
            };

            return View(viewModel);  // Truyền view model vào view
        }

    }
}
