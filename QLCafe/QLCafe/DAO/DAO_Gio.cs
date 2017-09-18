using QLCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCafe.DAO
{
    public class DAO_Gio
    {
        private static DAO_Gio instance;

        public static DAO_Gio Instance
        {
            get { if (instance == null) instance = new DAO_Gio(); return DAO_Gio.instance; }
            private set { DAO_Gio.instance = value; }
        }
        public static int LayTyLe(string GioVao)
        {

            string sTruyVan = string.Format(@"SELECT TyLe FROM [CF_Gio] WHERE '{0}' BETWEEN FORMAT(GioBatDau,'h:mm:ss') AND FORMAT(GioKetThuc,'h:mm:ss')", GioVao);
            DataTable data = new DataTable();
            data = DataProvider.TruyVanLayDuLieu(sTruyVan);
            if (data.Rows.Count > 0)
            {
                DataRow dr = data.Rows[0];
                return Int32.Parse(dr["TyLe"].ToString());
            }
            return 0;
        }
    }
}
