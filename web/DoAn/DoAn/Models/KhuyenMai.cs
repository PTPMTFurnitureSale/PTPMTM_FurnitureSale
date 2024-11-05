using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn.Models
{
    public class KhuyenMai
    {
        [Key]
        [Column("id_khuyen_mai")]
        public int IdKhuyenMai { get; set; }

        [Column("ten_khuyen_mai")]
        public string TenKhuyenMai { get; set; }

        [Column("noi_dung_khuyen_mai")]
        public string NoiDungKhuyenMai { get; set; }

        [Column("phan_tram_khuyen_mai")]
        public int PhanTramKhuyenMai { get; set; }

        [Column("id_san_pham")]
        public string IdSanPham { get; set; }
        public SanPham SanPham { get; set; }
    }
}
