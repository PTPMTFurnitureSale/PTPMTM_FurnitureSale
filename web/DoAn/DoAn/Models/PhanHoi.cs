using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn.Models
{
    public class PhanHoi
    {
        [Key]
        [Column("id_phan_hoi")]
        public int IdPhanHoi { get; set; }

        [Column("noi_dung_phan_hoi")]
        public string NoiDungPhanHoi { get; set; }

        [Column("ngay_phan_hoi")]
        public DateTime NgayPhanHoi { get; set; }

        [Column("id_khach_hang")]
        public string IdKhachHang { get; set; }

        [Column("id_san_pham")]
        public string IdSanPham { get; set; }
        public KhachHang KhachHang { get; set; }
        public SanPham SanPham { get; set; }
    }
}
