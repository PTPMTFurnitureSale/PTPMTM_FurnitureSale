using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace DAL
{
    public class HoaDonDAL
    {
        private QuanLyBanHangDataContext db;

        public HoaDonDAL()
        {
            db = new QuanLyBanHangDataContext();
        }

        public List<hoa_don> GetAllHoaDon()
        {
            return db.hoa_dons.ToList();
        }

        public bool ThemHoaDon(hoa_don hoaDon)
        {
            try
            {
                var check = db.hoa_dons.FirstOrDefault(h => h.id_hoa_don == hoaDon.id_hoa_don);
                if (check != null)
                    return false;

                db.hoa_dons.InsertOnSubmit(hoaDon);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SuaHoaDon(hoa_don hoaDonMoi)
        {
            try
            {
                var hoaDonCu = db.hoa_dons.SingleOrDefault(h => h.id_hoa_don == hoaDonMoi.id_hoa_don);
                if (hoaDonCu == null)
                    return false;

                hoaDonCu.id_khach_hang = hoaDonMoi.id_khach_hang;
                hoaDonCu.ngay_dat = hoaDonMoi.ngay_dat;
                hoaDonCu.ngay_giao_hang = hoaDonMoi.ngay_giao_hang;
                hoaDonCu.tinh_trang_thanh_toan = hoaDonMoi.tinh_trang_thanh_toan;
                hoaDonCu.phuong_thuc_thanh_toan = hoaDonMoi.phuong_thuc_thanh_toan;
                hoaDonCu.tinh_trang_giao_hang = hoaDonMoi.tinh_trang_giao_hang;
                hoaDonCu.tong_tien = hoaDonMoi.tong_tien;

                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool XoaHoaDon(string maHD)
        {
            try
            {
                var chiTietList = db.chi_tiet_hoa_dons.Where(ct => ct.id_hoa_don == maHD);
                if (chiTietList.Any())
                {
                    db.chi_tiet_hoa_dons.DeleteAllOnSubmit(chiTietList);
                    db.SubmitChanges();
                }

                var hoaDon = db.hoa_dons.SingleOrDefault(h => h.id_hoa_don == maHD);
                if (hoaDon != null)
                {
                    db.hoa_dons.DeleteOnSubmit(hoaDon);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
