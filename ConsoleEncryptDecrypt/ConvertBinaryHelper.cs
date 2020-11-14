using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEncryptDecrypt
{
    public class ConvertBinaryHelper
    {
        public ConvertBinaryHelper()
        {
            // 範本
            string s = "stackoverflow";
            var binary = new List<List<int>>();
            for (var counter = 0; counter != s.Length; counter++)
            {
                List<int> a = ConvertTextToBinary(s[counter], 2);
                binary.Add(a);
                foreach (var bit in a)
                {
                    Console.Write(bit);
                }
                Console.Write("\n");
            }
            string str = ConvertBinaryToText(binary);
            Console.WriteLine(str);//stackoverflow
        }

        /// <summary>
        /// 傳入 char (ASCII) 轉為 Base進位
        /// </summary>
        /// <param name="number"></param>
        /// <param name="Base"></param>
        /// <returns></returns>
        public static List<int> ConvertTextToBinary(int number, int Base)
        {
            List<int> list = new List<int>();
            while (number != 0)
            {
                list.Add(number % Base);
                number = number / Base;
            }
            list.Reverse();
            return list;
        }

        /// <summary>
        /// 把二進位轉為字串
        /// </summary>
        /// <param name="seq"></param>
        /// <returns></returns>
        public static string ConvertBinaryToText(List<List<int>> seq)
        {
            return new String(seq.Select(s => (char)s.Aggregate((a, b) => a * 2 + b)).ToArray());
        }
    }
}
