using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCafe.DTO
{
    public class DTO_DanhSachMenu
    {
        private int inPhaChe;

        public int InPhaChe
        {
            get { return inPhaChe; }
            set { inPhaChe = value; }
        }
        private int iDMayIn;

        public int IDMayIn
        {
            get { return iDMayIn; }
            set { iDMayIn = value; }
        }
        private double thanhTien;

        public double ThanhTien
        {
            get { return thanhTien; }
            set { thanhTien = value; }
        }

        private double donGia;

        public double DonGia
        {
            get { return donGia; }
            set { donGia = value; }
        }
        private int soLuong;

        public int SoLuong
        {
            get { return soLuong; }
            set { soLuong = value; }
        }
        private string donViTinh;

        public string DonViTinh
        {
            get { return donViTinh; }
            set { donViTinh = value; }
        }
        private string tenHangHoa;

        public string TenHangHoa
        {
            get { return tenHangHoa; }
            set { tenHangHoa = value; }
        }
        private string maHangHoa;

        public string MaHangHoa
        {
            get { return maHangHoa; }
            set { maHangHoa = value; }
        }
        private double trongLuong;

        public double TrongLuong
        {
            get { return trongLuong; }
            set { trongLuong = value; }
        }
        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private int trangThai;

        public int TrangThai
        {
            get { return trangThai; }
            set { trangThai = value; }
        }

        // Bổ sung.
        private string ghiChu;

        public string GhiChu
        {
            get { return ghiChu; }
            set { ghiChu = value; }
        }

        public DTO_DanhSachMenu(string getMaHangHoa, string getTenHangHoa, string getTenDonViTinh, int getSL, double getDonGia, double getThanhTien, double getTrongLuong, int getID, int getTrangThai, string getGhiChu, int getIDMayIn, int getInPhaChe)
        {
            this.MaHangHoa = getMaHangHoa;
            this.TenHangHoa = getTenHangHoa;
            this.DonViTinh = getTenDonViTinh;
            this.SoLuong = getSL;
            this.DonGia = getDonGia;
            this.ThanhTien = getThanhTien;
            this.TrongLuong = getTrongLuong;
            this.ID = getID;
            this.TrangThai = getTrangThai;
            this.IDMayIn = getIDMayIn;
            this.InPhaChe = getInPhaChe;
            // Bổ sung.
            this.GhiChu = getGhiChu;
        }
        public DTO_DanhSachMenu(DataRow dr)
        {
            this.MaHangHoa = dr["MaHangHoa"].ToString();
            this.TenHangHoa = dr["TenHangHoa"].ToString();
            this.DonViTinh = dr["TenDonViTinh"].ToString();
            this.SoLuong = Int32.Parse(dr["SoLuong"].ToString());
            this.DonGia = double.Parse(dr["DonGia"].ToString());
            this.ThanhTien = double.Parse(dr["ThanhTien"].ToString());
            this.TrongLuong = double.Parse(dr["TrongLuong"].ToString());
            this.ID = Int32.Parse(dr["ID"].ToString());
            this.TrangThai = Int32.Parse(dr["TrangThai"].ToString());
            this.IDMayIn = Int32.Parse(dr["IDMayIn"].ToString());
            this.InPhaChe = Int32.Parse(dr["InPhaChe"].ToString());
            // Bổ sung.
            this.GhiChu = dr["GhiChu"].ToString();
        }
    }
}
