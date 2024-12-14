using Microsoft.ML;

namespace DoAn.Models
{
    public class RecommendationService
    {
        private readonly QuanLyBanHangContext _context;

        public RecommendationService(QuanLyBanHangContext context)
        {
            _context = context;
        }

        // Lấy tất cả dữ liệu hành động người dùng từ cơ sở dữ liệu
        public List<UserActivity> GetUserActivitiesFromDb()
        {
            var userActivities = _context.UserActivities
                .Select(activity => new UserActivity
                {
                    UserId = activity.UserId,
                    ProductId = activity.ProductId,
                    Rating = activity.Rating // Hoặc có thể dùng Action (view, buy)
                }).ToList();

            return userActivities;
        }

        // Huấn luyện mô hình gợi ý
        public ITransformer TrainRecommendationModel(List<UserActivity> userActivities)
        {
            var mlContext = new MLContext();

            // Chuyển dữ liệu thành IDataView
            var trainingData = mlContext.Data.LoadFromEnumerable(userActivities);

            // Tạo pipeline huấn luyện mô hình
            var pipeline = mlContext.Recommendation().Trainers.MatrixFactorization(
                labelColumnName: nameof(UserActivity.Rating),
                matrixColumnIndexColumnName: nameof(UserActivity.UserId),
                matrixRowIndexColumnName: nameof(UserActivity.ProductId));

            // Huấn luyện mô hình
            var model = pipeline.Fit(trainingData);

            return model;
        }

        // Dự đoán các sản phẩm gợi ý
        public List<SanPham> GetRecommendations(string userId, string productId)
        {
            var mlContext = new MLContext();

            // Lấy tất cả các dữ liệu hành động của người dùng từ cơ sở dữ liệu
            var userActivities = GetUserActivitiesFromDb();

            // Huấn luyện mô hình dựa trên dữ liệu hiện có
            var model = TrainRecommendationModel(userActivities);

            // Tạo PredictionEngine
            var predictionEngine = mlContext.Model.CreatePredictionEngine<UserActivity, ProductRecommendation>(model);

            // Dự đoán các sản phẩm tương tự cho người dùng
            var predictions = new List<SanPham>();

            foreach (var activity in userActivities)
            {
                Console.WriteLine($"UserId: {activity.UserId}, ProductId: {activity.ProductId}, Rating: {activity.Rating}");

                if (activity.UserId == userId && activity.ProductId != productId)
                {
                    var prediction = predictionEngine.Predict(new UserActivity
                    {
                        UserId = userId,
                        ProductId = activity.ProductId
                    });

                    Console.WriteLine($"Predicted productId: {prediction.ProductId}");

                    // Lấy thông tin sản phẩm từ database
                    var recommendedProduct = _context.SanPhams.FirstOrDefault(p => p.IdSanPham == prediction.ProductId);

                    if (recommendedProduct != null)
                    {
                        predictions.Add(recommendedProduct);
                        Console.WriteLine($"Added product: {recommendedProduct.TenSanPham}");
                    }
                }
            }


            // Nếu không có đủ 4 sản phẩm gợi ý, bổ sung thêm sản phẩm ngẫu nhiên hoặc phổ biến
            if (predictions.Count < 4)
            {
                var additionalProducts = _context.SanPhams
                    .Where(p => !predictions.Any(r => r.IdSanPham == p.IdSanPham))
                    .Take(4 - predictions.Count)  // Lấy thêm sản phẩm để đủ 4 sản phẩm
                    .ToList();

                predictions.AddRange(additionalProducts);
            }


            return predictions.Take(4).ToList();  // Chỉ trả về tối đa 4 sản phẩm
        }


    }

}
