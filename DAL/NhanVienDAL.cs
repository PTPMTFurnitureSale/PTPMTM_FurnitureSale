using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class NhanVienDAL
    {
        QuanLyBanHangDataContext qlbh = new QuanLyBanHangDataContext();

        public List<nhan_vien> GetNhanVien()
        {
            return qlbh.nhan_viens.Select(nv => nv).ToList();
        }

        public bool ThemNhanVien(nhan_vien nv)
        {
            try
            {
                qlbh.nhan_viens.InsertOnSubmit(nv);
                qlbh.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SuaNhanVien(nhan_vien nv)
        {
            try
            {
                nhan_vien nvUpdate = qlbh.nhan_viens.Where(n => n.id_nhan_vien == nv.id_nhan_vien).FirstOrDefault();
                if (nvUpdate != null)
                {
                    nvUpdate.ten_nhan_vien = nv.ten_nhan_vien;
                    nvUpdate.tai_khoan = nv.tai_khoan;
                    nvUpdate.mat_khau = nv.mat_khau;
                    nvUpdate.ngay_sinh = nv.ngay_sinh;
                    nvUpdate.dia_chi = nv.dia_chi;
                    nvUpdate.dien_thoai = nv.dien_thoai;
                    nvUpdate.chuc_vu = nv.chuc_vu;
                    nvUpdate.id_loai_nhan_vien = nv.id_loai_nhan_vien;
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

        public bool XoaNhanVien(string id)
        {
            try
            {
                nhan_vien nv = qlbh.nhan_viens.Where(n => n.id_nhan_vien == id).FirstOrDefault();
                if (nv != null)
                {
                    qlbh.nhan_viens.DeleteOnSubmit(nv);
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

        public List<nhan_vien> TimKiemNhanVien(string keyword)
        {
            return qlbh.nhan_viens.Where(nv => 
                nv.ten_nhan_vien.Contains(keyword) || 
                nv.tai_khoan.Contains(keyword) ||
                nv.dien_thoai.Contains(keyword)
            ).ToList();
        }
    }
}
