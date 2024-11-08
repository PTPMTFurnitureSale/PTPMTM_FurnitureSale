using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn.Models
{
    [Table("khuyen_mai")]
    public class KhuyenMai
    {
        [Key]
        [Column("id_khuyen_mai")]
        public string IdKhuyenMai { get; set; }

        [Column("ten_khuyen_mai")]
        public string TenKhuyenMai { get; set; }

        [Column("noi_dung_khuyen_mai")]
        public string NoiDungKhuyenMai { get; set; }

        [Column("phan_tram_khuyen_mai")]
        public int PhanTramKhuyenMai { get; set; }

        [ForeignKey("SanPham")]
        [Column("id_san_pham")]  // Chắc chắn rằng cột này tồn tại trong cơ sở dữ liệu
        public string IdSanPham { get; set; }

        public SanPham SanPham { get; set; }  // Mối quan hệ với SanPham
    }
}
