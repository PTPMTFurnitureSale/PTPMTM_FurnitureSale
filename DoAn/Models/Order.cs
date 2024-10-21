using System.ComponentModel.DataAnnotations;

namespace DoAn.Models
{
    public class Order
    {
        [Key]
        public string OrderId { get; set; } // Thay Guid bằng string
        public string CustomerId { get; set; } // Thay Guid bằng string
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime? ShippingDate { get; set; }
        public string Status { get; set; }

        // Navigation property
        public Customer Customer { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
