using System.ComponentModel.DataAnnotations;

namespace DoAn.Models
{
    public class Category
    {
        [Key]
        public string CategoryId { get; set; } // Thay Guid bằng string
        public string CategoryName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
