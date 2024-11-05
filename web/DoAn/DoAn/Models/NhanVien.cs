using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn.Models
{
    public class NhanVien
    {
        [Key]
        [Column("id_nhan_vien")]
        public string IdNhanVien { get; set; }

        [Column("ten_nhan_vien")]
        public string TenNhanVien { get; set; }

        [Column("tai_khoan")]
        public string TaiKhoan { get; set; }

        [Column("mat_khau")]
        public string MatKhau { get; set; }

        [Column("ngay_sinh")]
        public DateTime NgaySinh { get; set; }

        [Column("dia_chi")]
        public string DiaChi { get; set; }

        [Column("dien_thoai")]
        public string DienThoai { get; set; }

        [Column("chuc_vu")]
        public string ChucVu { get; set; }

        [Column("id_loai_nhan_vien")]
        public string IdLoaiNhanVien { get; set; }
    }
}
