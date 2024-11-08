namespace DoAn.Models
{
    public class ProductViewModel
    {
        public List<SanPham> Products { get; set; } // Tất cả sản phẩm
        public List<LoaiSanPham> ProductTypes { get; set; } // Các loại sản phẩm
        public List<KhuyenMai> Promotions { get; set; } // Các chương trình khuyến mãi
                                                        // Thêm các thuộc tính phân trang
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public List<SanPham> ProductsWithPromotions { get; set; } // Sản phẩm có khuyến mãi
        public List<SanPham> NewArrivals { get; set; } // Sản phẩm mới
        public List<SanPham> KitchenFurniture { get; set; } // Sản phẩm nội thất phòng bếp
    }
}
