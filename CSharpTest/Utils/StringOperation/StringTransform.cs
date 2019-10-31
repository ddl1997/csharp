using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CSharpTest.Utils.StringOperation
{
    public class StringTransform
    {
        public static string StylePrice(double price)
        {
            string str = price.ToString();
            string res = "";
            string front, back;
            int point = str.IndexOf(".");
            if (point >= 0)
            {
                front = str.Substring(0, point);
                back = point + 1 >= str.Length ? "" : str.Substring(point + 1, str.Length - point - 2 > 0 ? 2 : str.Length - point - 1);
            }
            else
            {
                front = str;
                back = "";
            }

            for (int i = front.Length; i > 0; i -= 3)
            {
                int start = i - 3 >= 0 ? i - 3 : 0;
                res = front.Substring(start, i - start) + (res != "" ? "," : "") + res;
            }
            res = res + (back != "" ? "." : "") + back;
            return res;
        }

        public static string DigitalToChinese(double price)
        {
            string str = price.ToString();
            int point = str.IndexOf(".");
            string res = "";
            string number = "零壹贰叁肆伍陆柒捌玖";
            string unit = "仟佰拾亿仟佰拾万仟佰拾元";
            if (point >= 0)
            {
                res = (point + 1 < str.Length ? number[Convert.ToInt32(str[point + 1].ToString())].ToString() + "角" : "") + (point + 2 < str.Length ? number[Convert.ToInt32(str[point + 2].ToString())].ToString() + "分" : "");
                res = res.Replace("零角", "").Replace("零分", "");
                str = str.Substring(0, point);
            }
            if (res == "")
                res = "整";
            if (str.Length > unit.Length)
                return null;
            unit = unit.Substring(unit.Length - str.Length);
            for (int i = str.Length - 1; i >= 0; i--)
            {
                res = number[Convert.ToInt32(str[i].ToString())].ToString() + unit[i].ToString() + res;
            }
            Regex regex = new Regex("(零(亿|万|仟|佰|拾))+");
            res = regex.Replace(res, "零");
            return res;
        }
    }
}
