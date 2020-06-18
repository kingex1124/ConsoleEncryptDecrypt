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
        private static string _cryptoKey;

        /// <summary>
        /// 建構子
        /// </summary>
        static DESEncryptHelper()
        {
            _cryptoKey = "87654321";
        }

        public DESEncryptHelper()
        {
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <returns></returns>
        public static string DESEncryptBase64(string sourceStr)
        {
            return DESEncryptBase64(sourceStr, _cryptoKey);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <returns></returns>
        public static string DESEncryptBase64(string sourceStr, string cryptoKey)
        {
            string base64String;
            try
            {
                string dESCryptoKey = cryptoKey;
                DESCryptoServiceProvider des = new DESCryptoServiceProvider()
                {
                    Mode = CipherMode.ECB,
                    Key = Encoding.ASCII.GetBytes(dESCryptoKey),
                    IV = Encoding.ASCII.GetBytes(dESCryptoKey)
                };
                byte[] s = Encoding.ASCII.GetBytes(sourceStr);
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
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static string DESDecryptBase64(string base64String)
        {
            return DESDecryptBase64(base64String, _cryptoKey);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static string DESDecryptBase64(string base64String, string cryptoKey)
        {
            string result;
            try
            {
                string dESCryptoKey = cryptoKey;
                DESCryptoServiceProvider des = new DESCryptoServiceProvider()
                {
                    Mode = CipherMode.ECB,
                    Key = Encoding.ASCII.GetBytes(dESCryptoKey),
                    IV = Encoding.ASCII.GetBytes(dESCryptoKey)
                };
                byte[] s = Convert.FromBase64CharArray(base64String.ToCharArray(), 0, base64String.Length);
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
