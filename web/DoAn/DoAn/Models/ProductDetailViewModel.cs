namespace DoAn.Models
{
    public class ProductDetailViewModel
    {
        public SanPham Product { get; set; }
        public double DiscountPrice { get; set; } // Giá sau khuyến mãi
        public List<KhuyenMai> Promotions { get; set; } // Các khuyến mãi liên quan
        public List<DanhGia> Reviews { get; set; } // Các bình luận
        public List<SanPham> NewArrivals { get; set; }
    }
}
