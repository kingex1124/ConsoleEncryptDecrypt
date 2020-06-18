using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEncryptDecrypt
{
    public class MD5EncryptHelper
    {
        /// <summary>
        /// MD5 演算法
        /// MD5 即 Message-Digest Algorithm 5，是電腦廣泛使用的雜湊演算法之一 
        /// 其演算法複雜度和不可逆性，通常用於確保資訊傳輸完整一致 因其不可逆性，
        /// 所以只有加密的函式，沒有解密的函式
        /// </summary>
        public MD5EncryptHelper()
        {

        }

        /// <summary> 
        /// 取得 MD5 編碼後的 Hex 字串 
        /// 加密後為 32 Bytes Hex String (16 Byte) 
        /// </summary> 
        /// <span  name="original" class="mceItemParam"></span>原始字串</param> 
        /// <returns></returns> 
        public static string GetMD5(string sourceStr)
        {
            string result = string.Empty;
            try
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] b = md5.ComputeHash(Encoding.UTF8.GetBytes(sourceStr));
                result = BitConverter.ToString(b).Replace("-", string.Empty);
            }
            catch (Exception ex)
            {
            }
            return result;
        }
    }
}
