using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using DTO;
using BLL;
using System.IO;

namespace GUI
{
    public partial class SanPham : UserControl
    {
        SanPhamBLL spbll = new SanPhamBLL();
        LoaiSanPhamBLL lspbll = new LoaiSanPhamBLL();

        public SanPham()
        {
            InitializeComponent();
            this.Load += SanPham_Load;
        }

        void SanPham_Load(object sender, EventArgs e)
        {
            CustomizeDataGridView();
            LoadSanPham();
            LoadComboboxLoaiSanPham();
        }

        public void LoadSanPham()
        {
            dtgvSanPham.DataSource = spbll.GetSanPham();
            dtgvSanPham.Columns["loai_san_pham"].Visible = false;
            
            dtgvSanPham.Columns["id_san_pham"].HeaderText = "Mã Sản Phẩm";
            dtgvSanPham.Columns["ten_san_pham"].HeaderText = "Tên Sản Phẩm";
            dtgvSanPham.Columns["so_luong_ton"].HeaderText = "Số lượng tồn";
            dtgvSanPham.Columns["don_gia"].HeaderText = "Đơn Giá";
            dtgvSanPham.Columns["diem"].HeaderText = "Điểm";
            dtgvSanPham.Columns["hinh"].HeaderText = "Hình Ảnh";
            dtgvSanPham.Columns["mo_ta"].HeaderText = "Mô Tả";
            dtgvSanPham.Columns["id_loai"].HeaderText = "Mã Loại Sản Phẩm";
            
        }

        public void LoadComboboxLoaiSanPham()
        {
            cbbLoaiSanPham.DataSource = lspbll.GetLoaiSanPham();
            cbbLoaiSanPham.DisplayMember = "ten_loai";
            cbbLoaiSanPham.ValueMember = "id_loai";
        }

        private void CustomizeDataGridView()
        {
            // Set alternating row colors
            dtgvSanPham.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            // Set column headers style
            dtgvSanPham.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dtgvSanPham.ColumnHeadersDefaultCellStyle.BackColor = Color.Brown;
            dtgvSanPham.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgvSanPham.EnableHeadersVisualStyles = false;

            // Set grid line style
            dtgvSanPham.GridColor = Color.Black;
            dtgvSanPham.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Auto size columns
            dtgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Set row height
            dtgvSanPham.RowTemplate.Height = 30;
        }

        private void LoadImageFromFile(string imagePath)
        {
            try
            {
                if (pbHinhAnh.Image != null)
                {
                    pbHinhAnh.Image.Dispose();
                    pbHinhAnh.Image = null;
                }

                if (File.Exists(imagePath))
                {
                    // Tạo bản sao của ảnh để tránh lock file
                    byte[] imageBytes = File.ReadAllBytes(imagePath);
                    using (var ms = new MemoryStream(imageBytes))
                    {
                        pbHinhAnh.Image = new Bitmap(ms);
                        pbHinhAnh.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
            }
            catch (Exception ex)
            {
                
                pbHinhAnh.Image = null;
            }
        }

        private void dtgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgvSanPham.Rows[e.RowIndex];
                txtMaSanPham.Text = row.Cells["id_san_pham"].Value.ToString();
                txtTenSanPham.Text = row.Cells["ten_san_pham"].Value.ToString();
                txtDonGia.Text = row.Cells["don_gia"].Value.ToString();
                txtDiem.Text = row.Cells["diem"].Value.ToString();
                txtSoLuong.Text = row.Cells["so_luong_ton"].Value.ToString();
                rtbMoTa.Text = row.Cells["mo_ta"].Value.ToString();
                
                cbbLoaiSanPham.SelectedValue = row.Cells["id_loai"].Value;

                string hinh = row.Cells["hinh"].Value != null ? row.Cells["hinh"].Value.ToString() : null;
                if (!string.IsNullOrEmpty(hinh))
                {
                    string imagePath = Path.Combine(Application.StartupPath, "Resources", hinh);
                    LoadImageFromFile(imagePath);
                }
                else
                {
                    if (pbHinhAnh.Image != null)
                    {
                        pbHinhAnh.Image.Dispose();
                        pbHinhAnh.Image = null;
                    }
                }
            }
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = openFileDialog.FileName;
                    LoadImageFromFile(selectedImagePath);
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Giải phóng ảnh trong PictureBox
                if (pbHinhAnh.Image != null)
                {
                    pbHinhAnh.Image.Dispose();
                    pbHinhAnh.Image = null;
                }

                san_pham sp = new san_pham
                {
                    id_san_pham = txtMaSanPham.Text,
                    ten_san_pham = txtTenSanPham.Text,
                    don_gia = double.Parse(txtDonGia.Text),
                    diem = double.Parse(txtDiem.Text),
                    mo_ta = rtbMoTa.Text,
                    id_loai = cbbLoaiSanPham.SelectedValue.ToString(),
                    so_luong_ton = int.Parse(txtSoLuong.Text),
                };

                // Xử lý lưu hình ảnh
                if (!string.IsNullOrEmpty(selectedImagePath))
                {
                    string fileName = Path.GetFileName(selectedImagePath);
                    string destinationPath = Path.Combine(Application.StartupPath, "Resources", fileName);

                    // Đảm bảo thư mục Resources tồn tại
                    string resourcesPath = Path.Combine(Application.StartupPath, "Resources");
                    if (!Directory.Exists(resourcesPath))
                    {
                        Directory.CreateDirectory(resourcesPath);
                    }

                    // Copy file với try-catch riêng
                    try
                    {
                        // Nếu file đã tồn tại thì không copy nữa
                        if (!File.Exists(destinationPath))
                        {
                            File.Copy(selectedImagePath, destinationPath, true);
                        }
                        sp.hinh = fileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi copy file ảnh: " + ex.Message);
                        return;
                    }
                }

                if (spbll.ThemSanPham(sp))
                {
                    MessageBox.Show("Thêm sản phẩm thành công!");
                    LoadSanPham();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Thêm sản phẩm thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                // Load lại ảnh vào PictureBox nếu cần
                if (!string.IsNullOrEmpty(selectedImagePath))
                {
                    LoadImageFromFile(selectedImagePath);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                san_pham sanPhamHienTai = spbll.GetSanPham().FirstOrDefault(s => s.id_san_pham == txtMaSanPham.Text);
                if (sanPhamHienTai == null)
                {
                    MessageBox.Show("Không tìm thấy sản phẩm để sửa!");
                    return;
                }

                san_pham sanPhamCapNhat = new san_pham
                {
                    id_san_pham = txtMaSanPham.Text,
                    ten_san_pham = txtTenSanPham.Text,
                    don_gia = double.Parse(txtDonGia.Text),
                    diem = double.Parse(txtDiem.Text),
                    mo_ta = rtbMoTa.Text,
                    id_loai = cbbLoaiSanPham.SelectedValue.ToString(),
                    so_luong_ton = int.Parse(txtSoLuong.Text),
                    hinh = sanPhamHienTai.hinh
                };

                if (!string.IsNullOrEmpty(selectedImagePath))
                {
                    string fileName = Path.GetFileName(selectedImagePath); // Lấy tên file gốc
                    string destinationPath = Path.Combine(Application.StartupPath, "Resources", fileName);
            
                    // Xóa ảnh cũ nếu khác với ảnh mới
                    if (!string.IsNullOrEmpty(sanPhamHienTai.hinh) && 
                        sanPhamHienTai.hinh != fileName)
                    {
                        string oldImagePath = Path.Combine(Application.StartupPath, "Resources", sanPhamHienTai.hinh);
                        if (File.Exists(oldImagePath))
                        {
                            try
                            {
                                File.Delete(oldImagePath);
                            }
                            catch { }
                        }
                    }

                    // Copy ảnh mới nếu chưa tồn tại
                    if (!File.Exists(destinationPath))
                    {
                        File.Copy(selectedImagePath, destinationPath, true);
                    }
                    sanPhamCapNhat.hinh = fileName;
                }

                if (spbll.SuaSanPham(sanPhamCapNhat))
                {
                    MessageBox.Show("Sửa sản phẩm thành công!");
                    LoadSanPham();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Sửa sản phẩm thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("L���i: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa sản phẩm này?", "Xác nhận", 
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (spbll.XoaSanPham(txtMaSanPham.Text))
                {
                    MessageBox.Show("Xóa sản phẩm thành công!");
                    LoadSanPham();
                    ClearInputs();
                }
                else
                    MessageBox.Show("Xóa sản phẩm thất bại!");
            }
        }

        private void ClearInputs()
        {
            txtMaSanPham.Clear();
            txtTenSanPham.Clear();
            txtDonGia.Clear();
            txtDiem.Clear();
            rtbMoTa.Clear();
            if (pbHinhAnh.Image != null)
            {
                pbHinhAnh.Image.Dispose();
                pbHinhAnh.Image = null;
            }
            cbbLoaiSanPham.SelectedIndex = -1;
            selectedImagePath = string.Empty;
        }

        private OpenFileDialog openFileDialog = new OpenFileDialog
        {
            InitialDirectory = "c:\\",
            Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
            FilterIndex = 1,
            RestoreDirectory = true
        };

        private string selectedImagePath = string.Empty;
    }
}
