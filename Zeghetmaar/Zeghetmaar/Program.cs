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
                Console.Write("Inlognaam: ");
                string userName = Console.ReadLine();
                Console.Write("Wachtwoord: ");
                string passWord = Console.ReadLine();
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE username = @USERNAME AND password = @PASSWORD collate Latin1_General_CS_AS;", conn);
                cmd.Parameters.AddWithValue("@USERNAME", userName);
                cmd.Parameters.AddWithValue("@PASSWORD", passWord);
                SqlDataReader r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    Console.WriteLine("Succesvol ingelogt");
                }
                else
                {
                    Console.WriteLine("niet kunnen inloggen");
                }
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
