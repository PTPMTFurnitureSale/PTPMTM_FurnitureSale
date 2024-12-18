using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class LoaiNhanVienBLL
    {
        LoaiNhanVienDAL lnvDAL = new LoaiNhanVienDAL();

        public List<loai_nhan_vien> GetLoaiNhanVien()
        {
            return lnvDAL.GetLoaiNhanVien();
        }

        public bool ThemLoaiNhanVien(loai_nhan_vien lnv)
        {
            if (string.IsNullOrEmpty(lnv.ten_loai_nhan_vien))
                return false;
            
            return lnvDAL.ThemLoaiNhanVien(lnv);
        }

        public bool SuaLoaiNhanVien(loai_nhan_vien lnv)
        {
            if (string.IsNullOrEmpty(lnv.ten_loai_nhan_vien))
                return false;

            return lnvDAL.SuaLoaiNhanVien(lnv);
        }

        public bool XoaLoaiNhanVien(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;
                
            return lnvDAL.XoaLoaiNhanVien(id);
        }
    }
}
