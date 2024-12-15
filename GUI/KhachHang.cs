using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BLL;

namespace GUI
{
    public partial class KhachHang : UserControl
    {
        KhachHangBLL khbll = new KhachHangBLL();

        public KhachHang()
        {
            InitializeComponent();
            this.Load += KhachHang_Load;
        }

        void KhachHang_Load(object sender, EventArgs e)
        {
            CustomizeDataGridView();
            LoadKhachHang();
        }

        public void LoadKhachHang()
        {
            dtgvKhachHang.DataSource = khbll.GetKhachHang();
            dtgvKhachHang.Columns["id_khach_hang"].HeaderText = "Mã Khách Hàng";
            dtgvKhachHang.Columns["ten_khach_hang"].HeaderText = "Tên Khách Hàng";
            dtgvKhachHang.Columns["ngay_sinh"].HeaderText = "Ngày Sinh";
            dtgvKhachHang.Columns["dia_chi"].HeaderText = "Địa Chỉ";
            dtgvKhachHang.Columns["email"].HeaderText = "Email";
            dtgvKhachHang.Columns["dien_thoai"].HeaderText = "Điện Thoại";
            dtgvKhachHang.Columns["tai_khoan"].HeaderText = "Tài Khoản";
            dtgvKhachHang.Columns["mat_khau"].HeaderText = "Mật Khẩu";
            dtgvKhachHang.Columns["dia_chi_giao_hang"].HeaderText = "Địa Chỉ Giao Hàng";
        }

        private void CustomizeDataGridView()
        {
            // Set alternating row colors
            dtgvKhachHang.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            // Set column headers style
            dtgvKhachHang.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dtgvKhachHang.ColumnHeadersDefaultCellStyle.BackColor = Color.Brown;
            dtgvKhachHang.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgvKhachHang.EnableHeadersVisualStyles = false;

            // Set grid line style
            dtgvKhachHang.GridColor = Color.Black;
            dtgvKhachHang.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Auto size columns
            dtgvKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Set row height
            dtgvKhachHang.RowTemplate.Height = 30;
        }

        private void dtgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvKhachHang.Rows[e.RowIndex];
                txtMaKhachHang.Text = row.Cells["id_khach_hang"].Value.ToString();
                txtTenKhachHang.Text = row.Cells["ten_khach_hang"].Value.ToString();
                mtbNgaySinh.Text = Convert.ToDateTime(row.Cells["ngay_sinh"].Value).ToString("dd/MM/yyyy");
                txtDiaChi.Text = row.Cells["dia_chi"].Value.ToString();
                txtEmail.Text = row.Cells["email"].Value.ToString();
                txtSoDienThoai.Text = row.Cells["dien_thoai"].Value.ToString();
                txtTaiKhoan.Text = row.Cells["tai_khoan"].Value.ToString();
                txtMatKhau.Text = row.Cells["mat_khau"].Value.ToString();
                rtbDiaChiGiaoHang.Text = row.Cells["dia_chi_giao_hang"].Value.ToString();
            }
        }

        private void ClearInputs()
        {
            txtMaKhachHang.Clear();
            txtTenKhachHang.Clear();
            mtbNgaySinh.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            txtSoDienThoai.Clear();
            txtTaiKhoan.Clear();
            txtMatKhau.Clear();
            rtbDiaChiGiaoHang.Clear();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaKhachHang.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã khách hàng!");
                    return;
                }

                khach_hang kh = new khach_hang
                {
                    id_khach_hang = txtMaKhachHang.Text,
                    ten_khach_hang = txtTenKhachHang.Text,
                    ngay_sinh = DateTime.ParseExact(mtbNgaySinh.Text, "dd/MM/yyyy", null),
                    dia_chi = txtDiaChi.Text,
                    email = txtEmail.Text,
                    dien_thoai = txtSoDienThoai.Text,
                    tai_khoan = txtTaiKhoan.Text,
                    mat_khau = txtMatKhau.Text,
                    dia_chi_giao_hang = rtbDiaChiGiaoHang.Text
                };

                if (khbll.ThemKhachHang(kh))
                {
                    MessageBox.Show("Thêm khách hàng thành công!");
                    LoadKhachHang();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Thêm khách hàng thất bại! Vui lòng kiểm tra lại thông tin.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                khach_hang kh = new khach_hang
                {
                    id_khach_hang = txtMaKhachHang.Text,
                    ten_khach_hang = txtTenKhachHang.Text,
                    ngay_sinh = DateTime.ParseExact(mtbNgaySinh.Text, "dd/MM/yyyy", null),
                    dia_chi = txtDiaChi.Text,
                    email = txtEmail.Text,
                    dien_thoai = txtSoDienThoai.Text,
                    tai_khoan = txtTaiKhoan.Text,
                    mat_khau = txtMatKhau.Text,
                    dia_chi_giao_hang = rtbDiaChiGiaoHang.Text
                };

                if (khbll.SuaKhachHang(kh))
                {
                    MessageBox.Show("Sửa khách hàng thành công!");
                    LoadKhachHang();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Sửa khách hàng thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKhachHang.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa khách hàng này?", "Xác nhận", 
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (khbll.XoaKhachHang(txtMaKhachHang.Text))
                {
                    MessageBox.Show("Xóa khách hàng thành công!");
                    LoadKhachHang();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Xóa khách hàng thất bại! Khách hàng có thể đã có hóa đơn.");
                }
            }
        }
    }
}
