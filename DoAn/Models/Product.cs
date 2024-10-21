using System.ComponentModel.DataAnnotations;

namespace DoAn.Models
{
    public class Product
    {
        [Key]
        public string ProductId { get; set; } // Khóa chính
        public string ProductName { get; set; } // Tên sản phẩm
        public decimal Price { get; set; } // Giá sản phẩm
        public string CategoryId { get; set; } // ID danh mục
        public string Description { get; set; } // Mô tả sản phẩm
        public string ImageUrl { get; set; } // URL hình ảnh
        public int Stock { get; set; } // Số lượng tồn kho
        public string Material { get; set; } // Chất liệu sản phẩm
        public string Dimensions { get; set; } // Kích thước sản phẩm
        public int WarrantyPeriod { get; set; } // Thời gian bảo hành (tháng)

        public Category Category { get; set; } 
    }
}
