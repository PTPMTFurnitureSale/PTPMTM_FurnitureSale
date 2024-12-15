using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            FormClosing += Home_FormClosing;
            LoadTongQuan();
        }

        void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void toolStripThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void LoadTongQuan()
        {
            panelContent.Controls.Clear();
            TongQuan tq = new TongQuan();
            tq.Dock = DockStyle.Fill;
            panelContent.Controls.Add(tq);
        }

        void LoadSanPham()
        {
            panelContent.Controls.Clear();
            SanPham sp = new SanPham();
            sp.Dock = DockStyle.Fill;
            panelContent.Controls.Add(sp);
        }

        void LoadKhachHang()
        {
            panelContent.Controls.Clear();
            KhachHang kh = new KhachHang();
            kh.Dock = DockStyle.Fill;
            panelContent.Controls.Add(kh);
        }

        void LoadNhanVien()
        {
            panelContent.Controls.Clear();
            NhanVien nv = new NhanVien();
            nv.Dock = DockStyle.Fill;
            panelContent.Controls.Add(nv);
        }

        void LoadDonHang()
        {
            panelContent.Controls.Clear();
            DonHang dh = new DonHang();
            dh.Dock = DockStyle.Fill;
            panelContent.Controls.Add(dh);
        }

        void LoadNhapHang()
        {
            panelContent.Controls.Clear();
            NhapHang nh = new NhapHang();
            nh.Dock = DockStyle.Fill;
            panelContent.Controls.Add(nh);
        }

        private void toolStripTongQuan_Click(object sender, EventArgs e)
        {
            LoadTongQuan();
        }

        private void toolStripSanPham_Click(object sender, EventArgs e)
        {
            LoadSanPham();
        }

        private void toolStripKhachHang_Click(object sender, EventArgs e)
        {
            LoadKhachHang();
        }

        private void toolStripNhanVien_Click(object sender, EventArgs e)
        {
            LoadNhanVien();
        }

        private void toolStripHoaDon_Click(object sender, EventArgs e)
        {
            LoadDonHang();
        }

        private void toolStripThongKe_Click(object sender, EventArgs e)
        {
            LoadNhapHang();
        }

        
    }
}
