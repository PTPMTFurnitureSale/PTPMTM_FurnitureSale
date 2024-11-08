using System.Diagnostics;
using DoAn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DoAn.Controllers
{
    public class HomeController : Controller
    {
        private readonly QuanLyBanHangContext _context;

        public HomeController(QuanLyBanHangContext context)
        {
            _context = context;
        }

        // Phương thức Index trả về view
        public IActionResult Index()
        {
            var categories = _context.LoaiSanPhams.ToList();

            // Lấy tất cả sản phẩm trong cơ sở dữ liệu, bao gồm thông tin khuyến mãi và loại sản phẩm
            var products = _context.SanPhams.Include(p => p.KhuyenMais).Include(p => p.LoaiSanPham).ToList();

            // Lấy tất cả khuyến mãi
            var promotions = _context.KhuyenMais.Where(km => km.PhanTramKhuyenMai > 0).ToList();

            // Lọc các sản phẩm có khuyến mãi và sắp xếp theo phần trăm giảm giá (từ cao đến thấp)
            var productsWithPromotions = products
                .Where(p => p.KhuyenMais != null && p.KhuyenMais.Any(pm => pm.PhanTramKhuyenMai > 0))
                .SelectMany(p => p.KhuyenMais, (product, promotion) => new { product, promotion })
                .Where(x => x.promotion.PhanTramKhuyenMai > 0)
                .OrderByDescending(x => x.promotion.PhanTramKhuyenMai)
                .Take(8)  // Lấy 8 sản phẩm có giảm giá
                .ToList();

            // Lọc các sản phẩm mới (ví dụ, sản phẩm có ngày tạo gần đây)
            var newArrivals = products.OrderByDescending(p => p.TenSanPham).Take(8).ToList();

            // Lọc các sản phẩm thuộc danh mục "Nội Thất Phòng Bếp" (giả sử có loại sản phẩm này trong cơ sở dữ liệu)
            var kitchenFurniture = products.Where(p => p.LoaiSanPham.TenLoai == "Nhà bếp").Take(8).ToList();

            // Trả về View với dữ liệu loại sản phẩm, sản phẩm khuyến mãi, sản phẩm mới và sản phẩm nội thất phòng bếp
            var viewModel = new ProductViewModel
            {
                ProductTypes = categories,
                ProductsWithPromotions = productsWithPromotions.Select(x => x.product).ToList(),
                Promotions = promotions,
                NewArrivals = newArrivals,
                KitchenFurniture = kitchenFurniture
            };

            return View(viewModel);
        }


        // Phương thức Privacy trả về view
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
