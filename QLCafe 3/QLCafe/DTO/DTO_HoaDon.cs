using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCafe.DTO
{
    public class DTO_HoaDon
    {
        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        private DateTime? gioVao;

        public DateTime? GioVao
        {
            get { return gioVao; }
            set { gioVao = value; }
        }
        private DateTime? gioRa;

        public DateTime? GioRa
        {
            get { return gioRa; }
            set { gioRa = value; }
        }
        private int iDBan;

        public int IDBan
        {
            get { return iDBan; }
            set { iDBan = value; }
        }
        private int trangThai;

        public int TrangThai
        {
            get { return trangThai; }
            set { trangThai = value; }
        }
        private int iDKhachHang;

        public int IDKhachHang
        {
            get { return iDKhachHang; }
            set { iDKhachHang = value; }
        }
        private string maHoaDon;

        public string MaHoaDon
        {
            get { return maHoaDon; }
            set { maHoaDon = value; }
        }
        private int iDNhanVien;

        public int IDNhanVien
        {
            get { return iDNhanVien; }
            set { iDNhanVien = value; }
        }
        private double tongTien;

        public double TongTien
        {
            get { return tongTien; }
            set { tongTien = value; }
        }
        private double giamGia;

        public double GiamGia
        {
            get { return giamGia; }
            set { giamGia = value; }
        }
        private double khachCanTra;

        public double KhachCanTra
        {
            get { return khachCanTra; }
            set { khachCanTra = value; }
        }
        private double khachThanhToan;

        public double KhachThanhToan
        {
            get { return khachThanhToan; }
            set { khachThanhToan = value; }
        }
        private double tienThua;

        public double TienThua
        {
            get { return tienThua; }
            set { tienThua = value; }
        }
        private double tienGio;

        public double TienGio
        {
            get { return tienGio; }
            set { tienGio = value; }
        }
        public DTO_HoaDon(int getid, DateTime? getgiovao, DateTime? getgiora, int getidban, int gettrangthai, double getTienGio, string getmahoadon, int getidnhanvien, double gettongtien, double getgiamgia, double getkhachcantra, double getkhachthanhtoan, double gettienthua)
        {
            this.ID = getid;
            this.GioRa = getgiora;
            this.GioVao = getgiovao;
            this.IDBan = getidban;
            this.TrangThai = gettrangthai;
            this.TienGio = getTienGio;
            this.MaHoaDon = getmahoadon;
            this.IDNhanVien = getidnhanvien;
            this.TongTien = gettongtien;
            this.GiamGia = getgiamgia;
            this.KhachCanTra = getkhachcantra;
            this.KhachThanhToan = getkhachthanhtoan;
            this.TienThua = gettienthua;
        }
        public DTO_HoaDon(DataRow dr)
        {
            this.ID = (int)dr["ID"];

            var KT = dr["GioRa"];
            if(KT.ToString() != "")
                this.GioRa = (DateTime?)KT;
            this.GioVao = (DateTime?)dr["GioVao"];
            this.IDBan = (int)dr["IDBan"];
            this.TrangThai = (int)dr["TrangThai"];
            var MaHD = dr["MaHoaDon"];
            if(MaHD.ToString() !="")
                this.MaHoaDon = MaHD.ToString();
            var IDNV = dr["IDNhanVien"];
            if(IDNV.ToString() !="")
                this.IDNhanVien = (int)IDNV;
            this.TongTien = double.Parse(dr["TongTien"].ToString());
            this.GiamGia = double.Parse(dr["GiamGia"].ToString());
            this.KhachCanTra = double.Parse(dr["KhachCanTra"].ToString());
            this.KhachThanhToan = double.Parse(dr["KhachThanhToan"].ToString());
            this.TienThua = double.Parse(dr["TienThua"].ToString());
            this.TienGio = double.Parse(dr["TienGio"].ToString());
        }
    }
}
