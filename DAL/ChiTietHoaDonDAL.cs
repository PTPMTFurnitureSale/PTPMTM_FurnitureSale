using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class ChiTietHoaDonDAL
    {
        private QuanLyBanHangDataContext qlbh;

        public ChiTietHoaDonDAL()
        {
            qlbh = new QuanLyBanHangDataContext();
        }

        public List<chi_tiet_hoa_don> GetChiTietHoaDon(string maHD)
        {
            return qlbh.chi_tiet_hoa_dons.Where(ct => ct.id_hoa_don == maHD).ToList();
        }

        public bool ThemChiTietHoaDon(chi_tiet_hoa_don cthd)
        {
            try
            {
                qlbh.chi_tiet_hoa_dons.InsertOnSubmit(cthd);
                qlbh.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool XoaChiTietHoaDon(string maHD, string maSP)
        {
            try
            {
                chi_tiet_hoa_don cthd = qlbh.chi_tiet_hoa_dons.SingleOrDefault(
                    ct => ct.id_hoa_don == maHD && ct.id_san_pham == maSP);
                if (cthd != null)
                {
                    qlbh.chi_tiet_hoa_dons.DeleteOnSubmit(cthd);
                    qlbh.SubmitChanges();
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
            return qlbh.chi_tiet_hoa_dons.ToList();
        }

        public bool DeleteChiTietHoaDon(string idHoaDon, string idSanPham)
        {
            try
            {
                var chiTiet = qlbh.chi_tiet_hoa_dons.FirstOrDefault(ct => ct.id_hoa_don == idHoaDon && ct.id_san_pham == idSanPham);
                if (chiTiet != null)
                {
                    qlbh.chi_tiet_hoa_dons.DeleteOnSubmit(chiTiet);
                    qlbh.SubmitChanges();
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
