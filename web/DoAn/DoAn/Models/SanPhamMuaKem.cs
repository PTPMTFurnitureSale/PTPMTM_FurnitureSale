using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn.Models
{
    [Table("san_pham_mua_kem")]
    public class SanPhamMuaKem
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }
        [Column("id_san_pham")]
        public string IdSanPham { get; set; }
        [Column("id_san_pham_kem")]
        public string IdSanPhamKem { get; set; }
        [Column("don_gia")]
        public double DonGia { get; set; } // Thay đổi từ float thành double

        [Column("rating")]
        public double Rating { get; set; }

    }
}
