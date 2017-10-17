using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCafe.DAO
{
    class DAO_ConnectSQL
    {
        private string datasource;
        public string Datasource
        {
            get { return datasource; }
            set { datasource = value; }
        }
        private string database;

        public string Database
        {
            get { return database; }
            set { database = value; }
        }
        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public SqlConnection Connect()
        {
            try
            {
                this.ReadFileConnect();
                string strConnect = "Data Source=" + Datasource + ";Initial Catalog=" + Database + ";User ID=" + Username + ";Password=" + Password + ";Persist Security Info=True";
                SqlConnection KetNoi = new SqlConnection(strConnect);
                KetNoi.Open();
                return KetNoi;
            }
            catch (Exception ex)
            { return null; }
        }

        public void DisConnect(SqlConnection mySqlConnect)
        {
            mySqlConnect.Close();
        }

        public static bool WriteFileConnect(string datasource, string database, string username, string password)
        {
            FileStream fileStream = new FileStream("ServerSql.txt", FileMode.Create, FileAccess.Write, FileShare.None);
            try
            {
                StreamWriter sw = new StreamWriter(fileStream);
                sw.WriteLine(datasource);
                sw.WriteLine(database);
                sw.WriteLine(username);
                sw.WriteLine(password);
                sw.Flush();
                sw.Close();
                fileStream.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void ReadFileConnect()
        {
            FileStream fileStream = new FileStream("ServerSql.txt", FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader sr = new StreamReader(fileStream);
            Datasource = sr.ReadLine();
            Database = sr.ReadLine();
            Username = sr.ReadLine();
            Password = sr.ReadLine();
            sr.Close();
            fileStream.Close();
        }
    }
}
