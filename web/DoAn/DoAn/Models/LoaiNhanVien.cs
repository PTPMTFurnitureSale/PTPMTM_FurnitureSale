using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn.Models
{
    public class LoaiNhanVien
    {
        [Key]
        [Column("id_loai_nhan_vien")]
        public string IdLoaiNhanVien { get; set; }

        [Column("ten_loai_nhan_vien")]
        public string TenLoaiNhanVien { get; set; }
        public ICollection<NhanVien> NhanViens { get; set; }
    }
}
