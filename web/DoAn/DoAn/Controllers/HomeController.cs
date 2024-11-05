using System.Diagnostics;
using DoAn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace DoAn.Controllers
{
    public class HomeController : Controller
    {
        // Phương thức Index trả về view
        public IActionResult Index()
        {
            return View();
        }

        // Phương thức Privacy trả về view
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
