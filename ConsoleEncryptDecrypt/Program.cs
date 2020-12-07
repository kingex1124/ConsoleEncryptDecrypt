using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEncryptDecrypt
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test 把 '我' 轉為 10進制 unicode  再轉為 16進制 unicode 再轉為 byte[] 再編碼為 '我'
            string encodResult = EncodingHelper.BytesEncodToUnicodeString(EncodingHelper.HexStringToByteArr(EncodingHelper.DecToHexString(EncodingHelper.StringToDecUnicode('我'))));

            byte[] byteArray = EncodingHelper.HexStringToByteArr(EncodingHelper.DecToHexString(EncodingHelper.StringToDecUnicode('我')));

            byte[] byteArray1 = Encoding.Unicode.GetBytes("我");

            byte[] byteArray2 = Encoding.Default.GetBytes("我");

            // "我" 在BIG5 碼 = A7DA (此為16進制)
            // 轉為 二進制為 10100111 11011010 切成兩段 分別如下方兩組 轉為十進制的數字
            // byteArray3[0] = 167
            // byteArray3[1] = 218
            byte[] byteArray3 = Encoding.GetEncoding("Big5").GetBytes("我");

            //將byte資料轉為 base64字串
            string base64Data = Convert.ToBase64String(byteArray);

            // 將 '我' 轉為二進位List 再轉回 "我" 字串
            var binary = new List<List<int>>();

            var byteArray4 =  ConvertBinaryHelper.ConvertTextToBinary((int)'我', 2);

            binary.Add(byteArray4);

            var binaryToTextResult = ConvertBinaryHelper.ConvertBinaryToText(binary);

            var resultUrl = SHA256EncryptHelper.Encrypt(@"12345678");
            var resultUrl2 = MD5EncryptHelper.GetMD5(@"12345678");
            var resultUrl3 = AESEncryptHelper.AESEncryptBase64(@"12345678", "1");


            #region 將KeyID 轉為6碼代碼 再轉回ID (縮網址用)

            //var total = 61 * Math.Pow(62, 5) + 61 * Math.Pow(62, 4) + 61 * Math.Pow(62, 3) + 61 * Math.Pow(62, 2) + 61 * Math.Pow(62, 1) + 61 * Math.Pow(62, 0);

            // ToBase62
            var id = 56800235583;

            var longToBase62 = EncodingHelper.LongToBase62(id, 6);

            var base62ToString = EncodingHelper.Base62ToString(longToBase62);

            var stringToBase62Code = EncodingHelper.StringToBase62Code(base62ToString);

            var base62ToBase10 = EncodingHelper.Base62ToBase10(stringToBase62Code);

            //var total = 35 * Math.Pow(36, 5) + 35 * Math.Pow(36, 4) + 35 * Math.Pow(36, 3) + 35 * Math.Pow(36, 2) + 35 * Math.Pow(36, 1) + 35 * Math.Pow(36, 0);

            // ToBase36
            var id36 = 2176782335;

            var longToBase36 = EncodingHelper.LongToBase36(id36, 6);

            var base36ToString = EncodingHelper.Base36ToString(longToBase36);

            var stringToBase36Code = EncodingHelper.StringToBase36Code(base36ToString);

            var base36ToBase10 = EncodingHelper.Base36ToBase10(stringToBase36Code);
            
            var total = 63 * Math.Pow(64, 5) + 63 * Math.Pow(64, 4) + 63 * Math.Pow(64, 3) + 63 * Math.Pow(64, 2) + 63 * Math.Pow(64, 1) + 63 * Math.Pow(64, 0);

            var id64 = 68719476735;

            var longToBase64 = EncodingHelper.LongToBase64(id64, 6);

            var base64ToString = EncodingHelper.Base64ToString(longToBase64);

            var stringToBase64Code = EncodingHelper.StringToBase64Code(base64ToString);

            var base64ToBase10 = EncodingHelper.Base64ToBase10(stringToBase64Code);

            #endregion

            var byteUTF8 = Encoding.UTF8.GetBytes(id.ToString());

            var resultUrl4 = Convert.ToBase64String(byteUTF8);

            List<long> value = new List<long>();
            while (id > 0)
            {
                long remainder = id % 62;
                value.Add(remainder);
                id = id / 62;
            }
           

            string key = "12345678";

            string possword = "1qaz@WSX";

            // key不能太長，容易被破解
            string resultA = DESEncryptHelper.DESEncryptBase64(possword, key);

            //用此較安全 key 可以比較長
            string resultB = AESEncryptHelper.AESEncryptBase64(possword, key);

            // 會產生一把公私key 公key用來加密用 私key用來解密用
            RSAEncryptHelper.GetKey();

            string resultC = RSAEncryptHelper.Encryption(possword);

            string resultD = MD5EncryptHelper.GetMD5(possword);

            Console.WriteLine("EncryptResultA: {0}", resultA);

            Console.WriteLine("EncryptResultB: {0}", resultB);

            Console.WriteLine("EncryptResultC: {0}", resultC);

            Console.WriteLine("EncryptResultD: {0}", resultD);

            Console.ReadKey();

            resultA = DESEncryptHelper.DESDecryptBase64(resultA, key);
            resultB = AESEncryptHelper.AESDecryptBase64(resultB, key);
            resultC = RSAEncryptHelper.Decrypt(resultC);

            Console.WriteLine("DecryptResultA: {0}", resultA);

            Console.WriteLine("DecryptResultB: {0}", resultB);

            Console.WriteLine("DecryptResultC: {0}", resultC);


            //測試檔案加密


            var encryptFile = AESEncryptHelper.AESEncryptByte(File.ReadAllBytes(@"D:\Users\kevan\Desktop\1.OOP 的四個特性.mp4"), "123456789");

            var decryptFile = AESEncryptHelper.AESDecryptByte(encryptFile, "123456789");
            using (var fs = new FileStream(@"D:\Users\kevan\Desktop\OK\1.OOP 的四個特性.mp4", FileMode.Create, FileAccess.Write))
            {
                fs.Write(decryptFile, 0, decryptFile.Length);
            }

            Console.ReadKey();
        }


    }
}
