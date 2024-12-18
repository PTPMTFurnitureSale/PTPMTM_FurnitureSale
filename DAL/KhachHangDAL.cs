using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class KhachHangDAL
    {
        QuanLyBanHangDataContext qlbh = new QuanLyBanHangDataContext();
        public KhachHangDAL() { }

        public List<khach_hang> GetAllKhachHang()
        {
            return qlbh.khach_hangs.ToList();
        }

        public List<khach_hang> GetKhachHang()
        {
            return qlbh.khach_hangs.Select(kh => kh).ToList();
        }

        public bool ThemKhachHang(khach_hang kh)
        {
            try
            {
                // Kiểm tra trùng mã khách hàng
                var khachHangTonTai = qlbh.khach_hangs.FirstOrDefault(k => k.id_khach_hang == kh.id_khach_hang);
                if (khachHangTonTai != null)
                    return false;

                // Kiểm tra trùng tài khoản
                khachHangTonTai = qlbh.khach_hangs.FirstOrDefault(k => k.tai_khoan == kh.tai_khoan);
                if (khachHangTonTai != null)
                    return false;

                qlbh.khach_hangs.InsertOnSubmit(kh);
                qlbh.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SuaKhachHang(khach_hang kh)
        {
            try
            {
                khach_hang khUpdate = qlbh.khach_hangs.FirstOrDefault(k => k.id_khach_hang == kh.id_khach_hang);
                if (khUpdate == null)
                    return false;

                // Kiểm tra trùng tài khoản với khách hàng khác
                var khachHangTrungTaiKhoan = qlbh.khach_hangs.FirstOrDefault(k => 
                    k.tai_khoan == kh.tai_khoan && 
                    k.id_khach_hang != kh.id_khach_hang);
                if (khachHangTrungTaiKhoan != null)
                    return false;

                khUpdate.ten_khach_hang = kh.ten_khach_hang;
                khUpdate.ngay_sinh = kh.ngay_sinh;
                khUpdate.dia_chi = kh.dia_chi;
                khUpdate.email = kh.email;
                khUpdate.dien_thoai = kh.dien_thoai;
                khUpdate.tai_khoan = kh.tai_khoan;
                khUpdate.mat_khau = kh.mat_khau;
                khUpdate.dia_chi_giao_hang = kh.dia_chi_giao_hang;
                    
                qlbh.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool XoaKhachHang(string id)
        {
            try
            {
                // Kiểm tra khách hàng có hóa đơn không
                var hoaDonTonTai = qlbh.hoa_dons.Any(hd => hd.id_khach_hang == id);
                if (hoaDonTonTai)
                    return false;

                khach_hang kh = qlbh.khach_hangs.FirstOrDefault(k => k.id_khach_hang == id);
                if (kh == null)
                    return false;

                qlbh.khach_hangs.DeleteOnSubmit(kh);
                qlbh.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
