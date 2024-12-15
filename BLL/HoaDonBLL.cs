using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using DTO;
using System.Data;

namespace BLL
{
    public class HoaDonBLL
    {
        private HoaDonDAL hoaDonDAL;

        public HoaDonBLL()
        {
            hoaDonDAL = new HoaDonDAL();
        }

        public DataTable GetAllHoaDon()
        {
            try
            {
                var query = from hd in hoaDonDAL.GetAllHoaDon()
                           join kh in new KhachHangDAL().GetAllKhachHang()
                           on hd.id_khach_hang equals kh.id_khach_hang
                           select new
                           {
                               hd.id_hoa_don,
                               hd.id_khach_hang,
                               kh.ten_khach_hang,
                               hd.ngay_dat,
                               hd.ngay_giao_hang,
                               hd.tinh_trang_thanh_toan,
                               hd.phuong_thuc_thanh_toan,
                               hd.tinh_trang_giao_hang,
                               hd.tong_tien
                           };

                DataTable dt = new DataTable();
                dt.Columns.Add("id_hoa_don", typeof(string));
                dt.Columns.Add("id_khach_hang", typeof(string));
                dt.Columns.Add("ten_khach_hang", typeof(string));
                dt.Columns.Add("ngay_dat", typeof(DateTime));
                dt.Columns.Add("ngay_giao_hang", typeof(DateTime));
                dt.Columns.Add("tinh_trang_thanh_toan", typeof(int));
                dt.Columns.Add("phuong_thuc_thanh_toan", typeof(int));
                dt.Columns.Add("tinh_trang_giao_hang", typeof(int));
                dt.Columns.Add("tong_tien", typeof(float));

                foreach (var item in query)
                {
                    dt.Rows.Add(
                        item.id_hoa_don,
                        item.id_khach_hang,
                        item.ten_khach_hang,
                        item.ngay_dat,
                        item.ngay_giao_hang,
                        item.tinh_trang_thanh_toan,
                        item.phuong_thuc_thanh_toan,
                        item.tinh_trang_giao_hang,
                        item.tong_tien
                    );
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }

        public bool ThemHoaDon(hoa_don hoaDon)
        {
            return hoaDonDAL.ThemHoaDon(hoaDon);
        }

        public bool SuaHoaDon(hoa_don hoaDon)
        {
            return hoaDonDAL.SuaHoaDon(hoaDon);
        }

        public bool XoaHoaDon(string maHD)
        {
            return hoaDonDAL.XoaHoaDon(maHD);
        }

        public bool CapNhatTongTien(hoa_don hoaDon)
        {
            return hoaDonDAL.SuaHoaDon(hoaDon);
        }
    }
}
