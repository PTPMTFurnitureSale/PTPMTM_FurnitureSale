using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class SanPhamDAL
    {
        QuanLyBanHangDataContext qlbh = new QuanLyBanHangDataContext();
        public SanPhamDAL() { }

        public List<san_pham> GetSanPham()
        {
            return qlbh.san_phams.Select(sp => sp).ToList();
        }

        public List<san_pham> GetAllSanPham()
        {
            return qlbh.san_phams.ToList();
        }

        public bool ThemSanPham(san_pham sp)
        {
            try
            {
                qlbh.san_phams.InsertOnSubmit(sp);
                qlbh.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SuaSanPham(san_pham sp)
        {
            try
            {
                san_pham spUpdate = qlbh.san_phams.Where(s => s.id_san_pham == sp.id_san_pham).FirstOrDefault();
                if (spUpdate != null)
                {
                    spUpdate.ten_san_pham = sp.ten_san_pham;
                    spUpdate.don_gia = sp.don_gia;
                    spUpdate.diem = sp.diem;
                    spUpdate.hinh = sp.hinh;
                    spUpdate.mo_ta = sp.mo_ta;
                    spUpdate.id_loai = sp.id_loai;
                    spUpdate.so_luong_ton = sp.so_luong_ton;
                    
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

        public bool XoaSanPham(string id)
        {
            try
            {
                san_pham sp = qlbh.san_phams.Where(s => s.id_san_pham == id).FirstOrDefault();
                if (sp != null)
                {
                    qlbh.san_phams.DeleteOnSubmit(sp);
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
