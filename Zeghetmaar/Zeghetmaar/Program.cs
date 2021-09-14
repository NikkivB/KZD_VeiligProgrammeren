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
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users;", conn);
                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    Console.WriteLine("Username: " + r[0] + " Password: " + r[1]);
                }
                Console.WriteLine("Welkom!");
            }
            catch(SqlException sql)
            {
                Console.WriteLine("Verbinding met server is mislukt.");
            }
            catch(DivideByZeroException ex1)
            {
                Console.WriteLine("Niet deelbaar door nul");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception");
            }
            finally
            {
                if(conn.State != System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
            
        }
    }
}
