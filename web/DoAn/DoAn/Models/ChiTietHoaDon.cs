using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn.Models
{
    [Table("chi_tiet_hoa_don")]
    public class ChiTietHoaDon
    {
        [Key]
        [Column("id_hoa_don", Order = 0)]  // Combined the Order with Column attribute
        public string IdHoaDon { get; set; } 

        [Key]
        [Column("id_san_pham", Order = 1)] // Combined the Order with Column attribute
        public string IdSanPham { get; set; }

        [Column("so_luong")]
        public int SoLuong { get; set; }

        [Column("don_gia")]
        public double DonGia { get; set; }

        [Column("thanh_tien")]
        public double ThanhTien { get; set; }

        public HoaDon HoaDon { get; set; }
        public SanPham SanPham { get; set; }
    }
}
