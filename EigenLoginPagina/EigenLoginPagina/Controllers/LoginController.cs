using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EigenLoginPagina.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            SqlConnection conn = new SqlConnection();
            string cs = "Server=SQL6004.site4now.net;Database=DB_A2A0BC_vp;User Id=K0501;Password=ROCvT_K0501;";
            conn.ConnectionString = cs;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("splogin", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@USERNAME", username);
                cmd.Parameters.AddWithValue("@PASSWORD", password);
                SqlDataReader r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    return RedirectToAction("Home", "Index");
                }
                else
                {
                    ViewBag.username = username;
                    ViewBag.Error = "Login mislukt";
                    return View();
                }
            }
            catch (SqlException sql)
            {
                Console.WriteLine("Verbinding met server is mislukt.");
            }
            catch (DivideByZeroException ex1)
            {
                Console.WriteLine("Niet deelbaar door nul");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception");
            }
            finally
            {
                if (conn.State != System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
            return View();
        }
    }
}
