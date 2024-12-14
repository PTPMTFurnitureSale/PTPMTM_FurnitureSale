using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn.Models
{
    [Table("hoa_don")]
    public class HoaDon
    {
        [Key]
        [Column("id_hoa_don")]
        public string IdHoaDon { get; set; }
        [ForeignKey("KhachHang")]
        [Column("id_khach_hang")]
        public string IdKhachHang { get; set; }

        [Column("ngay_dat")]
        public DateTime NgayDat { get; set; }

        [Column("ngay_giao_hang")]
        public DateTime NgayGiaoHang { get; set; }

        [Column("tinh_trang_thanh_toan")]
        public int TinhTrangThanhToan { get; set; }

        [Column("phuong_thuc_thanh_toan")]
        public int PhuongThucThanhToan { get; set; }

        [Column("tinh_trang_giao_hang")]
        public int TinhTrangGiaoHang { get; set; }

        [Column("tong_tien")]
        public double TongTien { get; set; }
        public KhachHang KhachHang { get; set; }
        public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
