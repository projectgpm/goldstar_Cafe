using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCafe.DTO
{
    public class DTO_DatBan
    {
        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
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
        private DateTime gioDat;

        public DateTime GioDat
        {
            get { return gioDat; }
            set { gioDat = value; }
        }
        private int iDBan;

        public int IDBan
        {
            get { return iDBan; }
            set { iDBan = value; }
        }
        private string soNguoi;

        public string SoNguoi
        {
            get { return soNguoi; }
            set { soNguoi = value; }
        }
        public DTO_DatBan(int getiD, string getTenKhachHang, string getDienThoai, DateTime getGioDat, int getIDBan, string getSoNguoi)
        {
            this.ID = getiD;
            this.TenKhachHang = getTenKhachHang;
            this.DienThoai = getDienThoai;
            this.GioDat = getGioDat;
            this.IDBan = getIDBan;
            this.SoNguoi = getSoNguoi;
        }
        public DTO_DatBan(DataRow dr)
        {
            this.ID = (int)dr["ID"];
            this.TenKhachHang = dr["TenKhachHang"].ToString();
            this.DienThoai = dr["DienThoai"].ToString();
            this.GioDat = DateTime.Parse(dr["GioDat"].ToString());
            this.IDBan = (int)dr["IDBan"];
            this.SoNguoi = dr["SoNguoi"].ToString();
        }
    }
}
