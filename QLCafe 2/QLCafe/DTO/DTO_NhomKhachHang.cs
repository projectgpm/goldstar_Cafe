using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCafe.DTO
{
    public class DTO_NhomKhachHang
    {
        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        private string tenNhomKhachHang;

        public string TenNhomKhachHang
        {
            get { return tenNhomKhachHang; }
            set { tenNhomKhachHang = value; }
        }
        public DTO_NhomKhachHang(int getID, string getTen)
        {
            this.ID = getID;
            this.TenNhomKhachHang = getTen;
        }
        public DTO_NhomKhachHang(DataRow dr)
        {
            this.ID = Int32.Parse(dr["ID"].ToString());
            this.TenNhomKhachHang = dr["TenNhomKhachHang"].ToString();
        }
    }
}
