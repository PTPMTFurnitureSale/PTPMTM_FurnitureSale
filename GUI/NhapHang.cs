using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Transactions;
using DTO;

namespace GUI
{
    public partial class NhapHang : UserControl
    {
        private TextBox txtIdPhieuNhap;
        private DateTimePicker dtpNgayNhap;
        private ComboBox cboNhanVien;
        private TextBox txtTongTien;
        private TextBox txtGhiChu;
        private Button btnAddPhieuNhap;
        
        // Components for chi_tiet_phieu_nhap
        private ComboBox cboSanPham;
        private TextBox txtSoLuong;
        private TextBox txtDonGia;
        private TextBox txtThanhTien;
        private Button btnAddChiTiet;
        private DataGridView dgvChiTietPhieuNhap;
        
        private QuanLyBanHangDataContext db;
        private BindingList<chi_tiet_phieu_nhap> chiTietList;

        public NhapHang()
        {
            InitializeComponent();
            db = new QuanLyBanHangDataContext();
            chiTietList = new BindingList<chi_tiet_phieu_nhap>();
            CreateUI();
            LoadSanPham();
            LoadNhanVien();
            GenerateIdPhieuNhap();
        }

        private void CreateUI()
        {
            // Phieu Nhap section
            Label lblPhieuNhap = new Label { Text = "THÔNG TIN PHIẾU NHẬP", Location = new Point(20, 20), AutoSize = true, Font = new Font(Font.FontFamily, 14, FontStyle.Bold) };
            
            Label lblIdPhieuNhap = new Label { Text = "Mã phiếu nhập:", Location = new Point(20, 60), AutoSize = true };
            Label lblNgayNhap = new Label { Text = "Ngày nhập:", Location = new Point(20, 100), AutoSize = true };
            Label lblIdNhanVien = new Label { Text = "Mã nhân viên:", Location = new Point(20, 140), AutoSize = true };
            Label lblTongTien = new Label { Text = "Tổng tiền:", Location = new Point(20, 180), AutoSize = true };
            Label lblGhiChu = new Label { Text = "Ghi chú:", Location = new Point(20, 220), AutoSize = true };

            txtIdPhieuNhap = new TextBox 
            { 
                Location = new Point(120, 57), 
                Width = 200,
                ReadOnly = true,
                BackColor = Color.White
            };
            dtpNgayNhap = new DateTimePicker { Location = new Point(120, 97), Width = 200 };
            cboNhanVien = new ComboBox 
            { 
                Location = new Point(120, 137), 
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            txtTongTien = new TextBox { Location = new Point(120, 177), Width = 200, ReadOnly = true };
            txtGhiChu = new TextBox { Location = new Point(120, 217), Width = 200, Multiline = true, Height = 60 };

            // Chi tiet section
            Label lblChiTiet = new Label { Text = "CHI TIẾT PHIẾU NHẬP", Location = new Point(20, 300), AutoSize = true, Font = new Font(Font.FontFamily, 14, FontStyle.Bold) };
            
            Label lblSanPham = new Label { Text = "Sản phẩm:", Location = new Point(20, 340), AutoSize = true };
            Label lblSoLuong = new Label { Text = "Số lượng:", Location = new Point(20, 380), AutoSize = true };
            Label lblDonGia = new Label { Text = "Đơn giá:", Location = new Point(20, 420), AutoSize = true };
            Label lblThanhTien = new Label { Text = "Thành tiền:", Location = new Point(20, 460), AutoSize = true };
            
            cboSanPham = new ComboBox { Location = new Point(120, 337), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            txtSoLuong = new TextBox { Location = new Point(120, 377), Width = 200 };
            txtDonGia = new TextBox { Location = new Point(120, 417), Width = 200 };
            txtThanhTien = new TextBox { Location = new Point(120, 457), Width = 200, ReadOnly = true };
            
            btnAddChiTiet = new Button 
            { 
                Text = "Thêm chi tiết", 
                Location = new Point(120, 497), 
                Width = 200,
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            btnAddPhieuNhap = new Button 
            { 
                Text = "Lưu phiếu nhập", 
                Location = new Point(120, 537), 
                Width = 200,
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            dgvChiTietPhieuNhap = new DataGridView 
            { 
                Location = new Point(350, 57), 
                Width = 600, 
                Height = 500,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White
            };

            dgvChiTietPhieuNhap.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { Name = "IdSanPham", HeaderText = "Mã SP", DataPropertyName = "id_san_pham", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "SoLuong", HeaderText = "Số lượng", DataPropertyName = "so_luong", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "DonGia", HeaderText = "Đơn giá", DataPropertyName = "don_gia", Width = 150 },
                new DataGridViewTextBoxColumn { Name = "ThanhTien", HeaderText = "Thành tiền", DataPropertyName = "thanh_tien", Width = 150 }
            });

            // Add event handlers
            txtSoLuong.TextChanged += TxtSoLuong_TextChanged;
            txtDonGia.TextChanged += TxtDonGia_TextChanged;
            btnAddChiTiet.Click += BtnAddChiTiet_Click;
            btnAddPhieuNhap.Click += BtnAddPhieuNhap_Click;

            // Add controls
            this.Controls.AddRange(new Control[] 
            {
                lblPhieuNhap,
                lblIdPhieuNhap, txtIdPhieuNhap,
                lblNgayNhap, dtpNgayNhap,
                lblIdNhanVien, cboNhanVien,
                lblTongTien, txtTongTien,
                lblGhiChu, txtGhiChu,
                lblChiTiet,
                lblSanPham, cboSanPham,
                lblSoLuong, txtSoLuong,
                lblDonGia, txtDonGia,
                lblThanhTien, txtThanhTien,
                btnAddChiTiet,
                btnAddPhieuNhap,
                dgvChiTietPhieuNhap
            });

            dgvChiTietPhieuNhap.DataSource = chiTietList;

            // Set background color for the form
            this.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void LoadSanPham()
        {
            var products = db.san_phams.Select(p => new { p.id_san_pham, p.ten_san_pham }).ToList();
            cboSanPham.DataSource = products;
            cboSanPham.DisplayMember = "ten_san_pham";
            cboSanPham.ValueMember = "id_san_pham";
        }

        private void TxtSoLuong_TextChanged(object sender, EventArgs e)
        {
            CalculateThanhTien();
        }

        private void TxtDonGia_TextChanged(object sender, EventArgs e)
        {
            CalculateThanhTien();
        }

        private void CalculateThanhTien()
        {
            int soLuong;
            float donGia;
            
            if (int.TryParse(txtSoLuong.Text, out soLuong) && 
                float.TryParse(txtDonGia.Text, out donGia))
            {
                txtThanhTien.Text = (soLuong * donGia).ToString();
            }
        }

        private void BtnAddChiTiet_Click(object sender, EventArgs e)
        {
            if (ValidateChiTietInput())
            {
                var chiTiet = new chi_tiet_phieu_nhap
                {
                    id_phieu_nhap = txtIdPhieuNhap.Text,
                    id_san_pham = cboSanPham.SelectedValue.ToString(),
                    so_luong = int.Parse(txtSoLuong.Text),
                    don_gia = float.Parse(txtDonGia.Text),
                    thanh_tien = float.Parse(txtThanhTien.Text)
                };

                chiTietList.Add(chiTiet);
                UpdateTongTien();
                ClearChiTietInputs();
            }
        }

        private void UpdateTongTien()
        {
            float tongTien = (float)chiTietList.Sum(ct => ct.thanh_tien.GetValueOrDefault());
            txtTongTien.Text = tongTien.ToString();
        }

        private void ClearChiTietInputs()
        {
            cboSanPham.SelectedIndex = -1;
            txtSoLuong.Clear();
            txtDonGia.Clear();
            txtThanhTien.Clear();
        }

        private bool ValidateChiTietInput()
        {
            if (cboSanPham.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm");
                return false;
            }

            int soLuong;
            if (!int.TryParse(txtSoLuong.Text, out soLuong))
            {
                MessageBox.Show("Số lượng không hợp lệ");
                return false;
            }

            float donGia;
            if (!float.TryParse(txtDonGia.Text, out donGia))
            {
                MessageBox.Show("Đơn giá không hợp lệ");
                return false;
            }

            return true;
        }

        private void BtnAddPhieuNhap_Click(object sender, EventArgs e)
        {
            if (ValidatePhieuNhapInput())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        var phieuNhap = new phieu_nhap
                        {
                            id_phieu_nhap = txtIdPhieuNhap.Text,
                            ngay_nhap = dtpNgayNhap.Value,
                            id_nhan_vien = cboNhanVien.SelectedValue.ToString(),
                            tong_tien = float.Parse(txtTongTien.Text),
                            ghi_chu = txtGhiChu.Text
                        };

                        db.phieu_nhaps.InsertOnSubmit(phieuNhap);

                        foreach (var chiTiet in chiTietList)
                        {
                            db.chi_tiet_phieu_nhaps.InsertOnSubmit(chiTiet);
                        }

                        db.SubmitChanges();
                        transaction.Complete();
                        MessageBox.Show("Thêm phiếu nhập thành công!");
                        txtIdPhieuNhap.Clear();
                        ClearAll();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }
        }

        private bool ValidatePhieuNhapInput()
        {
            if (string.IsNullOrEmpty(txtIdPhieuNhap.Text))
            {
                MessageBox.Show("Vui lòng nhập mã phiếu nhập");
                return false;
            }

            if (cboNhanVien.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên");
                return false;
            }

            if (chiTietList.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất một chi tiết phiếu nhập");
                return false;
            }

            return true;
        }

        private void ClearAll()
        {
            
            dtpNgayNhap.Value = DateTime.Now;
            cboNhanVien.SelectedIndex = -1;
            txtTongTien.Clear();
            txtGhiChu.Clear();
            chiTietList.Clear();
            ClearChiTietInputs();
            GenerateIdPhieuNhap();
        }

        private void RefreshDatabase()
        {
            if (db != null)
            {
                db.Dispose();
            }
            db = new QuanLyBanHangDataContext();
        }

        private void GenerateIdPhieuNhap()
        {
            try
            {
                RefreshDatabase(); // Refresh database trước khi truy vấn

                // Lấy ngày hiện tại
                string currentDate = DateTime.Now.ToString("yyyyMMdd");

                // Tìm mã phiếu nhập cuối cùng của ngày hiện tại
                var lastPhieuNhap = db.phieu_nhaps
                    .Where(p => p.id_phieu_nhap.StartsWith("PN" + currentDate))
                    .OrderByDescending(p => p.id_phieu_nhap)
                    .FirstOrDefault();

                string newId;
                if (lastPhieuNhap == null)
                {
                    // Nếu chưa có phiếu nhập nào trong ngày
                    newId = "PN" + currentDate + "001";
                }
                else
                {
                    // Lấy số thứ tự và tăng lên 1
                    string lastNumber = lastPhieuNhap.id_phieu_nhap.Substring(10); // Lấy 3 số cuối
                    int number = int.Parse(lastNumber) + 1;
                    newId = "PN" + currentDate + number.ToString("D3");
                }

                txtIdPhieuNhap.Text = newId;
            }
            catch
            {
                //MessageBox.Show("Lỗi khi tạo mã phiếu nhập");
            }
        }

        private void LoadNhanVien()
        {
            var nhanViens = db.nhan_viens
                .Select(nv => new 
                { 
                    nv.id_nhan_vien, 
                    HienThi = nv.id_nhan_vien + " - " + nv.ten_nhan_vien 
                })
                .ToList();

            cboNhanVien.DataSource = nhanViens;
            cboNhanVien.DisplayMember = "HienThi";
            cboNhanVien.ValueMember = "id_nhan_vien";
        }
    }
}
