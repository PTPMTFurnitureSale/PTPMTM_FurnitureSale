using DoAn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
namespace DoAn.Controllers
{
    public class CartController : Controller
    {
        private readonly QuanLyBanHangContext _context;

        public CartController(QuanLyBanHangContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            // Lấy dữ liệu giỏ hàng từ session (nếu có)
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            return View(cart);
        }
        [HttpPost]
        public IActionResult UpdateQuantity(string productId, int change)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);

            if (item != null)
            {
                item.Quantity += change;
                if (item.Quantity <= 0)
                {
                    cart.Remove(item);
                }
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult RemoveFromCart(string productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            var itemToRemove = cart.FirstOrDefault(c => c.ProductId == productId);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult AddToCart(string productId, int quantity)
        {
            var product = _context.SanPhams.FirstOrDefault(p => p.IdSanPham == productId);
            if (product == null)
            {
                return NotFound();
            }

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            var existingItem = cart.FirstOrDefault(c => c.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = product.TenSanPham,
                    Image = product.Hinh,
                    Price = (decimal)product.DonGia,
                    Quantity = quantity
                });
            }

            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("Index");
        }
    }
}
