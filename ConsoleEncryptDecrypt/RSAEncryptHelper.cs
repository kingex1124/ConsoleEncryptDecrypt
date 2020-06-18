using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEncryptDecrypt
{
    public class RSAEncryptHelper
    {
        /// <summary>
        /// RSA 加解密演算法
        /// RSA 是一種非對稱性加密演算法，其原理是以公鑰及私鑰來處理加解密 
        /// 簡單來說，公鑰可以提供給任何需要加密的人，但是私鑰必須妥善保存 
        /// 加密時以公鑰處理即可，但解密必須有私鑰
        /// </summary>
        public RSAEncryptHelper()
        {
        }

        /// <summary>
        /// 獲取加密所使用的key，RSA算法是一種非對稱密碼算法，所謂非對稱，就是指該算法需要一對密鑰，使用其中一個加密，則需要用另一個才能解密。
        /// </summary>
        public static void GetKey()
        {
            try
            {
                string publicKey = string.Empty;
                string privateKey = string.Empty;
                RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
                publicKey = rSACryptoServiceProvider.ToXmlString(false); // 獲取公匙，用於加密
                privateKey = rSACryptoServiceProvider.ToXmlString(true); // 獲取公匙和私匙，用於解密

                //Console.WriteLine("PublicKey is {0}", PublicKey);        // 輸出公匙
                //Console.WriteLine("PrivateKey is {0}", PrivateKey);     // 輸出密匙
                // 密匙中含有公匙，公匙是根據密匙進行計算得來的。

                using (StreamWriter streamWriter = new StreamWriter("PublicKey.xml"))
                {
                    streamWriter.Write(rSACryptoServiceProvider.ToXmlString(false));// 將公匙保存到運行目錄下的PublicKey
                }
                using (StreamWriter streamWriter = new StreamWriter("PrivateKey.xml"))
                {
                    streamWriter.Write(rSACryptoServiceProvider.ToXmlString(true)); // 將公匙&私匙保存到運行目錄下的PrivateKey
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="sourceStr">需要加密的明文</param>
        /// <returns></returns>
        public static string Encryption(string sourceStr)
        {
            string result = string.Empty;
            try
            {
                RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
                using (StreamReader streamReader = new StreamReader("PublicKey.xml")) // 讀取運行目錄下的PublicKey.xml
                {
                    rSACryptoServiceProvider.FromXmlString(streamReader.ReadToEnd()); // 將公匙載入進RSA實例中
                }
                byte[] buffer = Encoding.UTF8.GetBytes(sourceStr); // 將明文轉換為byte[]

                // 加密後的數據就是一個byte[] 數組,可以以 文件的形式保存 或 別的形式(網上很多教程,使用Base64進行編碼化保存)
                byte[] encryptBuffer = rSACryptoServiceProvider.Encrypt(buffer, false); // 進行加密

                string encryptBase64 = Convert.ToBase64String(encryptBuffer); // 如果使用base64進行明文化，在解密時 需要再次將base64 轉換為byte[]
                                                                              //Console.WriteLine(EncryptBase64);
                result = encryptBase64;
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string Decrypt(string base64String)
        {
            string result = string.Empty;
            try
            {
                byte[] buffer = Convert.FromBase64String(base64String);
                RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
                using (StreamReader streamReader = new StreamReader("PrivateKey.xml")) // 讀取運行目錄下的PrivateKey.xml
                {
                    rSACryptoServiceProvider.FromXmlString(streamReader.ReadToEnd()); // 將私匙載入進RSA實例中
                }
                // 解密後得到一個byte[] 數組
                byte[] decryptBuffer = rSACryptoServiceProvider.Decrypt(buffer, false); // 進行解密
                result = Encoding.UTF8.GetString(decryptBuffer); // 將byte[]轉換為明文
            }
            catch (Exception ex)
            {
            }
            return result;
        }
    }
}
