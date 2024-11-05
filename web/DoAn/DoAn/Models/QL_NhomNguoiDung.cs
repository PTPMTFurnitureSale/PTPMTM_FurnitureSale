using System.ComponentModel.DataAnnotations;

namespace DoAn.Models
{
    public class QL_NhomNguoiDung
    {
        [Key]
        public string MaNhom { get; set; }
        public string TenNhom { get; set; }
        public string GhiChu { get; set; }
        public ICollection<QL_PhanQuyen> PhanQuyens { get; set; }
    }
}
