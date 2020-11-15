using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEncryptDecrypt
{
    public class SHA256EncryptHelper
    {
        public SHA256EncryptHelper()
        {

        }

        public static string Encrypt(string sourceStr)
        {
            string result = string.Empty;
            try
            {
                SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();

                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(sourceStr));

                result = Convert.ToBase64String(hashValue);
            }
            catch (Exception ex)
            { 
            }

            return result;
        }
    }
}
