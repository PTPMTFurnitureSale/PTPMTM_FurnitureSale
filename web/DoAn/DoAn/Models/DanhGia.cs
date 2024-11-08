using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
namespace DoAn.Models
{
    [Table("danh_gia")]
    public class DanhGia
    {
        [Key]
        [Column("id_danh_gia")]
        public string IdDanhGia { get; set; } = Guid.NewGuid().ToString();

        [Column("noi_dung_danh_gia")]
        public string NoiDungDanhGia { get; set; }

        [Column("diem")]
        public int Diem { get; set; }

        [Column("ngay_danh_gia")]
        public DateTime NgayDanhGia { get; set; }
        [ForeignKey("KhachHang")]
        [Column("id_khach_hang")]
        public string IdKhachHang { get; set; }
        [ForeignKey("SanPham")]
        [Column("id_san_pham")]
        public string IdSanPham { get; set; }
        public KhachHang KhachHang { get; set; }
        public SanPham SanPham { get; set; }
    }
}
