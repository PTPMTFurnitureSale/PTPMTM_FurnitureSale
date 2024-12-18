using DoAn.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using Microsoft.ML;
using Microsoft.ML.Trainers;
using Microsoft.ML.Data;
namespace DoAn.Controllers
{

    public class SanPhamController : Controller
    {
        private readonly KMeansRecommendationService _kmeansRecommendationService;
        private readonly QuanLyBanHangContext _context;
        private readonly RecommendationService _recommendationService;
        private readonly UserActivityService _userActivityService; // Thêm UserActivityService vào controller
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;
        // Constructor Injection của các dịch vụ
        public SanPhamController(QuanLyBanHangContext context,
                         RecommendationService recommendationService,
                         UserActivityService userActivityService,
                         KMeansRecommendationService kmeansRecommendationService)
        {
            _context = context;
            _recommendationService = recommendationService;
            _userActivityService = userActivityService;
            _kmeansRecommendationService = kmeansRecommendationService;
        }
        public IActionResult Detail(string id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var product = _context.SanPhams
                .Include(p => p.KhuyenMais)
                .FirstOrDefault(p => p.IdSanPham == id);

            if (product == null)
            {
                return NotFound();
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            // Ghi nhận hoạt động xem sản phẩm
            if (userId != null)
            {
                _userActivityService.AddUserActivity(userId, id, "view");
            }

            // Gọi dịch vụ để nhận danh sách sản phẩm mua kèm
            var recommendedProductIds = _kmeansRecommendationService.RecommendProducts(id);
            var recommendedProducts = _context.SanPhams
                .Where(p => recommendedProductIds.Contains(p.IdSanPham))
                .ToList();

            var viewModel = new ProductDetailViewModel
            {
                Product = product,
                DiscountPrice = product.DonGia,
                Promotions = _context.KhuyenMais.Where(k => k.IdSanPham == id).ToList(),
                Reviews = _context.DanhGias.Include(r => r.KhachHang).Where(r => r.IdSanPham == id).ToList(),
                NewArrivals = recommendedProducts
            };

            return View("~/Views/SanPham/Detail.cshtml", viewModel);
        }

        // GET: SanPham/Detail
        //    public IActionResult Detail(string id)
        //    {
        //        if (!User.Identity.IsAuthenticated)
        //        {
        //            return RedirectToAction("Login", "Account");
        //        }

        //        var product = _context.SanPhams
        //            .Include(p => p.KhuyenMais)
        //            .FirstOrDefault(p => p.IdSanPham == id);

        //        if (product == null)
        //        {
        //            return NotFound();
        //        }

        //        var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

        //        // Ghi nhận hoạt động xem sản phẩm
        //        if (userId != null)
        //        {
        //            _userActivityService.AddUserActivity(userId, id, "view");
        //        }

        //        // Gợi ý sản phẩm cùng thể loại
        //        var recommendedProducts = _context.SanPhams
        //            .Where(p => p.IdLoai == product.IdLoai && p.IdSanPham != product.IdSanPham) // Cùng thể loại, khác sản phẩm hiện tại
        //            .Take(4)
        //            .ToList();

        //        // Nếu không đủ, bổ sung sản phẩm gần giá
        //        if (recommendedProducts.Count < 4)
        //        {
        //            // Lấy toàn bộ sản phẩm từ database và chuyển sang client-side
        //            var additionalProducts = _context.SanPhams
        //                .AsEnumerable() // Chuyển toàn bộ dữ liệu sang bộ nhớ
        //                .Where(p => !recommendedProducts.Any(r => r.IdSanPham == p.IdSanPham) && p.IdSanPham != product.IdSanPham)
        //                .OrderBy(p => Math.Abs(p.DonGia - product.DonGia)) // Gần giá
        //                .Take(4 - recommendedProducts.Count)
        //                .ToList();

        //            recommendedProducts.AddRange(additionalProducts);
        //        }


        //        // Nếu vẫn không đủ, bổ sung sản phẩm ngẫu nhiên
        //        if (recommendedProducts.Count < 4)
        //        {
        //            var randomProducts = _context.SanPhams
        //                .Where(p => !recommendedProducts.Any(r => r.IdSanPham == p.IdSanPham) && p.IdSanPham != product.IdSanPham)
        //                .OrderBy(_ => Guid.NewGuid()) // Random sản phẩm
        //                .Take(4 - recommendedProducts.Count)
        //                .ToList();
        //            recommendedProducts.AddRange(randomProducts);
        //        }

        //        var viewModel = new ProductDetailViewModel
        //        {
        //            Product = product,
        //            DiscountPrice = product.DonGia,
        //            Promotions = _context.KhuyenMais.Where(k => k.IdSanPham == id).ToList(),
        //            Reviews = _context.DanhGias.Include(r => r.KhachHang).Where(r => r.IdSanPham == id).ToList(),
        //            NewArrivals = recommendedProducts.Distinct().Take(4).ToList() // Đảm bảo không trùng lặp, giới hạn 4 sản phẩm
        //        };

        //        return View("~/Views/SanPham/Detail.cshtml", viewModel);
        //    }



        //    // Lấy các sản phẩm gợi ý từ hoạt động của người dùng trong bộ nhớ
        //    public List<string> GetRecommendations(string productId)
        //    {
        //        // Đọc dữ liệu từ file CSV
        //        IDataView dataView = _mlContext.Data.LoadFromTextFile<ProductPurchase>("", hasHeader: true, separatorChar: ',');

        //        // Xây dựng pipeline huấn luyện
        //        var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("IdSanPham")
        //            .Append(_mlContext.Transforms.Conversion.MapValueToKey("IdSanPhamKem"))
        //            .Append(_mlContext.Recommendation().Trainers.MatrixFactorization(
        //                new Microsoft.ML.Trainers.MatrixFactorizationTrainer.Options
        //                {
        //                    MatrixColumnIndexColumnName = "IdSanPham",
        //                    MatrixRowIndexColumnName = "IdSanPhamKem",
        //                    LabelColumnName = "Rating",
        //                    NumberOfIterations = 20,
        //                    ApproximationRank = 100
        //                }))
        //            .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedIdSanPhamKem", "IdSanPhamKem"));

        //        // Huấn luyện mô hình
        //        var model = pipeline.Fit(dataView);

        //        // Tạo prediction engine
        //        var predictionEngine = _mlContext.Model.CreatePredictionEngine<ProductPurchase, ProductPrediction>(model);

        //        // Lấy danh sách sản phẩm
        //        var allProducts = dataView.GetColumn<string>("IdSanPhamKem").Distinct().ToList();

        //        var recommendations = new List<(string ProductId, float Score)>();

        //        foreach (var otherProductId in allProducts)
        //        {
        //            if (otherProductId != productId) // Không tự gợi ý chính nó
        //            {
        //                var prediction = predictionEngine.Predict(new ProductPurchase
        //                {
        //                    IdSanPham = productId,
        //                    IdSanPhamKem = otherProductId
        //                });

        //                recommendations.Add((otherProductId, prediction.Score));
        //            }
        //        }

        //        // Sắp xếp theo điểm số giảm dần và trả về 4 sản phẩm gợi ý
        //        return recommendations
        //            .OrderByDescending(r => r.Score)
        //            .Take(4)
        //            .Select(r => r.ProductId)
        //            .ToList();
        //    }
        //}

        // Hành động tìm kiếm (AJAX)
        // Hành động tìm kiếm (AJAX)
        [HttpGet]
        public async Task<IActionResult> Danhsach(string keyword, int page = 1)
        {
            const int pageSize = 12;

            // Query danh sách sản phẩm
            var productsQuery = _context.SanPhams
                .Include(sp => sp.KhuyenMais)
                .AsQueryable();

            // Lọc theo từ khóa nếu có
            if (!string.IsNullOrEmpty(keyword))
            {
                productsQuery = productsQuery.Where(p => p.TenSanPham.Contains(keyword));
            }

            // Tổng số sản phẩm
            var totalProducts = await productsQuery.CountAsync();

            // Lấy sản phẩm của trang hiện tại
            var products = await productsQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Tính số trang
            var totalPages = (int)Math.Ceiling(totalProducts / (float)pageSize);

            // Tạo ViewModel
            var model = new ProductViewModel
            {
                Products = products,
                ProductTypes = await _context.LoaiSanPhams.ToListAsync(),
                Promotions = await _context.KhuyenMais.ToListAsync(),
                CurrentPage = page,
                TotalPages = totalPages
            };

            // Nếu yêu cầu là AJAX, trả về PartialView
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_ProductGrid", model);
            }

            // Trả về View chính nếu không phải AJAX
            return View(model);
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

            return RedirectToAction("Detail", "SanPham", new { id = productId });
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
            var totalPages = (int)Math.Ceiling(totalProducts / (float)pageSize);

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

        [HttpPost]
        public IActionResult FilterProducts(List<string> types, double? minPrice, double? maxPrice, string sortOrder, int page = 1, int pageSize = 10)
        {
            // Log các giá trị nhận được từ request
            Debug.WriteLine("Types: " + (types != null ? string.Join(", ", types) : "null"));
            Debug.WriteLine("Min Price: " + (minPrice.HasValue ? minPrice.Value.ToString() : "null"));
            Debug.WriteLine("Max Price: " + (maxPrice.HasValue ? maxPrice.Value.ToString() : "null"));
            Debug.WriteLine("Sort Order: " + sortOrder);
            Debug.WriteLine("Page: " + page);
            Debug.WriteLine("Page Size: " + pageSize);

            var query = _context.SanPhams.Include(sp => sp.KhuyenMais).AsQueryable();

            // Lọc theo loại sản phẩm
            if (types != null && types.Any())
            {
                query = query.Where(sp => types.Contains(sp.LoaiSanPham.TenLoai));
                Debug.WriteLine("Filter by types applied");
            }

            // Lọc theo mức giá
            if (minPrice.HasValue)
            {
                query = query.Where(sp => sp.DonGia >= (float)minPrice.Value);
                Debug.WriteLine("Filter by minPrice applied");
            }
            if (maxPrice.HasValue)
            {
                query = query.Where(sp => sp.DonGia <= (float)maxPrice.Value);
                Debug.WriteLine("Filter by maxPrice applied");
            }

            // Sắp xếp sản phẩm
            if (!string.IsNullOrEmpty(sortOrder))
            {
                if (sortOrder == "name-asc")
                {
                    query = query.OrderBy(sp => sp.TenSanPham);
                    Debug.WriteLine("Sorting by name ascending");
                }
                else if (sortOrder == "name-desc")
                {
                    query = query.OrderByDescending(sp => sp.TenSanPham);
                    Debug.WriteLine("Sorting by name descending");
                }
            }

            // Tổng số sản phẩm
            var totalProducts = query.Count();

            // Chia trang
            var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);
            var products = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            Debug.WriteLine("Total products retrieved: " + products.Count);

            // Tạo view model và trả lại partial view
            var model = new ProductViewModel
            {
                Products = products,
                ProductTypes = _context.LoaiSanPhams.ToList(),
                Promotions = _context.KhuyenMais.ToList(),
                CurrentPage = page,
                TotalPages = totalPages
            };

            return PartialView("_ProductGrid", model);
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
