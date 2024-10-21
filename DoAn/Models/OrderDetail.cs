using System.ComponentModel.DataAnnotations;

namespace DoAn.Models
{
    public class OrderDetail
    {
        [Key]
        public string OrderDetailId { get; set; } // Thay Guid bằng string
        public string OrderId { get; set; } // Thay Guid bằng string
        public string ProductId { get; set; } // Thay Guid bằng string
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Navigation property
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
