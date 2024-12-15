using System;
using System.Linq;
using DTO;

namespace DAL
{
    public class PhieuNhapDAL
    {
        private QuanLyBanHangDataContext db;

        public PhieuNhapDAL()
        {
            db = new QuanLyBanHangDataContext();
        }

        public bool AddPhieuNhap(phieu_nhap phieuNhap)
        {
            try
            {
                db.phieu_nhaps.InsertOnSubmit(phieuNhap);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
