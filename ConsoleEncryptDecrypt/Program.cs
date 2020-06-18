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
