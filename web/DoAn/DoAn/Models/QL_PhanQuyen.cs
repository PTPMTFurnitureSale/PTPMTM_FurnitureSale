using System.ComponentModel.DataAnnotations;

namespace DoAn.Models
{
    public class QL_PhanQuyen
    {
        [Key]
        public string MaNhomNguoiDung { get; set; }
        [Key]
        public string MaManHinh { get; set; }
        public bool CoQuyen { get; set; }
        public QL_NhomNguoiDung NhomNguoiDung { get; set; }
        public DM_ManHinh ManHinh { get; set; }
    }
}
