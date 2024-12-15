using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class SanPhamBLL
    {
        SanPhamDAL spdal = new SanPhamDAL();
        public SanPhamBLL() { }

        public List<san_pham> GetSanPham()
        {
            return spdal.GetSanPham();
        }

        public bool ThemSanPham(san_pham sp)
        {
            if (string.IsNullOrEmpty(sp.ten_san_pham) || 
                !sp.don_gia.HasValue || 
                string.IsNullOrEmpty(sp.id_loai))
                return false;

            return spdal.ThemSanPham(sp);
        }

        public bool SuaSanPham(san_pham sp)
        {
            if (string.IsNullOrEmpty(sp.ten_san_pham) || 
                !sp.don_gia.HasValue || 
                string.IsNullOrEmpty(sp.id_loai))
                return false;

            return spdal.SuaSanPham(sp);
        }

        public bool XoaSanPham(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;
                
            return spdal.XoaSanPham(id);
        }

    }
}
