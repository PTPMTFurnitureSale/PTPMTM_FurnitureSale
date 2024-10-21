using System.Diagnostics;
using DoAn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DoAn.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EGAFurnitureContext _context;

        public HomeController(EGAFurnitureContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                // Truy xuất dữ liệu từ DbContext
                var products = _context.Products.ToList();

                // Trả về view với danh sách sản phẩm
                ViewData["DbConnectionStatus"] = "Kết nối cơ sở dữ liệu thành công.";
                return View(products);
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "Lỗi kết nối SQL Server: {Message}", sqlEx.Message);
                ViewData["DbConnectionStatus"] = "Lỗi kết nối SQL Server: " + sqlEx.Message;
                return View("Index", new List<Product>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi không xác định: {Message}", ex.Message);
                ViewData["DbConnectionStatus"] = "Lỗi không xác định khi kết nối cơ sở dữ liệu: " + ex.Message;
                return View("Index", new List<Product>());
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorViewModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(errorViewModel);
        }
    }
}
