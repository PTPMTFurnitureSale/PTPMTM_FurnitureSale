using System.ComponentModel.DataAnnotations;

namespace DoAn.Models
{
    public class Customer
    {
        [Key]
        public string CustomerId { get; set; } // Thay Guid bằng string
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Avatar { get; set; }

        // Navigation property
        public ICollection<Order> Orders { get; set; }
    }
}
