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
        private float giaTong;

        public float GiaTong
        {
            get { return giaTong; }
            set { giaTong = value; }
        }
        private float phuThuKhuVuc;

        public float PhuThuKhuVuc
        {
            get { return phuThuKhuVuc; }
            set { phuThuKhuVuc = value; }
        }
        private float phuThuGio;

        public float PhuThuGio
        {
            get { return phuThuGio; }
            set { phuThuGio = value; }
        }
        private float thanhTien;

        public float ThanhTien
        {
            get { return thanhTien; }
            set { thanhTien = value; }
        }

        private float donGia;

        public float DonGia
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
        public DTO_DanhSachMenu(string getMaHangHoa, string getTenHangHoa, string getTenDonViTinh, int getSL, float getDonGia, float getThanhTien, float getgio, float getkhuvuc, float getgiatong)
        {
            this.MaHangHoa = getMaHangHoa;
            this.TenHangHoa = getTenHangHoa;
            this.DonViTinh = getTenDonViTinh;
            this.SoLuong = getSL;
            this.DonGia = getDonGia;
            this.ThanhTien = getThanhTien;
            this.PhuThuGio = getgio;
            this.PhuThuKhuVuc = getkhuvuc;
            this.GiaTong = getgiatong;
        }
        public DTO_DanhSachMenu(DataRow dr)
        {
            this.MaHangHoa = dr["MaHangHoa"].ToString();
            this.TenHangHoa = dr["TenHangHoa"].ToString();
            this.DonViTinh = dr["TenDonViTinh"].ToString();
            this.SoLuong = Int32.Parse(dr["SoLuong"].ToString());
            this.DonGia = float.Parse(dr["DonGia"].ToString());
            this.ThanhTien = float.Parse(dr["ThanhTien"].ToString());
            this.PhuThuGio = float.Parse(dr["PhuThuGio"].ToString());
            this.PhuThuKhuVuc = float.Parse(dr["PhuThuKhuVuc"].ToString());
            this.GiaTong = float.Parse(dr["GiaTong"].ToString());
        }
    }
}
