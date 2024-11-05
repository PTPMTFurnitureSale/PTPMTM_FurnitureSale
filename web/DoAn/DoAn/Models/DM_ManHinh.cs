using System.ComponentModel.DataAnnotations;

namespace DoAn.Models
{
    public class DM_ManHinh
    {
        [Key]
        public string MaManHinh { get; set; }
        public string TenManHinh { get; set; }
        public ICollection<QL_PhanQuyen> PhanQuyens { get; set; }
    }
}
