using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace VeiligProgrammeren2122
{
    /// <summary>
    /// 
    /// </summary>
    public static class StaticMethods
    {
        private readonly static SqlConnection conn = 
            new SqlConnection("Server=sql6004.site4now.net;Database=DB_A2A0BC_vp;");

        /// <summary>
        /// Checks the login.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Connecting to database failed</exception>
        public static bool CheckLogin(string username , string password)
        {
            bool ok = false;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Users WHERE UserName = @USERNAME AND password = @PASSWORD";
            cmd.Parameters.AddWithValue("@USERNAME", username);
            cmd.Parameters.AddWithValue("@PASSWORD", password);
            cmd.Connection = conn;
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                ok = reader.HasRows;
            }
            catch
            {
                throw new Exception("Connecting to database failed");
            }
            finally
            {
                if(conn.State != System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
            return ok;
        }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="newpassword">The newpassword.</param>
        /// <param name="confirmpassword">The confirmpassword.</param>
        /// <returns></returns>
        public static string ChangePassword(string username , string password , 
            string newpassword , string confirmpassword)
        {
            if(!CheckLogin(username , password))
            {
                return "username and/or old password incorrect";
            }
            else if(newpassword != confirmpassword)
            {
                return "Passwords do not match";
            }
            else
            {
                if(IsValidPassword(newpassword , 8 , 2, 2, 1, 1, "+=%!?-_"))
                {
                    // Update the password
                    // SqlCommand cmd2 =
                    //    new SqlCommand("UPDATE Users SET Password = @PASSWORD WHERE Username = @USERNAME",
                    //    conn);
                    SqlCommand cmd2 =
                        new SqlCommand("UPDATE Users SET Password = '" + 
                        newpassword + "' WHERE Username = '" +
                        username + "'", conn);

                    //cmd2.Parameters.AddWithValue("@USERNAME", username);
                    //cmd2.Parameters.AddWithValue("@PASSWORD", newpassword);
                    conn.Open();
                    if (cmd2.ExecuteNonQuery() == 0)
                    {
                        return "Updating password failed";
                    }
                    else
                    {
                        return "Password updated successfully";
                    }
                }
            }
            return "New password does not meet criteria";
        }

        /// <summary>
        /// Determines whether [is valid password] [the specified password].
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="minLength">The minimum length.</param>
        /// <param name="minUpper">The minimum upper.</param>
        /// <param name="minLower">The minimum lower.</param>
        /// <param name="minDigit">The minimum digit.</param>
        /// <param name="minSpecial">The minimum special.</param>
        /// <param name="allowedSpecials">The allowed specials.</param>
        /// <returns>
        ///   <c>true</c> if [is valid password] [the specified password]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidPassword(string password,int minLength ,
            int minUpper, int minLower, int minDigit, int minSpecial , string allowedSpecials)
        {
            int upperCount = 0;
            int lowerCount = 0;
            int digitCount = 0;
            int specialsCount = 0;
            foreach(char c in password)
            {
                int ascii = (int)c;
                if (ascii >= 65 && ascii <= 90) upperCount++;
                if (ascii >= 97 && ascii <= 122) lowerCount++;
                if (ascii >= 48 && ascii <= 57) digitCount++;
                if (allowedSpecials.Contains(c)) specialsCount++;
            }
            return upperCount >= minUpper && lowerCount >= minLower &&
                digitCount >= minDigit && specialsCount >= minSpecial &&
                password.Length >= minLength;
        }

        public static string ShiftCypher(string text , int key)
        {
            //TODO: Update this method so that only letters are shifted
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            string encryptedText = "";
            foreach(char c in text)
            {
                int oldposition = alphabet.IndexOf(c);
                if ( oldposition < 0)
                {
                    encryptedText += c.ToString();
                }
                else
                {
                    for (int i = 0; i < encryptedText.Length; i++)
                    {
                        if(i % 2 == 0)
                        {
                            int newposition = oldposition + 2;
                            char newCharacter = alphabet[newposition % 52];
                            encryptedText += newCharacter.ToString();
                        }
                        else
                        {
                            int newposition = oldposition + 1;
                            char newCharacter = alphabet[newposition % 52];
                            encryptedText += newCharacter.ToString();
                        }
                    }
                }
            }
            return encryptedText;
        }

        public static string UnShiftCypher(string encryptedText , int key)
        {
            string decryptedText = "";
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            //TODO: Insert your code to decrypt the encrypted text using the given key
            // Only letters should be unshifted
            foreach (char c in encryptedText)
            {
                int oldposition = alphabet.IndexOf(c);
                if (oldposition < 0)
                {
                    encryptedText += c.ToString();
                }
                else
                {
                    int newposition = oldposition - key;
                    char newCharacter = alphabet[newposition % 52];
                    decryptedText += newCharacter.ToString();
                }
            }
            return decryptedText;
        }
    }
}
