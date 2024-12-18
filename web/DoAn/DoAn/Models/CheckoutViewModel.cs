namespace DoAn.Models
{
    public class CheckoutViewModel
    {
        public KhachHang Customer { get; set; }
        public List<CartItem> CartItems { get; set; }
        public double TotalAmount { get; set; }
        public int PaymentMethod { get; set; }
    }
}
