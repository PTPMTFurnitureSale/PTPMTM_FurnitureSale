using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn.Models
{
    [Table("phan_hoi")]
    public class PhanHoi
    {
        [Key]
        [Column("id_phan_hoi")]
        public string IdPhanHoi { get; set; } = Guid.NewGuid().ToString();

        [Column("noi_dung_phan_hoi")]
        public string NoiDungPhanHoi { get; set; }

        [Column("ngay_phan_hoi")]
        public DateTime NgayPhanHoi { get; set; }
        [ForeignKey("KhachHang")]
        [Column("id_khach_hang")]
        public string IdKhachHang { get; set; }

        [ForeignKey("SanPham")]
        [Column("id_san_pham")]  // Đảm bảo tên này khớp với cột trong cơ sở dữ liệu
        public string SanPhamId { get; set; }
        public KhachHang KhachHang { get; set; }
        public SanPham SanPham { get; set; }
    }
}
