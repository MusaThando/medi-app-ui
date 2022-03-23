using System.Text;
using XSystem.Security.Cryptography;

namespace MedicalAppUI.SQL
{
    public class Helper
    {
        public static string EncryptWithMD5(string inValue)
        {
            var md5 = new MD5CryptoServiceProvider();

            var encoder = new UTF8Encoding();

            string encryptedString = "";

            if (inValue == "")
            { return encryptedString; }
            else
            {
                byte[] hashedBytes = md5.ComputeHash(encoder.GetBytes(inValue));
                encryptedString = Convert.ToBase64String(hashedBytes);
                return encryptedString;
            }
        }
        public static string GetConnectionString()
        {
            string con = "Data source=DEVOPS; Initial Catalog = MedicalApp;Integrated Security=True";
            return con;
        }
        public static string GetBaseUrl() { return "https://localhost:44377"; }
    }
}
