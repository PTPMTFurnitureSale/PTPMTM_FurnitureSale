using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn.Models
{
    [Table("loai_san_pham")]
    public class LoaiSanPham
    {
        [Key]
        [Column("id_loai")]
        public string IdLoai { get; set; }

        [Column("ten_loai")]
        public string TenLoai { get; set; }

        [Column("hinh")]
        public string Hinh { get; set; }
        public ICollection<SanPham> SanPhams { get; set; }
    }
}
