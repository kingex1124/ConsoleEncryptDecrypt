using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEncryptDecrypt
{
    public class EncodingHelper
    {
        public EncodingHelper()
        {
           
        }

        /// <summary>
        /// 字串轉十進制Unicode
        /// ex: 傳入 '我'
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int StringToDecUnicode(char cha)
        {
            return (int)cha;
        }

        /// <summary>
        /// 十進制數字轉為16進制字串
        /// </summary>
        /// <param name="dec"></param>
        /// <returns></returns>
        public static string DecToHexString(int dec)
        {
            return Convert.ToString(dec, 16);
        }

        /// <summary>
        /// 16進制 轉為 Byte
        /// Ex 6211 
        /// ex 6211AB
        /// 每個byte裡面存的都是十進制數字(也就是把16進制轉為10進制存取)
        /// </summary>
        /// <param name="hexStr"></param>
        /// <returns></returns>
        public static byte[] HexStringToByteArr(string hexStr)
        {
            int count = hexStr.Length % 2 > 0 ? hexStr.Length / 2 + 1 : hexStr.Length / 2;
            
            byte[] bytes = new byte[count];

            for (int i = 0; i < bytes.Length; i++)
                if (hexStr.Length - (i + 1) * 2 >= 0)
                    bytes[i] = Convert.ToByte(hexStr.Substring(hexStr.Length - (i + 1) * 2, 2), 16);
                else
                    bytes[i] = Convert.ToByte(hexStr.Substring(0, 1), 16);
            return bytes;
        }

        /// <summary>
        /// 中文字串 轉為 Unicode Byte
        /// </summary>
        /// <param name="hexStr"></param>
        /// <returns></returns>
        public static byte[] StringToUnicodeByteArr(string hexStr)
        {
            return Encoding.Unicode.GetBytes(hexStr);
        }

        /// <summary>
        /// 使用Default轉碼
        /// 英文數字 為ASCII
        /// 中文會變成 ANSI編碼 也就是Big5
        /// </summary>
        /// <param name="hexStr"></param>
        /// <returns></returns>
        public static byte[] StringToANSIByteArr(string hexStr)
        {
            return Encoding.Default.GetBytes(hexStr);
        }

        /// <summary>
        /// 把byte[] 編碼為 Unicode 字串
        /// </summary>
        /// <param name="byteData"></param>
        /// <returns></returns>
        public static string BytesEncodToUnicodeString(byte[] byteData)
        {
            return Encoding.Unicode.GetString(byteData);
        }
    }
}
