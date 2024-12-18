﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn.Models
{
    [Table("san_pham")]
    public class SanPham
    {
        [Key]
        [Column("id_san_pham")]
        public string IdSanPham { get; set; }

        [Column("ten_san_pham")]
        public string TenSanPham { get; set; }

        [Column("don_gia")]
        public double DonGia { get; set; }

        [Column("diem")]
        public double Diem { get; set; }

        [Column("hinh")]
        public string Hinh { get; set; }

        [Column("mo_ta")]
        public string MoTa { get; set; }

        [Column("id_loai")]
        public string IdLoai { get; set; }
        [ForeignKey("IdLoai")]
        public LoaiSanPham LoaiSanPham { get; set; }
        public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public ICollection<KhuyenMai> KhuyenMais { get; set; } // Mối quan hệ với KhuyenMai
    }
}