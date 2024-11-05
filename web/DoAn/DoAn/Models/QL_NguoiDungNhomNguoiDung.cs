using System.ComponentModel.DataAnnotations;

namespace DoAn.Models
{
    public class QL_NguoiDungNhomNguoiDung
    {
        [Key]
        public string TenDangNhap { get; set; }
        [Key]
        public string MaNhomNguoiDung { get; set; }
        public string GhiChu { get; set; }
        public QL_NguoiDung NguoiDung { get; set; }
        public QL_NhomNguoiDung NhomNguoiDung { get; set; }
    }
}
