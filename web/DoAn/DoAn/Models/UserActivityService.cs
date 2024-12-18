namespace DoAn.Models
{
    public class UserActivityService
    {
        // Danh sách tĩnh lưu trữ các hoạt động của người dùng
        private static List<UserActivity> _userActivities = new List<UserActivity>();

        // Thêm một hoạt động người dùng vào bộ nhớ
        public void AddUserActivity(string userId, string productId, string action)
        {
            _userActivities.Add(new UserActivity
            {
                UserId = userId,
                ProductId = productId,
                Action = action,
                Timestamp = DateTime.Now
            });
        }

        // Lấy các hoạt động của người dùng từ bộ nhớ
        public List<UserActivity> GetUserActivitiesFromMemory(string userId)
        {
            // Lọc các hoạt động của người dùng theo UserId
            return _userActivities.Where(u => u.UserId == userId).ToList();
        }

        // Lấy tất cả các hoạt động từ bộ nhớ
        public List<UserActivity> GetAllUserActivities()
        {
            return _userActivities;
        }
        public List<UserActivity> GetUserActivities(string userId)
        {
            return _userActivities.Where(u => u.UserId == userId).ToList();
        }
    }

}
