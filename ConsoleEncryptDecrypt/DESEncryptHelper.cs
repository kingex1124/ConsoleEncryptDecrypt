using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEncryptDecrypt
{
    /// <summary>
    /// DES 加解密演算法
    /// DES 加密法於 1977 年被聯邦政府列為數據加密標準 其加解密速度非常快速，
    /// 但因為 56 位元金鑰過短 很有可能於 24 小時內被破解 如果需要安全一點的加密方式，
    /// 可以考慮改用 AES 機制 AES 語法與 DES 大致相同，只在加解密的金鑰 KEY 及 IV 長度不同 
    /// 加密及解碼需使用相同的金鑰
    /// </summary>
    public class DESEncryptHelper
    {
        /// <summary>
        /// 加密Key
        /// </summary>
        private static string _dESCryptoKey;

        /// <summary>
        /// 建構子
        /// </summary>
        static DESEncryptHelper()
        {
            _dESCryptoKey = "87654321";
        }

        public DESEncryptHelper()
        {
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static string EncryptDES(string original)
        {
            return EncryptDES(original, _dESCryptoKey);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static string EncryptDES(string original, string dESCryptoKey)
        {
            string base64String;
            try
            {
                string DESCryptoKey = dESCryptoKey;
                DESCryptoServiceProvider des = new DESCryptoServiceProvider()
                {
                    Mode = CipherMode.ECB,
                    Key = Encoding.ASCII.GetBytes(DESCryptoKey),
                    IV = Encoding.ASCII.GetBytes(DESCryptoKey)
                };
                byte[] s = Encoding.ASCII.GetBytes(original);
                ICryptoTransform desencrypt = des.CreateEncryptor();
                byte[] CryptBytes = desencrypt.TransformFinalBlock(s, 0, (int)s.Length);
                base64String = Convert.ToBase64String(CryptBytes);
            }
            catch (Exception exception)
            {
                base64String = string.Empty;
            }
            return base64String;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Base64String"></param>
        /// <returns></returns>
        public static string DecryptDES(string Base64String)
        {
            return DecryptDES(Base64String, _dESCryptoKey);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Base64String"></param>
        /// <returns></returns>
        public static string DecryptDES(string Base64String, string dESCryptoKey)
        {
            string result;
            try
            {
                string DESCryptoKey = dESCryptoKey;
                DESCryptoServiceProvider des = new DESCryptoServiceProvider()
                {
                    Mode = CipherMode.ECB,
                    Key = Encoding.ASCII.GetBytes(DESCryptoKey),
                    IV = Encoding.ASCII.GetBytes(DESCryptoKey)
                };
                byte[] s = Convert.FromBase64CharArray(Base64String.ToCharArray(), 0, Base64String.Length);
                ICryptoTransform desencrypt = des.CreateDecryptor();
                result = Encoding.ASCII.GetString(desencrypt.TransformFinalBlock(s, 0, (int)s.Length));
            }
            catch (Exception exception)
            {
                result = string.Empty;
            }
            return result;
        }
    }
}
