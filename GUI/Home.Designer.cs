namespace GUI
{
    partial class Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripTongQuan = new System.Windows.Forms.ToolStripButton();
            this.toolStripSanPham = new System.Windows.Forms.ToolStripButton();
            this.toolStripKhachHang = new System.Windows.Forms.ToolStripButton();
            this.toolStripNhanVien = new System.Windows.Forms.ToolStripButton();
            this.toolStripHoaDon = new System.Windows.Forms.ToolStripButton();
            this.toolStripThongKe = new System.Windows.Forms.ToolStripButton();
            this.toolStripThoat = new System.Windows.Forms.ToolStripButton();
            this.panelContent = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTongQuan,
            this.toolStripSanPham,
            this.toolStripKhachHang,
            this.toolStripNhanVien,
            this.toolStripHoaDon,
            this.toolStripThongKe,
            this.toolStripThoat});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(984, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripTongQuan
            // 
            this.toolStripTongQuan.Image = ((System.Drawing.Image)(resources.GetObject("toolStripTongQuan.Image")));
            this.toolStripTongQuan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripTongQuan.Name = "toolStripTongQuan";
            this.toolStripTongQuan.Size = new System.Drawing.Size(84, 22);
            this.toolStripTongQuan.Text = "Tổng quan";
            this.toolStripTongQuan.Click += new System.EventHandler(this.toolStripTongQuan_Click);
            // 
            // toolStripSanPham
            // 
            this.toolStripSanPham.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSanPham.Image")));
            this.toolStripSanPham.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSanPham.Name = "toolStripSanPham";
            this.toolStripSanPham.Size = new System.Drawing.Size(80, 22);
            this.toolStripSanPham.Text = "Sản phẩm";
            this.toolStripSanPham.Click += new System.EventHandler(this.toolStripSanPham_Click);
            // 
            // toolStripKhachHang
            // 
            this.toolStripKhachHang.Image = ((System.Drawing.Image)(resources.GetObject("toolStripKhachHang.Image")));
            this.toolStripKhachHang.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripKhachHang.Name = "toolStripKhachHang";
            this.toolStripKhachHang.Size = new System.Drawing.Size(90, 22);
            this.toolStripKhachHang.Text = "Khách hàng";
            this.toolStripKhachHang.Click += new System.EventHandler(this.toolStripKhachHang_Click);
            // 
            // toolStripNhanVien
            // 
            this.toolStripNhanVien.Image = ((System.Drawing.Image)(resources.GetObject("toolStripNhanVien.Image")));
            this.toolStripNhanVien.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripNhanVien.Name = "toolStripNhanVien";
            this.toolStripNhanVien.Size = new System.Drawing.Size(81, 22);
            this.toolStripNhanVien.Text = "Nhân viên";
            this.toolStripNhanVien.Click += new System.EventHandler(this.toolStripNhanVien_Click);
            // 
            // toolStripHoaDon
            // 
            this.toolStripHoaDon.Image = ((System.Drawing.Image)(resources.GetObject("toolStripHoaDon.Image")));
            this.toolStripHoaDon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripHoaDon.Name = "toolStripHoaDon";
            this.toolStripHoaDon.Size = new System.Drawing.Size(79, 22);
            this.toolStripHoaDon.Text = "Đơn hàng";
            this.toolStripHoaDon.Click += new System.EventHandler(this.toolStripHoaDon_Click);
            // 
            // toolStripThongKe
            // 
            this.toolStripThongKe.Image = ((System.Drawing.Image)(resources.GetObject("toolStripThongKe.Image")));
            this.toolStripThongKe.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripThongKe.Name = "toolStripThongKe";
            this.toolStripThongKe.Size = new System.Drawing.Size(86, 22);
            this.toolStripThongKe.Text = "Nhập hàng";
            this.toolStripThongKe.Click += new System.EventHandler(this.toolStripThongKe_Click);
            // 
            // toolStripThoat
            // 
            this.toolStripThoat.Image = ((System.Drawing.Image)(resources.GetObject("toolStripThoat.Image")));
            this.toolStripThoat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripThoat.Name = "toolStripThoat";
            this.toolStripThoat.Size = new System.Drawing.Size(57, 22);
            this.toolStripThoat.Text = "Thoát";
            this.toolStripThoat.Click += new System.EventHandler(this.toolStripThoat_Click);
            // 
            // panelContent
            // 
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 25);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(984, 636);
            this.panelContent.TabIndex = 1;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripTongQuan;
        private System.Windows.Forms.ToolStripButton toolStripSanPham;
        private System.Windows.Forms.ToolStripButton toolStripKhachHang;
        private System.Windows.Forms.ToolStripButton toolStripNhanVien;
        private System.Windows.Forms.ToolStripButton toolStripHoaDon;
        private System.Windows.Forms.ToolStripButton toolStripThongKe;
        private System.Windows.Forms.ToolStripButton toolStripThoat;
        private System.Windows.Forms.Panel panelContent;

    }
}