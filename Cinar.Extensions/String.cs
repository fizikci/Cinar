using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Cinar.Extensions
{
    public static class String
    {
        public static float ToFloat(this string s)
        {
            string res = "";
            for (int i = 0; i < s.Length; i++)
                if (Char.IsDigit(s[i]) || s[i] == '.')
                    res += s[i];
            return float.Parse(res);
        }
        public static int ToInt(this string s, int baseNumber)
        {
            if (baseNumber == 16)
            {
                if (s.Length == 1)
                {
                    if (s == "A") return 10;
                    else if (s == "B") return 11;
                    else if (s == "C") return 12;
                    else if (s == "D") return 13;
                    else if (s == "E") return 14;
                    else if (s == "F") return 15;
                    else if (Char.IsDigit(s[0])) return int.Parse(s);
                    else return 0;
                }
            }
            int total = 0;
            for (int i = 0; i < s.Length; i++)
                total += (int)Math.Pow(baseNumber, i) * s[s.Length - i - 1].ToString().ToInt(baseNumber);
            return total;
        }
        public static Color ToColor(this string s)
        {
            if (s.StartsWith("#"))
            {
                s = s.Substring(1);
                if (s.Length == 6)
                {
                    int red = s.Substring(0, 2).ToInt(16);
                    int green = s.Substring(2, 2).ToInt(16);
                    int blue = s.Substring(4, 2).ToInt(16);
                    return Color.FromArgb(red, green, blue);
                }
                else if (s.Length == 3)
                {
                    int red = (s[0] + "" + s[0]).ToInt(16);
                    int green = (s[1] + "" + s[1]).ToInt(16);
                    int blue = (s[2] + "" + s[2]).ToInt(16);
                    return Color.FromArgb(red, green, blue);
                }
            }
            else
            {
                Color cl = Color.FromName(s);
                if (s.ToLower() != "black" && cl.ToArgb() == 0)
                    return ("#" + s).ToColor();
                else
                    return cl;
            }

            return Color.Red;
        }

        public static bool ContainedBy(this string str, List<string> list)
        {
            return list.Any(s => s == str);
        }
    }
}
