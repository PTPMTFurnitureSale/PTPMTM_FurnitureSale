using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class NhanVienBLL
    {
        NhanVienDAL nvDAL = new NhanVienDAL();

        public List<nhan_vien> GetNhanVien()
        {
            return nvDAL.GetNhanVien();
        }

        public bool ThemNhanVien(nhan_vien nv)
        {
            if (string.IsNullOrEmpty(nv.ten_nhan_vien) || 
                string.IsNullOrEmpty(nv.tai_khoan) || 
                string.IsNullOrEmpty(nv.mat_khau))
                return false;
            
            return nvDAL.ThemNhanVien(nv);
        }

        public bool SuaNhanVien(nhan_vien nv)
        {
            if (string.IsNullOrEmpty(nv.ten_nhan_vien) || 
                string.IsNullOrEmpty(nv.tai_khoan) || 
                string.IsNullOrEmpty(nv.mat_khau))
                return false;

            return nvDAL.SuaNhanVien(nv);
        }

        public bool XoaNhanVien(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;
                
            return nvDAL.XoaNhanVien(id);
        }

        public List<nhan_vien> TimKiemNhanVien(string keyword)
        {
            return nvDAL.TimKiemNhanVien(keyword);
        }
    }
}
