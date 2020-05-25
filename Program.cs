using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bidai
{
    class Program
    {
        static void Main(string[] args)
        {
            string csvstring = "123,456,789,\"0,1\"\n\r\n,,,a,b,'c',\"\"\"d\"\"\"";
            string[][] result = Test.FromCSV(csvstring);
            Console.WriteLine("输入的csv字符串为：");
            Console.WriteLine(csvstring);
            Console.WriteLine("识别结果\r\n行数=" + result.Length);
            Console.WriteLine("内容如下：");
            foreach (var line in result)
            {
                //每行开头输出分割线
                for (int i = 0; i < 30; ++i)
                    Console.Write("-");
                Console.Write("\r\n");

                foreach (var a in line)
                {
                    Console.Write('|');//每格开头输出一个|
                    Console.Write(a);
                    Console.Write('\t');
                }

                Console.Write("\r\n");
            }
            Console.Read();
        }
    }
}
