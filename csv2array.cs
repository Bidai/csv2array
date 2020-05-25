using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bidai
{
    class Test
    {
        /// <summary>
        /// 传入csv格式的字符串，返回string[][]格式对象
        /// </summary>
        /// <param name="str">csv字符串，格式为：行内元素间以逗号分隔，行间以\n（或者\r\n）分隔，双引号表示内部是一整个字符串</param>
        /// <returns>返回string[][]格式的对象（假设r为返回的对象，则r[0][1]表示第一行第二列的内容）</returns>
        /// <note>每行的长度（Length）有可能不一样，但不会为null</note>
        static public string[][] FromCSV(string str)
        {
            //只有在传入csv字符串为null的时候返回null
            if (str == null) return null;
            //结果列表暂且设置为空的一行
            List<string[]> result = new List<string[]>();
            //当前行
            List<string> current = new List<string>();
            //变量inner用以标记当前字符是否在双引号内部，首个字符不在双引号内部
            bool inner = false;
            //变量last用以标记当前单元格的起始下标，第一个单元格就是从首个字符算起的
            int last = 0;
            //逐个字节去判断，当能判断一个单元格的起始和终止位置时，将单元格内容添加到结果数组中
            for (int i = 0; i <= str.Length; ++i)
            {
                //首先按双引号内外分两类
                if (inner)
                {
                    //如果到达文本结尾，还是在引号内部，可以按在外部处理
                    if (i == str.Length)
                    {
                        inner = false;
                        i--;
                        continue;
                    }
                    //如果当前字符是双引号，则变换到外部
                    if (str[i] == '\"')
                        inner = false;
                }
                else
                {
                    //如果当前已经到达文本结尾，或者遇到逗号，或者遇到换行，则说明已经到达单元格结尾，需要添加单元格内容到数组
                    if (i == str.Length || str[i] == ',' || str[i] == '\n')
                    {
                        //对于换行的情况，需要标记一下
                        bool enter = false;
                        //记录单元格字符串
                        string s = "";
                        //对于到达文件结尾的，单元格字符串就是从last到末尾
                        if (i == str.Length)
                            s = str.Substring(last);
                        //对于遇到逗号的，单元格字符串就是从last到逗号的前一个字符
                        else if (str[i] == ',')
                        {
                            s = str.Substring(last, i - last);
                        }
                        //对于遇到回车的，因为windows的换行符是\r\n，所以很可能要排除前面的\r
                        else if (str[i] == '\n')
                        {
                            enter = true;
                            if (i > 0 && str[i - 1] == '\r')
                                s = str.Substring(last, i - last - 1);
                            else
                                s = str.Substring(last, i - last);
                        }
                        //认为下一个字符就是新的单元格开始
                        last = i + 1;
                        //获取到单元格内容之后，需要对双引号包起来的进行处理
                        if (s.Length >= 2 && s[0] == '\"')
                            s = s.Substring(1, s.Length - 2).Replace("\"\"", "\"");
                        else if (string.IsNullOrEmpty(s)) s = null;
                        //将字符串添加到最后一行，使用Array.Resize()方法调整该行数组大小
                        current.Add(s);
                        //如果遇到换行，则新增一行
                        if (enter)
                        {
                            if (current.Count == 1 && current[0] == null)
                                result.Add(new string[0]);
                            else result.Add(current.ToArray());
                            current.Clear();
                        }
                    }
                    //在外部遇到双引号则进入内部
                    else if (str[i] == '\"')
                    {
                        inner = true;
                    }
                }
            }
            if (current.Count != 0) result.Add(current.ToArray());
            //返回结果
            return result.ToArray();
        }
    }
}
