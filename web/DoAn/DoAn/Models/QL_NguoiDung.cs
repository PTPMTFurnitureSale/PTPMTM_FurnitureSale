using System.ComponentModel.DataAnnotations;

namespace DoAn.Models
{
    public class QL_NguoiDung
    {
        [Key]
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public bool HoatDong { get; set; }
        public ICollection<QL_NguoiDungNhomNguoiDung> NhomNguoiDungs { get; set; }
    }
}
