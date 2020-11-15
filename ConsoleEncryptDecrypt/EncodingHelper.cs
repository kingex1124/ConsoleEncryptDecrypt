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

        #region 縮網址編碼

        #region Base62 編碼

        /// <summary>
        /// 將ID轉為Base62 16進位代碼
        /// ID: 0 ~ 56800235583
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="size">產生幾碼</param>
        /// <returns></returns>
        public static List<long> LongToBase62(long id, int size)
        {
            List<long> value = new List<long>();
            while (id > 0)
            {
                // 超過表示溢位
                if (value.Count == size)
                    return null;

                long remainder = id % 62;
                value.Add(remainder);
                id = id / 62;
            }

            while (value.Count < size)
                value.Add(0);

            value.Reverse();

            return value;
        }

        /// <summary>
        /// 轉碼成Base62的對應符號字串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Base62ToString(List<long> value)
        {
            string result = string.Empty;

            Dictionary<long, string> mappingData = new Dictionary<long, string>();

            for (long i = 0; i < 26; i++)
                mappingData.Add(i, ((char)(97+i)).ToString());

            for (int i = 26; i < 52; i++)
                mappingData.Add(i, ((char)(40 + i)).ToString());

            for (int i = 52; i < 62; i++)
                mappingData.Add(i, ((char)(-4 + i)).ToString());

            foreach (var item in value)
                result = result + mappingData[item];
            
            return result;
        }

        /// <summary>
        /// 將Base62 字串符號 轉為代碼
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<long> StringToBase62Code(string str)
        {
            List<long> result = new List<long>();

            Dictionary<char, long> mappingData = new Dictionary<char, long>();

            for (long i = 0; i < 26; i++)
                mappingData.Add(((char)(97 + i)), i);

            for (int i = 26; i < 52; i++)
                mappingData.Add(((char)(40 + i)), i);

            for (int i = 52; i < 62; i++)
                mappingData.Add(((char)(-4 + i)), i);

            foreach (var item in str)
                result.Add(mappingData[item]);
            
            return result;
        }

        /// <summary>
        /// 透過Base62 轉回 ID (10進位)
        /// </summary>
        /// <param name="base62"></param>
        /// <returns></returns>
        public static long Base62ToBase10(List<long> base62)
        {         
            base62.Reverse();

            long id = 0;

            for (int i = 0; i < base62.Count; i++)
                id += (long)(base62[i] * Math.Pow(62, i));
            
            return id;
        }

        #endregion

        #region Base36 編碼

        /// <summary>
        /// 將ID轉為Base62 16進位代碼
        /// ID: 0 ~ 2176782335
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="size">產生幾碼</param>
        /// <returns></returns>
        public static List<long> LongToBase36(long id, int size)
        {
            List<long> value = new List<long>();
            while (id > 0)
            {
                // 超過表示溢位
                if (value.Count == size)
                    return null;

                long remainder = id % 36;
                value.Add(remainder);
                id = id / 36;
            }

            while (value.Count < size)
                value.Add(0);

            value.Reverse();

            return value;
        }

        /// <summary>
        /// 轉碼成Base36的對應符號字串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Base36ToString(List<long> value)
        {
            string result = string.Empty;

            Dictionary<long, string> mappingData = new Dictionary<long, string>();

            for (long i = 0; i < 26; i++)
                mappingData.Add(i, ((char)(97 + i)).ToString());

            for (int i = 26; i < 36; i++)
                mappingData.Add(i, ((char)(22 + i)).ToString());
    
            foreach (var item in value)
                result = result + mappingData[item];

            return result;
        }

        /// <summary>
        /// 將Base36 字串符號 轉為代碼
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<long> StringToBase36Code(string str)
        {
            List<long> result = new List<long>();

            Dictionary<char, long> mappingData = new Dictionary<char, long>();

            for (long i = 0; i < 26; i++)
                mappingData.Add(((char)(97 + i)), i);

            for (int i = 26; i < 36; i++)
                mappingData.Add(((char)(22 + i)), i);

            foreach (var item in str)
                result.Add(mappingData[item]);

            return result;
        }

        /// <summary>
        /// 透過Base36 轉回 ID (10進位)
        /// </summary>
        /// <param name="base36"></param>
        /// <returns></returns>
        public static long Base36ToBase10(List<long> base36)
        {
            base36.Reverse();

            long id = 0;

            for (int i = 0; i < base36.Count; i++)
                id += (long)(base36[i] * Math.Pow(36, i));

            return id;
        }

        #endregion

        #region Base64 編碼

        /// <summary>
        /// 將ID轉為Base64 16進位代碼
        /// ID: 0 ~ 68719476735
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="size">產生幾碼</param>
        /// <returns></returns>
        public static List<long> LongToBase64(long id, int size)
        {
            List<long> value = new List<long>();
            while (id > 0)
            {
                // 超過表示溢位
                if (value.Count == size)
                    return null;

                long remainder = id % 64;
                value.Add(remainder);
                id = id / 64;
            }

            while (value.Count < size)
                value.Add(0);

            value.Reverse();

            return value;
        }

        /// <summary>
        /// 轉碼成Base64的對應符號字串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Base64ToString(List<long> value)
        {
            string result = string.Empty;

            Dictionary<long, string> mappingData = new Dictionary<long, string>();

            for (long i = 0; i < 26; i++)
                mappingData.Add(i, ((char)(65 + i)).ToString());

            for (int i = 26; i < 52; i++)
                mappingData.Add(i, ((char)(71 + i)).ToString());

            for (int i = 52; i < 62; i++)
                mappingData.Add(i, ((char)(-4 + i)).ToString());

            for (int i = 62; i < 64; i++)
                mappingData.Add(i, ((char)(-17 + i)).ToString());

            foreach (var item in value)
                result = result + mappingData[item];

            return result;
        }

        /// <summary>
        /// 將Base64 字串符號 轉為代碼
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<long> StringToBase64Code(string str)
        {
            List<long> result = new List<long>();

            Dictionary<char, long> mappingData = new Dictionary<char, long>();

            for (long i = 0; i < 26; i++)
                mappingData.Add(((char)(65 + i)), i);

            for (int i = 26; i < 52; i++)
                mappingData.Add(((char)(71 + i)), i);

            for (int i = 52; i < 62; i++)
                mappingData.Add(((char)(-4 + i)), i);

            for (int i = 62; i < 64; i++)
                mappingData.Add(((char)(-17 + i)), i);

            foreach (var item in str)
                result.Add(mappingData[item]);

            return result;
        }

        /// <summary>
        /// 透過Base64 轉回 ID (10進位)
        /// </summary>
        /// <param name="base36"></param>
        /// <returns></returns>
        public static long Base64ToBase10(List<long> base36)
        {
            base36.Reverse();

            long id = 0;

            for (int i = 0; i < base36.Count; i++)
                id += (long)(base36[i] * Math.Pow(36, i));

            return id;
        }

        #endregion

        #endregion
    }
}
