using System;
using System.Data.SqlClient;

namespace Zeghetmaar
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection conn = new SqlConnection();
            string cs = "Server=SQL6004.site4now.net;Database=DB_A2A0BC_vp;User Id=K0501;Password=ROCvT_K0501;";
            conn.ConnectionString = cs;

            conn.Open();

        }
    }
}
