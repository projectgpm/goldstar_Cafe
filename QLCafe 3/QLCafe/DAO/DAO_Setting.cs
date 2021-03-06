﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QLCafe.DAO
{
    class DAO_Setting
    {
        public static double LayTienQuiDoiDiem()
        {
            string sTruyVan = string.Format(@"SELECT SoTienTichLuy FROM [GPM_Setting] ");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return double.Parse(dr["SoTienTichLuy"].ToString());
            }
            else
            {
                return 0;
            }
        }
        public static double LayDiemHienTai(string IDKhachHang)
        {
            string sTruyVan = string.Format(@"SELECT DiemTichLuy FROM [GPM_KhachHang] WHERE [ID] = '{0}'", IDKhachHang);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return double.Parse(dr["DiemTichLuy"].ToString());
            }
            else
            {
                return 0;
            }
        }
        public static double LayDiemQuyDoiTien()
        {
            string sTruyVan = string.Format(@"SELECT SoTienQuyDoi FROM [GPM_Setting] ");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return double.Parse(dr["SoTienQuyDoi"].ToString());
            }
            else
            {
                return 0;
            }
        }
        public static double LayTienPhuThu(double TongTienHienTai)
        {
            //0 Phu thu $, 1 Phụ thu %, 2 Không tính phụ thu;
            string sTruyVan = string.Format(@"SELECT PhuThuCaFe_ApDung FROM [Setting] WHERE ID = 1 ");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            int PhuThuCaFe_ApDung = 0;
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                PhuThuCaFe_ApDung = Int32.Parse(dr["PhuThuCaFe_ApDung"].ToString());
            }
            else
            {
                PhuThuCaFe_ApDung = 2;
            }

           switch (PhuThuCaFe_ApDung)
           {
                case 0: // phụ thu tiền
                   sTruyVan = string.Format(@"SELECT PhuThuCaFe FROM [Setting] WHERE ID = 1 ");
                   data = new DataTable();
                   data = DataProvider.TruyVanLayDuLieu(sTruyVan);
                    if (data.Rows.Count > 0)
                    {
                        DataRow dr = data.Rows[0];
                        return double.Parse(dr["PhuThuCaFe"].ToString());
                        break;
                    }
                    else
                    {
                        return 0;
                    }
                   break;
               case 1:// phụ thu %
                   sTruyVan = string.Format(@"SELECT PhuThuCaFe_PhanTram FROM [Setting] WHERE ID = 1 ");
                   data = new DataTable();
                   data = DataProvider.TruyVanLayDuLieu(sTruyVan);
                    if (data.Rows.Count > 0)
                    {
                        DataRow dr = data.Rows[0];
                        double TylePhanTram =  double.Parse(dr["PhuThuCaFe_PhanTram"].ToString());
                        return TongTienHienTai * (TylePhanTram/100);
                        break;
                    }
                    else
                    {
                        return 0;
                    }
                   break;
               case 2:// không phụ thu
                   return 0;
                   break;
               default:
                   return 0;
                   break;



           }
        }
        public static double DiemTichLuy(string IDKhachHang)
        {
            string sTruyVan = string.Format(@"SELECT DiemTichLuy FROM [GPM_KhachHang] WHERE [ID] = {0}", IDKhachHang);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return double.Parse(dr["DiemTichLuy"].ToString());
            }
            else
            {
                return 0;
            }
        }
        public static int ThoiGianPhaChe()
        {
            string sTruyVan = string.Format(@"SELECT LamMoiPhaChe FROM [Setting] ");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return Int32.Parse(dr["LamMoiPhaChe"].ToString());
            }
            else
            {
                return 60;
            }
        }
        public static int LayIDMayIn(string IDHangHoa)
        {
            string sTruyVan = string.Format(@"SELECT LamMoiPhaChe FROM [Setting] ");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return Int32.Parse(dr["LamMoiPhaChe"].ToString());
            }
            else
            {
                return 60;
            }
        }
        public static int setKeyCode(string Key, string user)
        {
            if (Key.CompareTo("gpm686970") == 0)
            {
                string sx = GetHardDiskSerialNo();
                string strAddress = sx + "GPM2017";
                string sha1Address = GetSHA1HashData(strAddress);
                string sTruyVan = string.Format(@"INSERT INTO  [CF_KeyCode] (GetKey,NgayKichHoat) VALUES('{0}',getdate())", sha1Address);
                DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
                return 1;
            }
            return -1;

        }
        public static int getData_Setting(string CD)
        {
            string sTruyVan = string.Format(@"SELECT GetKey FROM [CF_KeyCode] WHERE GetKey  = '" + CD + "'");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                return 1;
            }
            else
                return 0;
        }
        public static string GetHardDiskSerialNo()
        {


            return System.Environment.MachineName;
            //string drive = "C";
            //ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"" + drive + ":\"");
            //disk.Get();
            //return disk["VolumeSerialNumber"].ToString();
        }
        public static int getKeyCode()
        {
            string sx = GetHardDiskSerialNo();
            string strAddress = sx + "GPM2017";
            string sha1Address = GetSHA1HashData(strAddress);
            if (getData_Setting(sha1Address) == 1)
                return 1;
            return -1;
        }
        public static bool IsNumber(string pValue)
        {
            foreach (Char c in pValue)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
        public static string GetSHA1HashData(string data)
        {
            SHA1 sha1 = SHA1.Create();
            byte[] hashData = sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(data + 123));
            System.Text.StringBuilder returnValue = new System.Text.StringBuilder();
            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString("x"));
            }
            return returnValue.ToString();
        }
        public static string convertDauSangKhongDau(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').ToUpper();
        }

        public static int LayIDHangHoa(string MaHangHoa)
        {
            string sTruyVan = string.Format(@"SELECT ID FROM [CF_HangHoa] WHERE [MaHangHoa] = {0} ", MaHangHoa);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return Int32.Parse(dr["ID"].ToString());
            }
            else
                return 0;
        }
        public static int LayIDHangHoaTuChon(string MaNguyenLieu)
        {
            string sTruyVan = string.Format(@"SELECT ID FROM [CF_NguyenLieu] WHERE [MaNguyenLieu] = {0} ", MaNguyenLieu);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return Int32.Parse(dr["ID"].ToString());
            }
            else
                return 0;
        }
        public static int TrangThaiTinhGio()
        {
            string sTruyVan = string.Format(@"SELECT TinhGio FROM [Setting]");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return Int32.Parse(dr["TinhGio"].ToString());
            }
            else
                return 0;
        }
        public static double LayGiaBanTheoKhuVuc(int IDBan)
        {
            string sTruyVan = string.Format(@"SELECT [CF_KhuVuc].GiaKhuVuc FROM [CF_Ban],[CF_KhuVuc] WHERE [CF_KhuVuc].ID =  [CF_Ban].IDKhuVuc AND [CF_Ban].ID = {0} ", IDBan);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return double.Parse(dr["GiaKhuVuc"].ToString());
            }
            else
                return 0;
        }
        public static bool TruTonKho(int IDNguyenLieu, string IDChiNhanh, double TrongLuong)
        {
            string sTruyVan = string.Format(@"UPDATE [CF_TonKho] SET  [TrongLuong] = [TrongLuong] - {0} WHERE IDNguyenLieu = {1} AND [IDChiNhanh] = {2}", TrongLuong, IDNguyenLieu, IDChiNhanh);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }

        public static int LayIDDonViTinh(string MaHangHoa)
        {
            string sTruyVan = string.Format(@"SELECT IDDonViTinh FROM [CF_HangHoa] WHERE [MaHangHoa] = {0} ", MaHangHoa);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return Int32.Parse(dr["IDDonViTinh"].ToString());
            }
            else
                return 0;
        }
        public static int LayIDDonViTinhTuChon(string MaNguyenLieu)
        {
            string sTruyVan = string.Format(@"SELECT IDDonViTinh FROM [CF_NguyenLieu] WHERE [MaNguyenLieu] = {0} ", MaNguyenLieu);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return Int32.Parse(dr["IDDonViTinh"].ToString());
            }
            else
                return 0;
        }
        public static string TenCongTy()
        {
            string sTruyVan = string.Format(@"SELECT CongTy FROM [Setting] ");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return dr["CongTy"].ToString();
            }
            else
                return "";
        }
        public static string DiaChiCongTy()
        {
            string sTruyVan = string.Format(@"SELECT DiaChi FROM [Setting] ");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return dr["DiaChi"].ToString();
            }
            else
                return "";
        }
        public static string TestDuKieu()
        {
            string sTruyVan = string.Format(@"SELECT TestDuLieu FROM [Setting] ");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return dr["TestDuLieu"].ToString();
            }
            else
                return "";
        }
        public static string LayTenMayInBill(string CD)
        {
            string sTruyVan = string.Format(@"SELECT TenMayIn FROM [CF_KeyCode] WHERE [GetKey] = N'{0}'", CD);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return dr["TenMayIn"].ToString();
            }
            else
                return "";
        }

        public static string LayTenMayInBillPhaChe()
        {
            string sTruyVan = string.Format(@"SELECT MayIn2 FROM [Setting] ");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return dr["MayIn2"].ToString();
            }
            else
                return "";
        }

        public static int KiemTra(string TenMayIn, string IDChiNhanh)
        {
            string sTruyVan = string.Format(@"SELECT * FROM [CF_MayIn] WHERE TenMayIn = N'{0}' AND [IDChiNhanh] = '{1}'", TenMayIn, IDChiNhanh);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return 1;
            }
            else
                return -1;
        }
        public static bool ThemMayIn(string TenMayIn, string IDChiNhanh, string KhoGiay)
        {
            string sTruyVan = string.Format(@"INSERT INTO [CF_MayIn](TenMayIn,IDChiNhanh,KhoGiay) VALUES(N'{0}','{1}','{2}')", TenMayIn, IDChiNhanh, KhoGiay);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
        public static bool XoaMayIn(string ID)
        {
            string sTruyVan = string.Format(@"DELETE FROM CF_MayIn WHERE ID  = {0} ", ID);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
        public static bool CapNhatMayInBill(string TenMayIn, string KhoGiay, string GetKey)
        {
            string sTruyVan = string.Format(@"UPDATE [CF_KeyCode] SET [TenMayIn] = N'{0}',[KhoGiay] = N'{1}'  WHERE [GetKey] = N'{2}'", TenMayIn, KhoGiay, GetKey);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }

        public static bool CapNhatBillInTemp(string ID)
        {
            string sTruyVan = string.Format(@"UPDATE [CF_HoaDon] SET [LanIn] = [LanIn] + 1 WHERE ID = {0}",ID);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
        public static bool CapNhatBillInPhaChe(string ID)
        {
            string sTruyVan = string.Format(@"UPDATE [CF_HoaDon] SET [LanIn2] = [LanIn2] + 1 WHERE ID = {0}", ID);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }

        public static bool CapNhatHangHoaInPhaChe(string ID, string IDMayIn)
        {
            string sTruyVan = string.Format(@"UPDATE CF_ChiTietHoaDon_Temp SET InPhaChe = SoLuong WHERE IDHoaDon = {0} AND IDMayIn = {1}", ID, IDMayIn);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }

        public static string DienThoaiCongTy()
        {
            string sTruyVan = string.Format(@"SELECT SDT FROM [Setting] ");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return dr["SDT"].ToString();
            }
            else
                return "";
        }

        public static int LaySoLanInPhaChe(string ID)
        {
            string sTruyVan = string.Format(@"SELECT LanIn2 FROM [CF_HoaDon] WHERE ID = " + ID + "");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return Int32.Parse(dr["LanIn2"].ToString());
            }
            else
                return 1;
        }
        public static int LaySoLanInTemp(string ID)
        {
            string sTruyVan = string.Format(@"SELECT LanIn FROM [CF_HoaDon] WHERE ID = " + ID + "");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return Int32.Parse(dr["LanIn"].ToString());
            }
            else
                return 1;
        }
        public static int ReportBill(string GetKey )
        {
            string sTruyVan = string.Format(@"SELECT KhoGiay FROM [CF_KeyCode] WHERE [GetKey] = N'{0}'", GetKey);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return Int32.Parse(dr["KhoGiay"].ToString());
            }
            else
                return 58;
        }
        public static int ReportBillPhaChe()
        {
            string sTruyVan = string.Format(@"SELECT ReportBill2 FROM [Setting] ");
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return Int32.Parse(dr["ReportBill2"].ToString());
            }
            else
                return 58;
        }
        public static int KTHoaDon(string IDHoaDon)
        {
            int r1 = 0, r2 = 0;
            
            string sTruyVan1 = string.Format(@"SELECT * FROM [CF_ChiTietHoaDon] WHERE IDHoaDon = '" + IDHoaDon + "'");
            string sTruyVan2 = string.Format(@"SELECT * FROM [CF_ChiTietGio] WHERE IDHoaDon = '" + IDHoaDon + "'");
            DataTable data1 = new DataTable();
            data1 = DataProvider.TruyVanLayDuLieu(sTruyVan1);
            if (data1.Rows.Count > 0)
            {
                r1 = 1;
            }

            DataTable data2 = new DataTable();
            data2 = DataProvider.TruyVanLayDuLieu(sTruyVan2);
            if (data2.Rows.Count > 0)
            {
                r2 = 1;
            }

            if (r1 == 1 && r2 == 1)
                return 0;
            else if (r1 == 1) return 1;
            else return 2;
        }

        public static int KTHoaDon_Temp(string IDHoaDon)
        {
            int r1 = 0, r2 = 0;

            string sTruyVan1 = string.Format(@"SELECT * FROM [CF_ChiTietHoaDon_Temp] WHERE IDHoaDon = '" + IDHoaDon + "'");
            string sTruyVan2 = string.Format(@"SELECT * FROM [CF_ChiTietGio] WHERE IDHoaDon = '" + IDHoaDon + "'");
            DataTable data1 = new DataTable();
            data1 = DataProvider.TruyVanLayDuLieu(sTruyVan1);
            if (data1.Rows.Count > 0)
            {
                r1 = 1;
            }

            DataTable data2 = new DataTable();
            data2 = DataProvider.TruyVanLayDuLieu(sTruyVan2);
            if (data2.Rows.Count > 0)
            {
                r2 = 1;
            }

            if (r1 == 1 && r2 == 1)
                return 0;
            else if (r1 == 1) return 1;
            else return 2;
        }

        public static bool ThemLichSuTruyCap(int IDNhanVien, int IDNhom, string IDChiNhanh, string Menu, string HanhDong)
        {
            string sTruyVan = string.Format(@"INSERT INTO [CF_LichSuTruyCap](IDChiNhanh,IDNhom,IDNguoiDung,Menu,HanhDong,ThoiGian) VALUES ('{0}','{1}','{2}',N'{3}',N'{4}',getdate())", IDChiNhanh, IDNhom, IDNhanVien, Menu, HanhDong);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
        public static bool ThemLichSuQuanLy(int IDNhanVien, int IDNhom, string IDChiNhanh, string Menu, string HanhDong)
        {
            string sTruyVan = string.Format(@"INSERT INTO [CF_LichSuQuanLy](IDChiNhanh,IDNhom,IDNguoiDung,Menu,HanhDong,ThoiGian) VALUES ('{0}','{1}','{2}',N'{3}',N'{4}',getdate())", IDChiNhanh, IDNhom, IDNhanVien, Menu, HanhDong);
            return DataProvider.TruyVanKhongLayDuLieu(sTruyVan);
        }
        //public static double LayDiemQuyDoiTien()
        //{
        //    string sTruyVan = string.Format(@"SELECT SoTienQuyDoi FROM [GPM_Setting] ");
        //    DataTable data = new DataTable();
        //    data = DataProvider.TruyVanLayDuLieu(sTruyVan);
        //    if (data.Rows.Count > 0)
        //    {
        //        DataRow dr = data.Rows[0];
        //        return double.Parse(dr["SoTienQuyDoi"].ToString());
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}

        //public static double LayTienQuyDoiDiem()
        //{
        //    string sTruyVan = string.Format(@"SELECT SoTienTichLuy FROM [GPM_Setting] ");
        //    DataTable data = new DataTable();
        //    data = DataProvider.TruyVanLayDuLieu(sTruyVan);
        //    if (data.Rows.Count > 0)
        //    {
        //        DataRow dr = data.Rows[0];
        //        return double.Parse(dr["SoTienTichLuy"].ToString());
        //    }
        //    return 0;
        //}

        //public static double DiemTichLuy(string IDKhachHang)
        //{
        //    string sTruyVan = string.Format(@"SELECT DiemTichLuy FROM [GPM_KhachHang] WHERE [ID] = {0}", IDKhachHang);
        //    DataTable data = new DataTable();
        //    data = DataProvider.TruyVanLayDuLieu(sTruyVan);
        //    if (data.Rows.Count > 0)
        //    {
        //        DataRow dr = data.Rows[0];
        //        return double.Parse(dr["DiemTichLuy"].ToString());
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}
    }
}
