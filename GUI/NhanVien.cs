using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BLL;
using DTO;

namespace GUI
{
    public partial class NhanVien : UserControl
    {
        private NhanVienBLL nvBLL = new NhanVienBLL();
        private LoaiNhanVienBLL lnvBLL = new LoaiNhanVienBLL();
        
        public NhanVien()
        {
            InitializeComponent();
            LoadData();
            CustomizeDataGridView();
            LoadComboBoxLoaiNhanVien();
            dgvNhanVien.CellClick += dgvNhanVien_CellClick;
        }

        private void LoadData()
        {
            dgvNhanVien.DataSource = nvBLL.GetNhanVien();
            dgvNhanVien.Columns["loai_nhan_vien"].Visible = false;
        }

        private void LoadComboBoxLoaiNhanVien()
        {
            cboLoaiNV.DataSource = lnvBLL.GetLoaiNhanVien();
            cboLoaiNV.DisplayMember = "ten_loai_nhan_vien";
            cboLoaiNV.ValueMember = "id_loai_nhan_vien";
        }

        private void CustomizeDataGridView()
        {
            try
            {
                if (dgvNhanVien != null)
                {
                    // Định dạng cơ bản cho DataGridView
                    dgvNhanVien.BackgroundColor = Color.White;
                    dgvNhanVien.BorderStyle = BorderStyle.None;
                    dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    dgvNhanVien.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                    dgvNhanVien.ScrollBars = ScrollBars.Both;
                    dgvNhanVien.RowHeadersVisible = false;
                    dgvNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgvNhanVien.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                    dgvNhanVien.RowTemplate.Height = 35;

                    // Định dạng màu sắc cho dòng
                    dgvNhanVien.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
                    dgvNhanVien.DefaultCellStyle.BackColor = Color.White;
                    dgvNhanVien.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
                    dgvNhanVien.DefaultCellStyle.SelectionForeColor = Color.White;
                    dgvNhanVien.DefaultCellStyle.Font = new Font("Segoe UI", 9.75F);

                    // Định dạng header
                    dgvNhanVien.EnableHeadersVisualStyles = false;
                    dgvNhanVien.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 122, 204);
                    dgvNhanVien.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    dgvNhanVien.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                    dgvNhanVien.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvNhanVien.ColumnHeadersHeight = 35;
                    dgvNhanVien.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

                    // Đổi tên cột
                    if(dgvNhanVien.Columns.Count > 0)
                    {
                        dgvNhanVien.Columns["id_nhan_vien"].HeaderText = "Mã NV";
                        dgvNhanVien.Columns["ten_nhan_vien"].HeaderText = "Tên Nhân Viên";
                        dgvNhanVien.Columns["tai_khoan"].HeaderText = "Tài Khoản";
                        dgvNhanVien.Columns["mat_khau"].HeaderText = "Mật Khẩu";
                        dgvNhanVien.Columns["ngay_sinh"].HeaderText = "Ngày Sinh";
                        dgvNhanVien.Columns["dia_chi"].HeaderText = "Địa Chỉ";
                        dgvNhanVien.Columns["dien_thoai"].HeaderText = "Điện Thoại";
                        dgvNhanVien.Columns["chuc_vu"].HeaderText = "Chức Vụ";
                        dgvNhanVien.Columns["id_loai_nhan_vien"].HeaderText = "Loại NV";

                        // Định dạng lại độ rộng một số cột đặc biệt (tùy chọn)
                        //dgvNhanVien.Columns["id_nhan_vien"].Width = 100;
                        //dgvNhanVien.Columns["ten_nhan_vien"].Width = 150;
                        //dgvNhanVien.Columns["dien_thoai"].Width = 100;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi định dạng bảng: " + ex.Message);
            }
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];

                    txtMaNV.Text = row.Cells["id_nhan_vien"].Value != null ? 
                                 row.Cells["id_nhan_vien"].Value.ToString() : "";
                                 
                    txtTenNV.Text = row.Cells["ten_nhan_vien"].Value != null ? 
                                  row.Cells["ten_nhan_vien"].Value.ToString() : "";
                                  
                    txtTaiKhoan.Text = row.Cells["tai_khoan"].Value != null ? 
                                     row.Cells["tai_khoan"].Value.ToString() : "";
                                     
                    txtMatKhau.Text = row.Cells["mat_khau"].Value != null ? 
                                    row.Cells["mat_khau"].Value.ToString() : "";

                    if (row.Cells["ngay_sinh"].Value != null && 
                        row.Cells["ngay_sinh"].Value != DBNull.Value)
                    {
                        dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["ngay_sinh"].Value);
                    }

                    txtDiaChi.Text = row.Cells["dia_chi"].Value != null ? 
                                   row.Cells["dia_chi"].Value.ToString() : "";
                                   
                    txtDienThoai.Text = row.Cells["dien_thoai"].Value != null ? 
                                      row.Cells["dien_thoai"].Value.ToString() : "";
                                      
                    txtChucVu.Text = row.Cells["chuc_vu"].Value != null ? 
                                   row.Cells["chuc_vu"].Value.ToString() : "";

                    var idLoaiNV = row.Cells["id_loai_nhan_vien"].Value;
                    if (idLoaiNV != null && idLoaiNV != DBNull.Value)
                    {
                        cboLoaiNV.SelectedValue = idLoaiNV.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi load dữ liệu: " + ex.Message);
                }
            }
        }

        private string TaoMaNhanVien()
        {
            // Lấy ngày hiện tại
            string ngayHienTai = DateTime.Now.ToString("ddMMyyyy");
            
            // Lấy danh sách nhân viên
            var dsNhanVien = nvBLL.GetNhanVien();
            
            // Lọc các mã nhân viên trong ngày hiện tại
            var dsNVTrongNgay = dsNhanVien.Where(nv => nv.id_nhan_vien.StartsWith(ngayHienTai))
                                         .Select(nv => nv.id_nhan_vien)
                                         .ToList();

            if (dsNVTrongNgay.Count == 0)
            {
                // Nếu chưa có nhân viên nào trong ngày -> trả về mã đầu tiên
                return string.Format("{0}.NV001", ngayHienTai);
            }
            else
            {
                // Tìm số thứ tự lớn nhất
                int maxStt = dsNVTrongNgay.Select(ma =>
                {
                    // Tách lấy phần số thứ tự (3 số cuối)
                    string stt = ma.Substring(ma.Length - 3);
                    return int.Parse(stt);
                }).Max();

                // Tạo số thứ tự mới
                string sttMoi = (maxStt + 1).ToString("000");
                return string.Format("{0}.NV{1}", ngayHienTai, sttMoi);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu nhập
                if (string.IsNullOrWhiteSpace(txtTenNV.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên nhân viên!");
                    txtTenNV.Focus();
                    return;
                }

                // Tạo đối tượng nhân viên mới
                nhan_vien nv = new nhan_vien
                {
                    id_nhan_vien = TaoMaNhanVien(),
                    ten_nhan_vien = txtTenNV.Text.Trim(),
                    tai_khoan = txtTaiKhoan.Text.Trim(),
                    mat_khau = txtMatKhau.Text.Trim(),
                    ngay_sinh = dtpNgaySinh.Value,
                    dia_chi = txtDiaChi.Text.Trim(),
                    dien_thoai = txtDienThoai.Text.Trim(),
                    chuc_vu = txtChucVu.Text.Trim(),
                    id_loai_nhan_vien = cboLoaiNV.SelectedValue.ToString()
                };

                // Thêm nhân viên
                if (nvBLL.ThemNhanVien(nv))
                {
                    MessageBox.Show("Thêm nhân viên thành công!");
                    LoadData();  // Refresh lại datagridview
                    XoaTrang(); // Xóa trắng các controls
                }
                else
                {
                    MessageBox.Show("Thêm nhân viên thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            nhan_vien nv = new nhan_vien
            {
                id_nhan_vien = txtMaNV.Text,
                ten_nhan_vien = txtTenNV.Text,
                tai_khoan = txtTaiKhoan.Text,
                mat_khau = txtMatKhau.Text,
                ngay_sinh = dtpNgaySinh.Value,
                dia_chi = txtDiaChi.Text,
                dien_thoai = txtDienThoai.Text,
                chuc_vu = txtChucVu.Text,
                id_loai_nhan_vien = cboLoaiNV.SelectedValue.ToString()
            };

            if (nvBLL.SuaNhanVien(nv))
            {
                MessageBox.Show("Sửa nhân viên thành công!");
                LoadData();
            }
            else
                MessageBox.Show("Sửa nhân viên thất bại!");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (nvBLL.XoaNhanVien(txtMaNV.Text))
            {
                MessageBox.Show("Xóa nhân viên thành công!");
                LoadData();
            }
            else
                MessageBox.Show("Xóa nhân viên thất bại!");
        }

        //private void txtTimKiem_TextChanged(object sender, EventArgs e)
        //{
        //    dgvNhanVien.DataSource = nvBLL.TimKiemNhanVien(txtTimKiem.Text);
        //}
        private void XoaTrang()
        {
            // Xóa trắng các TextBox
            txtMaNV.Clear();
            txtTenNV.Clear();
            txtTaiKhoan.Clear();
            txtMatKhau.Clear();
            txtDiaChi.Clear();
            txtDienThoai.Clear();
            txtChucVu.Clear();

            // Reset DateTimePicker về ngày hiện tại
            dtpNgaySinh.Value = DateTime.Now;

            // Reset ComboBox về item đầu tiên
            if (cboLoaiNV.Items.Count > 0)
            {
                cboLoaiNV.SelectedIndex = 0;
            }

            // Focus về TextBox đầu tiên
            txtTenNV.Focus();
        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnXoaTrang_Click(object sender, EventArgs e)
        {
            XoaTrang();
        }
    }
}
