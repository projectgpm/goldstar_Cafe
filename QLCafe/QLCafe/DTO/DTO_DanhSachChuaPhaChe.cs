using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCafe.DTO
{
    public class DTO_DanhSachChuaPhaChe
    {
        private int iD;

        public int ID
        {
          get { return iD; }
          set { iD = value; }
        }

        private string maHangHoa;

        public string MaHangHoa
        {
            get { return maHangHoa; }
            set { maHangHoa = value; }
        }
        private string tenHangHoa;

        public string TenHangHoa
        {
            get { return tenHangHoa; }
            set { tenHangHoa = value; }
        }
        private string donViTinh;

        public string DonViTinh
        {
            get { return donViTinh; }
            set { donViTinh = value; }
        }
        private int soLuong;

        public int SoLuong
        {
            get { return soLuong; }
            set { soLuong = value; }
        }

        private string tenBan;

        public string TenBan
        {
            get { return tenBan; }
            set { tenBan = value; }
        }

        private int trangThaiPhaChe;

        public int TrangThaiPhaChe
        {
            get { return trangThaiPhaChe; }
            set { trangThaiPhaChe = value; }
        }

        public DTO_DanhSachChuaPhaChe(int getID, string getMaHangHoa, string getTenHangHoa, string getTenDonViTinh, int getSoLuong, string getTenBan, int getTrangThaiPhaChe)
        {
            this.ID = getID;
            this.MaHangHoa = getMaHangHoa;
            this.TenHangHoa = getTenHangHoa;
            this.DonViTinh = getTenDonViTinh;
            this.SoLuong = getSoLuong;
            this.TenBan = getTenBan;
            this.TrangThaiPhaChe = getTrangThaiPhaChe;
        }
        public DTO_DanhSachChuaPhaChe(DataRow dr)
        {
            this.ID = Int32.Parse(dr["ID"].ToString());
            this.MaHangHoa = dr["MaHangHoa"].ToString();
            this.TenHangHoa = dr["TenHangHoa"].ToString();
            this.DonViTinh = dr["TenDonViTinh"].ToString();
            this.SoLuong = Int32.Parse(dr["SoLuong"].ToString());
            this.TenBan = dr["TenBan"].ToString();
            this.TrangThaiPhaChe = Int32.Parse(dr["TrangThai"].ToString());
        }
    }
}
