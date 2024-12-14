using Microsoft.EntityFrameworkCore;

namespace DoAn.Models
{
    public class QuanLyBanHangContext : DbContext
    {
        public QuanLyBanHangContext(DbContextOptions<QuanLyBanHangContext> options) : base(options) { }

        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<LoaiNhanVien> LoaiNhanViens { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<PhanHoi> PhanHois { get; set; }
        public DbSet<DanhGia> DanhGias { get; set; }
        public DbSet<KhuyenMai> KhuyenMais { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public DbSet<DM_ManHinh> DM_ManHinhs { get; set; }
        public DbSet<QL_NguoiDung> QL_NguoiDungs { get; set; }
        public DbSet<QL_NhomNguoiDung> QL_NhomNguoiDungs { get; set; }
        public DbSet<QL_NguoiDungNhomNguoiDung> QL_NguoiDungNhomNguoiDungs { get; set; }
        public DbSet<QL_PhanQuyen> QL_PhanQuyens { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Định nghĩa khóa chính cho bảng liên kết QL_NguoiDungNhomNguoiDung
            modelBuilder.Entity<QL_NguoiDungNhomNguoiDung>()
                .HasKey(nd => new { nd.TenDangNhap, nd.MaNhomNguoiDung });

            // Định nghĩa khóa chính cho bảng phân quyền QL_PhanQuyen
            modelBuilder.Entity<QL_PhanQuyen>()
                .HasKey(pq => new { pq.MaNhomNguoiDung, pq.MaManHinh });

            // Định nghĩa khóa chính cho bảng chi tiết hóa đơn ChiTietHoaDon
            modelBuilder.Entity<ChiTietHoaDon>()
                .HasKey(ct => new { ct.IdHoaDon, ct.IdSanPham });

            // Thiết lập các mối quan hệ khác nếu cần thiết
        }
    }
}
