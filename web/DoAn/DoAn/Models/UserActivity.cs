namespace DoAn.Models
{
    public class UserActivity
    {
        public string Id { get; set; }
        public string UserId { get; set; }  // ID người dùng
        public string ProductId { get; set; }  // ID sản phẩm
        public double Rating { get; set; }
        public DateTime Timestamp { get; set; }  // Thời gian hoạt động
        public string Action { get; set; }  // Loại hành động (xem, mua, đánh giá)
    }
}
