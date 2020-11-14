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
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
            
            byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(sourceStr));

            string e = System.Text.Encoding.Default.GetString(hashValue);
            //string test = SHA256EncryptHelper.ConvertBinaryToText();
           return  Convert.ToBase64String(hashValue);
        }
    }
}
