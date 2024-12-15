using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using DTO;

namespace BLL
{
    public class ChiTietHoaDonBLL
    {
        private ChiTietHoaDonDAL chiTietHoaDonDAL;
        private QuanLyBanHangDataContext db;
        private SanPhamDAL sanPhamDAL;

        public ChiTietHoaDonBLL()
        {
            chiTietHoaDonDAL = new ChiTietHoaDonDAL();
            db = new QuanLyBanHangDataContext();
            sanPhamDAL = new SanPhamDAL();
        }

        public List<chi_tiet_hoa_don> GetChiTietHoaDon(string maHD)
        {
            return chiTietHoaDonDAL.GetChiTietHoaDon(maHD);
        }

        public bool ThemChiTietHoaDon(chi_tiet_hoa_don cthd)
        {
            return chiTietHoaDonDAL.ThemChiTietHoaDon(cthd);
        }

        public bool XoaChiTietHoaDon(string maHD, string maSP)
        {
            return chiTietHoaDonDAL.XoaChiTietHoaDon(maHD, maSP);
        }

        public bool SuaChiTietHoaDon(chi_tiet_hoa_don chiTiet)
        {
            try
            {
                var chiTietCu = db.chi_tiet_hoa_dons.FirstOrDefault(
                    ct => ct.id_hoa_don == chiTiet.id_hoa_don && 
                          ct.id_san_pham == chiTiet.id_san_pham);

                if (chiTietCu != null)
                {
                    chiTietCu.so_luong = chiTiet.so_luong;
                    chiTietCu.don_gia = chiTiet.don_gia;
                    chiTietCu.thanh_tien = chiTiet.thanh_tien;
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

        public bool AddChiTietHoaDon(chi_tiet_hoa_don chiTiet)
        {
            try
            {
                db.chi_tiet_hoa_dons.InsertOnSubmit(chiTiet);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateChiTietHoaDon(chi_tiet_hoa_don chiTiet)
        {
            try
            {
                var existing = db.chi_tiet_hoa_dons.FirstOrDefault(ct => ct.id_hoa_don == chiTiet.id_hoa_don && ct.id_san_pham == chiTiet.id_san_pham);
                if (existing != null)
                {
                    existing.so_luong = chiTiet.so_luong;
                    existing.don_gia = chiTiet.don_gia;
                    existing.thanh_tien = chiTiet.thanh_tien;
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

        public List<chi_tiet_hoa_don> GetAllChiTietHoaDon()
        {
            return chiTietHoaDonDAL.GetAllChiTietHoaDon();
        }
    }
}
