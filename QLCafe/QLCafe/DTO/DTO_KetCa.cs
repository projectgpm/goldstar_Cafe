using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCafe.DTO
{
    public class DTO_KetCa
    {
        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        private DateTime thoiGianBatDau;

        public DateTime ThoiGianBatDau
        {
            get { return thoiGianBatDau; }
            set { thoiGianBatDau = value; }
        }
        private DateTime thoiGianKetThuc;

        public DateTime ThoiGianKetThuc
        {
            get { return thoiGianKetThuc; }
            set { thoiGianKetThuc = value; }
        }
        
        private double tongTienSauCa;

        public double TongTienSauCa
        {
            get { return tongTienSauCa; }
            set { tongTienSauCa = value; }
        }
        private int iDNhanVien;

        public int IDNhanVien
        {
            get { return iDNhanVien; }
            set { iDNhanVien = value; }
        }
        private string tenNguoiDung;

        public string TenNguoiDung
        {
            get { return tenNguoiDung; }
            set { tenNguoiDung = value; }
        }
        public DTO_KetCa(int getID, DateTime getThoiGianBatDau, DateTime getThoiGianKetThuc, double getTongTienSauCa, int getIDNhanVien, string getTenNguoiDung)
        {
            this.ID = getID;
            this.ThoiGianBatDau =  getThoiGianBatDau;
            this.ThoiGianKetThuc = getThoiGianKetThuc;
            this.TenNguoiDung = getTenNguoiDung;
            this.TongTienSauCa = getTongTienSauCa;
            this.IDNhanVien = getIDNhanVien;
        }
        public DTO_KetCa(DataRow dr)
        {
            this.ID = Int32.Parse(dr["ID"].ToString());
            this.ThoiGianBatDau = DateTime.Parse(dr["ThoiGianBatDau"].ToString());
            this.ThoiGianKetThuc = DateTime.Parse(dr["ThoiGianKetThuc"].ToString());
            this.TenNguoiDung = dr["TenNguoiDung"].ToString();
            this.TongTienSauCa = double.Parse(dr["TongTienSauCa"].ToString());
            this.IDNhanVien = Int32.Parse(dr["IDNhanVien"].ToString());
        }
    }
}
