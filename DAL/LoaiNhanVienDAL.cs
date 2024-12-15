using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class LoaiNhanVienDAL
    {
        QuanLyBanHangDataContext qlbh = new QuanLyBanHangDataContext();

        public List<loai_nhan_vien> GetLoaiNhanVien()
        {
            return qlbh.loai_nhan_viens.Select(lnv => lnv).ToList();
        }

        public bool ThemLoaiNhanVien(loai_nhan_vien lnv)
        {
            try
            {
                qlbh.loai_nhan_viens.InsertOnSubmit(lnv);
                qlbh.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SuaLoaiNhanVien(loai_nhan_vien lnv)
        {
            try
            {
                loai_nhan_vien lnvUpdate = qlbh.loai_nhan_viens.Where(l => l.id_loai_nhan_vien == lnv.id_loai_nhan_vien).FirstOrDefault();
                if (lnvUpdate != null)
                {
                    lnvUpdate.ten_loai_nhan_vien = lnv.ten_loai_nhan_vien;
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

        public bool XoaLoaiNhanVien(string id)
        {
            try
            {
                loai_nhan_vien lnv = qlbh.loai_nhan_viens.Where(l => l.id_loai_nhan_vien == id).FirstOrDefault();
                if (lnv != null)
                {
                    qlbh.loai_nhan_viens.DeleteOnSubmit(lnv);
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
