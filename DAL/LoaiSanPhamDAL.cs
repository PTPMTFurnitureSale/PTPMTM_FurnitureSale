using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class LoaiSanPhamDAL
    {
        QuanLyBanHangDataContext qlbh = new QuanLyBanHangDataContext();
        public LoaiSanPhamDAL() { }

        public List<loai_san_pham> GetLoaiSanPham()
        {
            return qlbh.loai_san_phams.Select(lsp => lsp).ToList();
        }

    }
}
