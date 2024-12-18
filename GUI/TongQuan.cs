using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Linq;
using BLL;
using DTO;
using System.Drawing;

namespace GUI
{
    public partial class TongQuan : UserControl
    {
        QuanLyBanHangDataContext qlbh = new QuanLyBanHangDataContext();

        public TongQuan()
        {
            InitializeComponent();
            LoadChart();
            LoadThongKe();
        }

        private void LoadChart()
        {
            try
            {
                // Lấy dữ liệu
                var query = from hd in qlbh.hoa_dons
                           where hd.ngay_dat.Value.Year == DateTime.Now.Year
                           group hd by new
                           {
                               Thang = hd.ngay_dat.Value.Month,
                               Nam = hd.ngay_dat.Value.Year
                           } into g
                           orderby g.Key.Nam, g.Key.Thang
                           select new
                           {
                               Thang = g.Key.Thang,
                               DoanhThu = g.Sum(x => x.tong_tien)
                           };

                // Cấu hình cơ bản
                chart1.Series.Clear();
                chart1.BackColor = Color.White;
                chart1.AntiAliasing = AntiAliasingStyles.All;
                chart1.TextAntiAliasingQuality = TextAntiAliasingQuality.High;

                // Tạo series
                Series series = new Series("Doanh Thu");
                series.ChartType = SeriesChartType.Column;
                series.BorderWidth = 0;

                // Màu gradient cho cột
                Color primaryColor = Color.FromArgb(65, 140, 240);
                Color secondaryColor = Color.FromArgb(252, 180, 65);

                foreach (var item in query)
                {
                    string label = "Tháng " + item.Thang.ToString();
                    double value = Convert.ToDouble(item.DoanhThu);
                    DataPoint point = new DataPoint();
                    point.SetValueXY(label, value);
                    point.Color = primaryColor;
                    point.BackSecondaryColor = secondaryColor;
                    point.BackGradientStyle = GradientStyle.VerticalCenter;
                    series.Points.Add(point);
                }

                chart1.Series.Add(series);

                // Cấu hình ChartArea
                ChartArea chartArea = chart1.ChartAreas[0];
                chartArea.BackColor = Color.White;
                chartArea.BorderColor = Color.White;
                chartArea.BorderWidth = 0;
                
                // Đổ bóng nhẹ
                chartArea.ShadowColor = Color.FromArgb(10, 0, 0, 0);
                chartArea.ShadowOffset = 3;

                // Định dạng lưới
                chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(235, 235, 235);
                chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(235, 235, 235);
                chartArea.AxisX.MinorGrid.Enabled = false;
                chartArea.AxisY.MinorGrid.Enabled = false;

                // Font styles
                Font labelFont = new Font("Segoe UI", 9f);
                Font titleFont = new Font("Segoe UI Semibold", 11f);

                // Định dạng trục X
                chartArea.AxisX.LabelStyle.Font = labelFont;
                chartArea.AxisX.LineColor = Color.FromArgb(235, 235, 235);
                chartArea.AxisX.LabelStyle.ForeColor = Color.FromArgb(100, 100, 100);
                chartArea.AxisX.Interval = 1;

                // Định dạng trục Y
                chartArea.AxisY.LabelStyle.Font = labelFont;
                chartArea.AxisY.LineColor = Color.FromArgb(235, 235, 235);
                chartArea.AxisY.LabelStyle.ForeColor = Color.FromArgb(100, 100, 100);
                chartArea.AxisY.LabelStyle.Format = "N0";
                
                // Vị trí và kích thước
                chartArea.Position.Auto = false;
                chartArea.Position.X = 5;
                chartArea.Position.Y = 15;
                chartArea.Position.Width = 90;
                chartArea.Position.Height = 75;

                // Tiêu đề
                chart1.Titles.Clear();
                Title title = new Title();
                title.Text = "THỐNG KÊ DOANH THU NĂM " + DateTime.Now.Year.ToString();
                title.Font = new Font("Segoe UI", 14f, FontStyle.Bold);
                title.ForeColor = Color.FromArgb(50, 50, 50);
                title.Alignment = ContentAlignment.TopCenter;
                title.DockedToChartArea = chartArea.Name;
                title.IsDockedInsideChartArea = false;
                chart1.Titles.Add(title);

                // Hiển thị giá trị
                series.IsValueShownAsLabel = true;
                series.LabelFormat = "N0";
                series.Font = new Font("Segoe UI", 9f);
                series.LabelForeColor = Color.FromArgb(100, 100, 100);

                // Độ rộng cột và khoảng cách
                series["PointWidth"] = "0.5";
                series["PixelPointWidth"] = "30";

                // Tooltip
                series.ToolTip = "Tháng #VALX\nDoanh thu: #VALY{N0} VNĐ";

                // Legend
                chart1.Legends[0].Enabled = false;

                // Margin
                chart1.Margin = new Padding(10);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Có thể xóa method này nếu không sử dụng
        }

        private void LoadThongKe()
        {
            try
            {
                // Đếm số lượng khách hàng
                int soKhachHang = qlbh.khach_hangs.Count();
                lbKhachHang.Text = soKhachHang.ToString();

                // Đếm số lượng sản phẩm
                int soSanPham = qlbh.san_phams.Count();
                lbSanPham.Text = soSanPham.ToString();

                // Đếm số lượng đơn hàng
                int soDonHang = qlbh.hoa_dons.Count();
                lbDonHang.Text = soDonHang.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thống kê: " + ex.Message);
            }
        }
    }
}
