using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;

namespace GUI
{
    public partial class ThongKeDoanhThu : UserControl
    {
        QuanLyBanHangDataContext qlbh = new QuanLyBanHangDataContext();
        public ThongKeDoanhThu()
        {
            InitializeComponent();
        }
    }
}
