using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCafe.DTO
{
    public class DTO_MayIn
    {
        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private string tenMayIn;

        public string TenMayIn
        {
            get { return tenMayIn; }
            set { tenMayIn = value; }
        }
        private string khoGiay;

        public string KhoGiay
        {
            get { return khoGiay; }
            set { khoGiay = value; }
        }
        public DTO_MayIn(int getID, string getTenMayIn, string getKhoGiay)
        {
            this.ID = getID;
            this.TenMayIn = getTenMayIn;
            this.KhoGiay = getKhoGiay;
        }

        public DTO_MayIn(DataRow dr)
        {
            this.ID = Int32.Parse(dr["ID"].ToString());
            this.TenMayIn = dr["TenMayIn"].ToString();
            this.KhoGiay = dr["KhoGiay"].ToString();
        }
    }
}
