using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class KhachHangBLL
    {
        KhachHangDAL khdal = new KhachHangDAL();
        public KhachHangBLL() { }

        public List<khach_hang> GetKhachHang()
        {
            return khdal.GetKhachHang();
        }

        public bool ThemKhachHang(khach_hang kh)
        {
            if (string.IsNullOrEmpty(kh.id_khach_hang) ||
                string.IsNullOrEmpty(kh.ten_khach_hang) ||
                string.IsNullOrEmpty(kh.tai_khoan) ||
                string.IsNullOrEmpty(kh.mat_khau))
                return false;

            if (!string.IsNullOrEmpty(kh.email) && 
                !System.Text.RegularExpressions.Regex.IsMatch(kh.email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                return false;

            if (!string.IsNullOrEmpty(kh.dien_thoai) && 
                !System.Text.RegularExpressions.Regex.IsMatch(kh.dien_thoai, @"^[0-9]{10}$"))
                return false;

            return khdal.ThemKhachHang(kh);
        }

        public bool SuaKhachHang(khach_hang kh)
        {
            if (string.IsNullOrEmpty(kh.id_khach_hang) ||
                string.IsNullOrEmpty(kh.ten_khach_hang) ||
                string.IsNullOrEmpty(kh.tai_khoan) ||
                string.IsNullOrEmpty(kh.mat_khau))
                return false;

            if (!string.IsNullOrEmpty(kh.email) && 
                !System.Text.RegularExpressions.Regex.IsMatch(kh.email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                return false;

            if (!string.IsNullOrEmpty(kh.dien_thoai) && 
                !System.Text.RegularExpressions.Regex.IsMatch(kh.dien_thoai, @"^[0-9]{10}$"))
                return false;

            return khdal.SuaKhachHang(kh);
        }

        public bool XoaKhachHang(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;
            
            return khdal.XoaKhachHang(id);
        }
    }
}
