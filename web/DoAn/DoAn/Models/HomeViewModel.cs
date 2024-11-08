namespace DoAn.Models
{
    public class HomeViewModel
    {
        public IEnumerable<LoaiSanPham> Categories { get; set; }
        public IEnumerable<SanPham> Products { get; set; }
    }
}
