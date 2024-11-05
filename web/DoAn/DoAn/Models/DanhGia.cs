using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn.Models
{
    public class DanhGia
    {
        [Key]
        [Column("id_danh_gia")]
        public int IdDanhGia { get; set; }

        [Column("noi_dung_danh_gia")]
        public string NoiDungDanhGia { get; set; }

        [Column("diem")]
        public int Diem { get; set; }

        [Column("ngay_danh_gia")]
        public DateTime NgayDanhGia { get; set; }

        [Column("id_khach_hang")]
        public string IdKhachHang { get; set; }

        [Column("id_san_pham")]
        public string IdSanPham { get; set; }
        public KhachHang KhachHang { get; set; }
        public SanPham SanPham { get; set; }
    }
}
