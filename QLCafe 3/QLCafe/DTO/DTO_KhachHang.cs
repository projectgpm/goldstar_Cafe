using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCafe.DTO
{
    public class DTO_KhachHang
    {
        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        private string maKhachHang;

        public string MaKhachHang
        {
            get { return maKhachHang; }
            set { maKhachHang = value; }
        }
        private string tenKhachHang;

        public string TenKhachHang
        {
            get { return tenKhachHang; }
            set { tenKhachHang = value; }
        }
        private string dienThoai;

        public string DienThoai
        {
            get { return dienThoai; }
            set { dienThoai = value; }
        }
        private string cMND;

        public string CMND
        {
            get { return cMND; }
            set { cMND = value; }
        }
        private double diemTichLuy;

        public double DiemTichLuy
        {
            get { return diemTichLuy; }
            set { diemTichLuy = value; }
        }

        public DTO_KhachHang(int getId, string getmakhachhang, string gettenkhachhang, string getdienthoai, string getCMND, double getdiemtichluy)
        {
            this.ID = getId;
            this.MaKhachHang = getmakhachhang;
            this.TenKhachHang = gettenkhachhang;
            this.DienThoai = getdienthoai;
            this.CMND = getCMND;
            this.DiemTichLuy = getdiemtichluy;
        }
        public DTO_KhachHang(DataRow dr)
        {
            this.ID = Int32.Parse(dr["ID"].ToString());
            this.MaKhachHang = dr["MaKhachHang"].ToString();
            this.TenKhachHang = dr["TenKhachHang"].ToString();
            this.DienThoai = dr["DienThoai"].ToString();
            this.CMND = dr["CMND"].ToString();
            this.DiemTichLuy = double.Parse(dr["DiemTichLuy"].ToString());
        }
    }
}
