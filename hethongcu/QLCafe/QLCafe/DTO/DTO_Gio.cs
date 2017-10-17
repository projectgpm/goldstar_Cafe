using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCafe.DTO
{
    public class DTO_Gio
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string maGio;

        public string MaGio
        {
            get { return maGio; }
            set { maGio = value; }
        }
        private DateTime gioBatDau;

        public DateTime GioBatDau
        {
            get { return gioBatDau; }
            set { gioBatDau = value; }
        }
        private DateTime gioKetThuc;

        public DateTime GioKetThuc
        {
            get { return gioKetThuc; }
            set { gioKetThuc = value; }
        }
        private int tyLe;

        public int TyLe
        {
            get { return tyLe; }
            set { tyLe = value; }
        }
        public DTO_Gio(int getID, string getMaGio, DateTime getGioBatDau, DateTime getGioKetThuc, int getTyLe)
        {
            this.Id = getID;
            this.MaGio = getMaGio;
            this.GioBatDau = getGioBatDau;
            this.GioKetThuc = getGioKetThuc;
            this.TyLe = getTyLe;
        }
        public DTO_Gio(DataRow dr)
        {
            this.Id = Int32.Parse(dr["ID"].ToString());
            this.MaGio = dr["MaGio"].ToString();
            this.GioBatDau = DateTime.Parse(dr["GioBatDau"].ToString());
            this.GioKetThuc = DateTime.Parse(dr["GioKetThuc"].ToString());
            this.TyLe = Int32.Parse(dr["TyLe"].ToString());
        }
    }
}
