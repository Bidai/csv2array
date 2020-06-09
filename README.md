# csv2array
将csv格式的字符串转换为string[][]格式的对象

## csv格式：
行与行之间以\n或者\r\n分隔
行内单元格之间以  ,  分隔
双引号表示内部为一整个字符串，内部的双引号用2个双引号表示

## 示例代码
```C#
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
```

## 输出结果：
```C#
输入的csv字符串为：
123,456,789,"0,1"

,,,a,b,'c',"""d"""
识别结果
行数=3
内容如下：
------------------------------
|123    |456    |789    |0,1
------------------------------

------------------------------
|       |       |       |a      |b      |'c'    |"d"
```
