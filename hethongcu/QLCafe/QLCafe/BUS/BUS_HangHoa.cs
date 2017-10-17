using QLCafe.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCafe.BUS
{
    class BUS_HangHoa
    {
        public static DataTable DSHangHoa(string IDNhom)
        {
            return DAO_HangHoa.DanhSachHangHoa(IDNhom);
        }
        public static DataTable DSHangHoa_Full()
        {
            return DAO_HangHoa.DanhSachHangHoa_Full();
        }
    }
}
