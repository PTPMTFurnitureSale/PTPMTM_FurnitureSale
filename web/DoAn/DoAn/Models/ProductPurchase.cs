using Microsoft.ML.Data;

namespace DoAn.Models
{
    public class ProductPurchase
    {
        [LoadColumn(0)] public string IdSanPham { get; set; }
        [LoadColumn(1)] public string IdSanPhamKem { get; set; }
        [LoadColumn(2)] public double Rating { get; set; }
    }
}
