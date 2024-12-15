using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;

namespace GUI
{
    public partial class DonHang : UserControl
    {
        private HoaDonBLL hoaDonBLL;
        private ChiTietHoaDonBLL chiTietHoaDonBLL;

        // Khai báo controls
        private DataGridView dgvDonHang;
        private DataGridView dgvChiTietDonHang;
        private Panel pnlThongTin;
        private Panel pnlChiTiet;
        
        private Label lblMaDH;
        private Label lblKhachHang;
        private Label lblNgayDat;
        private Label lblNgayGiao;
        private Label lblTrangThaiTT;
        private Label lblPhuongThucTT;
        private Label lblTrangThaiGH;
        private Label lblTongTien;

        private TextBox txtMaDH;
        private TextBox txtKhachHang;
        private DateTimePicker dtpNgayDat;
        private DateTimePicker dtpNgayGiao;
        private ComboBox cboTrangThaiTT;
        private ComboBox cboPhuongThucTT;
        private ComboBox cboTrangThaiGH;
        private TextBox txtTongTien;

        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;

        public DonHang()
        {
            hoaDonBLL = new HoaDonBLL();
            chiTietHoaDonBLL = new ChiTietHoaDonBLL();
            
            CreateGUI();
            LoadData();
            RegisterEvents();
        }

        private void CreateGUI()
        {
            // Thiết lập UserControl
            this.Size = new Size(984, 636);
            this.BackColor = Color.White;

            // Panel thông tin
            pnlThongTin = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(964, 180),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Labels - Cột 1
            lblMaDH = new Label
            {
                Text = "Mã đơn hàng:",
                Location = new Point(20, 20),
                Size = new Size(90, 20)
            };

            lblKhachHang = new Label
            {
                Text = "Khách hàng:",
                Location = new Point(20, 50),
                Size = new Size(90, 20)
            };

            lblNgayDat = new Label
            {
                Text = "Ngày đặt:",
                Location = new Point(20, 80),
                Size = new Size(90, 20)
            };

            // Labels - Cột 2
            lblNgayGiao = new Label
            {
                Text = "Ngày giao:",
                Location = new Point(330, 20),
                Size = new Size(90, 20)
            };

            lblTrangThaiTT = new Label
            {
                Text = "Trạng thái TT:",
                Location = new Point(330, 50),
                Size = new Size(90, 20)
            };

            lblPhuongThucTT = new Label
            {
                Text = "P.thức TT:",
                Location = new Point(330, 80),
                Size = new Size(90, 20)
            };

            // Labels - Cột 3
            lblTrangThaiGH = new Label
            {
                Text = "Trạng thái GH:",
                Location = new Point(640, 20),
                Size = new Size(90, 20)
            };

            lblTongTien = new Label
            {
                Text = "Tổng tiền:",
                Location = new Point(640, 50),
                Size = new Size(90, 20)
            };

            // TextBoxes và Controls - Cột 1
            txtMaDH = new TextBox
            {
                Location = new Point(110, 20),
                Size = new Size(180, 20)
            };

            txtKhachHang = new TextBox
            {
                Location = new Point(110, 50),
                Size = new Size(180, 20)
            };

            dtpNgayDat = new DateTimePicker
            {
                Location = new Point(110, 80),
                Size = new Size(180, 20),
                Format = DateTimePickerFormat.Short
            };

            // Controls - Cột 2
            dtpNgayGiao = new DateTimePicker
            {
                Location = new Point(420, 20),
                Size = new Size(180, 20),
                Format = DateTimePickerFormat.Short
            };

            cboTrangThaiTT = new ComboBox
            {
                Location = new Point(420, 50),
                Size = new Size(180, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboTrangThaiTT.Items.AddRange(new object[] { "Chưa thanh toán", "Đã thanh toán" }); // 0: Chưa TT, 1: Đã TT

            cboPhuongThucTT = new ComboBox
            {
                Location = new Point(420, 80),
                Size = new Size(180, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboPhuongThucTT.Items.AddRange(new object[] { "Tiền mặt", "Chuyển khoản" }); // Index 0: Tiền mặt, 1: Chuyển khoản

            // Controls - Cột 3
            cboTrangThaiGH = new ComboBox
            {
                Location = new Point(730, 20),
                Size = new Size(180, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboTrangThaiGH.Items.AddRange(new object[] { "Chưa giao", "Đã giao" }); // 0: Chưa giao, 1: Đã giao

            txtTongTien = new TextBox
            {
                Location = new Point(730, 50),
                Size = new Size(180, 20),
                ReadOnly = true
            };

            // Buttons
            btnThem = new Button
            {
                Text = "Thêm",
                Location = new Point(20, 130),
                Size = new Size(80, 30),
                BackColor = Color.FromArgb(76, 175, 80), // Màu xanh lá
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White
            };

            btnSua = new Button
            {
                Text = "Sửa",
                Location = new Point(110, 130),
                Size = new Size(80, 30),
                BackColor = Color.FromArgb(255, 193, 7), // Màu vàng
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White
            };

            btnXoa = new Button
            {
                Text = "Xóa",
                Location = new Point(200, 130),
                Size = new Size(80, 30),
                BackColor = Color.FromArgb(244, 67, 54), // Màu đỏ
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White
            };

            // DataGridViews
            dgvDonHang = new DataGridView
            {
                Location = new Point(10, 200),
                Size = new Size(964, 200),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            dgvChiTietDonHang = new DataGridView
            {
                Location = new Point(10, 410),
                Size = new Size(964, 216),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            // Thêm controls vào panel
            pnlThongTin.Controls.AddRange(new Control[] {
                lblMaDH, lblKhachHang, lblNgayDat, lblNgayGiao, lblTrangThaiTT,
                lblPhuongThucTT, lblTrangThaiGH, lblTongTien,
                txtMaDH, txtKhachHang, dtpNgayDat, dtpNgayGiao,
                cboTrangThaiTT, cboPhuongThucTT, cboTrangThaiGH, txtTongTien,
                btnThem, btnSua, btnXoa
            });

            // Thêm controls vào form
            this.Controls.AddRange(new Control[] {
                pnlThongTin,
                dgvDonHang,
                dgvChiTietDonHang
            });
        }

        private void LoadData()
        {
            dgvDonHang.DataSource = hoaDonBLL.GetAllHoaDon();
            FormatDonHangGrid();
        }

        private void FormatDonHangGrid()
        {
            if (dgvDonHang.Columns.Count > 0)
            {
                //dgvDonHang.Columns["khach_hang"].Visible = false;
                dgvDonHang.Columns["id_hoa_don"].HeaderText = "Mã đơn hàng";
                dgvDonHang.Columns["id_khach_hang"].HeaderText = "Khách hàng";
                dgvDonHang.Columns["ngay_dat"].HeaderText = "Ngày đặt";
                dgvDonHang.Columns["ngay_giao_hang"].HeaderText = "Ngày giao";
                dgvDonHang.Columns["tinh_trang_thanh_toan"].HeaderText = "TT thanh toán";
                dgvDonHang.Columns["phuong_thuc_thanh_toan"].HeaderText = "PT thanh toán";
                dgvDonHang.Columns["tinh_trang_giao_hang"].HeaderText = "TT giao hàng";
                dgvDonHang.Columns["tong_tien"].HeaderText = "Tổng tiền";

                dgvDonHang.Columns["ngay_dat"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvDonHang.Columns["ngay_giao_hang"].DefaultCellStyle.Format = "dd/MM/yyyy";

                dgvDonHang.Columns["tong_tien"].DefaultCellStyle.Format = "N0";
                dgvDonHang.Columns["tong_tien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                // Thêm xử lý hiển thị cho các cột trạng thái
                
            }
        }

        private void RegisterEvents()
        {
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            dgvDonHang.CellClick += DgvDonHang_CellClick;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            // Xóa trắng các controls
            txtMaDH.Text = "";
            txtKhachHang.Text = "";
            dtpNgayDat.Value = DateTime.Now;
            dtpNgayGiao.Value = DateTime.Now;
            cboTrangThaiTT.SelectedIndex = 0;
            cboPhuongThucTT.SelectedIndex = 0;
            cboTrangThaiGH.SelectedIndex = 0;
            txtTongTien.Text = "0";

            // Enable/Disable các controls phù hợp
            txtMaDH.Enabled = true;
            txtKhachHang.Enabled = true;
            dtpNgayDat.Enabled = true;
            dtpNgayGiao.Enabled = true;
            cboTrangThaiTT.Enabled = true;
            cboPhuongThucTT.Enabled = true;
            cboTrangThaiGH.Enabled = true;

            // Enable/Disable các button
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            // Enable/Disable các controls phù hợp
            txtMaDH.Enabled = false;
            txtKhachHang.Enabled = true;
            dtpNgayDat.Enabled = true;
            dtpNgayGiao.Enabled = true;
            cboTrangThaiTT.Enabled = true;
            cboPhuongThucTT.Enabled = true;
            cboTrangThaiGH.Enabled = true;

            // Enable/Disable các button
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaDH.Text))
            {
                MessageBox.Show("Vui lòng chọn đơn hàng cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa đơn hàng này?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (hoaDonBLL.XoaHoaDon(txtMaDH.Text))
                {
                    MessageBox.Show("Xóa đơn hàng thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Xóa đơn hàng thất bại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DgvDonHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvDonHang.Rows[e.RowIndex];
                    
                    // Hiển thị thông tin đơn hàng
                    txtMaDH.Text = row.Cells["id_hoa_don"].Value.ToString();
                    txtKhachHang.Text = row.Cells["id_khach_hang"].Value.ToString();
                    
                    // Xử lý ngày đặt
                    DateTime ngayDat;
                    if (DateTime.TryParse(row.Cells["ngay_dat"].Value.ToString(), out ngayDat))
                    {
                        dtpNgayDat.Value = ngayDat;
                    }
                    
                    // Xử lý ngày giao
                    if (row.Cells["ngay_giao_hang"].Value != null)
                    {
                        DateTime ngayGiao;
                        if (DateTime.TryParse(row.Cells["ngay_giao_hang"].Value.ToString(), out ngayGiao))
                        {
                            dtpNgayGiao.Value = ngayGiao;
                        }
                    }
                    
                    // Xử lý trạng thái thanh toán (0: Chưa TT, 1: Đã TT)
                    int trangThaiTT = Convert.ToInt32(row.Cells["tinh_trang_thanh_toan"].Value);
                    cboTrangThaiTT.SelectedIndex = trangThaiTT; // 0 hoặc 1 tương ứng với index trong combobox
                    
                    // Xử lý phương thức thanh toán (1: Tiền mặt, 2: Chuyển khoản)
                    int phuongThucTT = Convert.ToInt32(row.Cells["phuong_thuc_thanh_toan"].Value);
                    cboPhuongThucTT.SelectedIndex = phuongThucTT - 1; // Trừ 1 vì giá trị bắt đầu từ 1
                    
                    // Xử lý trạng thái giao hàng (0: Chưa giao, 1: Đã giao)
                    int trangThaiGH = Convert.ToInt32(row.Cells["tinh_trang_giao_hang"].Value);
                    cboTrangThaiGH.SelectedIndex = trangThaiGH; // 0 hoặc 1 tương ứng với index trong combobox
                    
                    // Hiển thị tổng tiền
                    decimal tongTien = Convert.ToDecimal(row.Cells["tong_tien"].Value);
                    txtTongTien.Text = tongTien.ToString("N0");

                    // Load chi tiết đơn hàng
                    string maHD = txtMaDH.Text;
                    dgvChiTietDonHang.DataSource = chiTietHoaDonBLL.GetChiTietHoaDon(maHD);

                    // Định dạng lại DataGridView chi tiết
                    FormatChiTietDonHangGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị thông tin: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatChiTietDonHangGrid()
        {
            if (dgvChiTietDonHang.Columns.Count > 0)
            {
                dgvChiTietDonHang.Columns["id_hoa_don"].HeaderText = "Mã đơn hàng";
                dgvChiTietDonHang.Columns["id_san_pham"].HeaderText = "Mã sản phẩm";
                dgvChiTietDonHang.Columns["so_luong"].HeaderText = "Số lượng";
                dgvChiTietDonHang.Columns["don_gia"].HeaderText = "Đơn giá";
                dgvChiTietDonHang.Columns["thanh_tien"].HeaderText = "Thành tiền";

                dgvChiTietDonHang.Columns["don_gia"].DefaultCellStyle.Format = "N0";
                dgvChiTietDonHang.Columns["thanh_tien"].DefaultCellStyle.Format = "N0";

                dgvChiTietDonHang.Columns["so_luong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvChiTietDonHang.Columns["don_gia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvChiTietDonHang.Columns["thanh_tien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }
    }
}
