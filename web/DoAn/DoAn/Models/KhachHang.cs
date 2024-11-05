using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn.Models
{
    [Table("khach_hang")]
    public class KhachHang
    {
        [Key]
        [Column("id_khach_hang")]
        public string IdKhachHang { get; set; } = Guid.NewGuid().ToString();

        [Column("ten_khach_hang")]
        [Required(ErrorMessage = "Họ và tên là bắt buộc.")]
        public string TenKhachHang { get; set; }

        [Column("ngay_sinh")]
        public DateTime NgaySinh { get; set; }

        [Column("dia_chi")]
        public string DiaChi { get; set; }

        [Column("email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }

        [Column("dien_thoai")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string DienThoai { get; set; }

        [Column("tai_khoan")]
        [Required(ErrorMessage = "Tài khoản là bắt buộc.")]
        public string TaiKhoan { get; set; }

        [Column("mat_khau")]
        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        public string MatKhau { get; set; }
    }
}
