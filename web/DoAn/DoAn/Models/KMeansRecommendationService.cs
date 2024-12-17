using Accord.MachineLearning;
using Accord.Statistics.Filters;
using DoAn.Models;
using System.Data;
using System.Linq;

namespace DoAn.Models
{
    public class KMeansRecommendationService
    {
        private readonly QuanLyBanHangContext _context;
        private KMeansClusterCollection _clusters;
        private Codification _codebook;

        public KMeansRecommendationService(QuanLyBanHangContext context)
        {
            _context = context;
            // Huấn luyện mô hình khi khởi động
            if (_clusters == null)
            {
                TrainKMeansModel();
            }
        }

        // Huấn luyện mô hình KMeans
        private void TrainKMeansModel()
        {
            var data = _context.SanPhamMuaKems
                .Select(p => new
                {
                    p.IdSanPham,
                    p.IdSanPhamKem,
                    p.DonGia,
                    p.Rating
                })
                .ToList();

            var dataTable = new DataTable();
            dataTable.Columns.Add("IdSanPham", typeof(string));
            dataTable.Columns.Add("IdSanPhamKem", typeof(string));
            dataTable.Columns.Add("DonGia", typeof(double));
            dataTable.Columns.Add("Rating", typeof(double));

            foreach (var item in data)
            {
                dataTable.Rows.Add(item.IdSanPham, item.IdSanPhamKem, item.DonGia, item.Rating);
            }

            _codebook = new Codification(dataTable);

            double[][] features = new double[dataTable.Rows.Count][];
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                features[i] = new double[]
                {
                    Convert.ToDouble(dataTable.Rows[i]["DonGia"]),
                    Convert.ToDouble(dataTable.Rows[i]["Rating"])
                };
            }

            var kmeans = new KMeans(k: 30); // Số cụm k
            _clusters = kmeans.Learn(features);

            // Dán nhãn cụm lại cho dữ liệu
            int[] labels = _clusters.Decide(features);

        }

        // Tìm sản phẩm mua kèm thuộc cùng cụm
        public List<string> RecommendProducts(string idsp)
        {
            // Bước 1: Lấy thông tin sản phẩm từ bảng SanPham
            var product = _context.SanPhams
                .Where(p => p.IdSanPham == idsp)
                .Select(p => new { p.DonGia, p.Diem })
                .FirstOrDefault();

            if (product == null)
            {
                return new List<string>(); // Không tìm thấy sản phẩm
            }

            // Bước 2: Dự đoán cụm của sản phẩm
            double[] productFeatures = new double[] { product.DonGia, product.Diem };
            int productCluster = _clusters.Decide(productFeatures);

            // Bước 3: Tìm các sản phẩm mua kèm thuộc cùng cụm
            var productKems = _context.SanPhamMuaKems
                .ToList() // Đưa toàn bộ dữ liệu vào bộ nhớ
                .Where(p =>
                {
                    double[] features = new double[] { p.DonGia, p.Rating };
                    int cluster = _clusters.Decide(features);
                    return cluster == productCluster;
                })
                .Select(p => p.IdSanPhamKem)
                .Distinct()
                .Take(4)
                .ToList();

            return productKems;
        }
    }
}
