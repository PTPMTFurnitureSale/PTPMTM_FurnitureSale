using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class LoaiSanPhamBLL
    {
        LoaiSanPhamDAL lspdal = new LoaiSanPhamDAL();
        public LoaiSanPhamBLL() { }


        public List<loai_san_pham> GetLoaiSanPham()
        {
            return lspdal.GetLoaiSanPham();
        }

    }
}
