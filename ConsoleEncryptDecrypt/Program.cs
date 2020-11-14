using System;
using System.Collections.Generic;
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

            //將byte資料轉為 base64字串
            string base64Data = Convert.ToBase64String(byteArray);

            // 將 '我' 轉為二進位List 再轉回 "我" 字串
            var binary = new List<List<int>>();

            var btteArray2 =  ConvertBinaryHelper.ConvertTextToBinary((int)'我', 2);

            binary.Add(btteArray2);

            var binaryToTextResult = ConvertBinaryHelper.ConvertBinaryToText(binary);

            var resultUrl = SHA256EncryptHelper.Encrypt(@"1");

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

            Console.ReadKey();
        }


    }
}
