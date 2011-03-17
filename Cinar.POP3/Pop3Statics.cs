using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Cinar.POP3
{
    /// <summary>
    /// Summary description for Pop3Statics.
    /// </summary>
    public class Pop3Statics
    {
        public const string DataFolder = @"c:\POP3Temp";

        public static string FromQuotedPrintable(string inString)
        {
            string outputString = null;
            string inputString = inString.Replace("=\n", "");

            if (inputString.Length > 3)
            {
                // initialise output string ...
                outputString = "";

                for (int x = 0; x < inputString.Length; )
                {
                    string s1 = inputString.Substring(x, 1);

                    if ((s1.Equals("=")) && ((x + 2) < inputString.Length))
                    {
                        string hexString = inputString.Substring(x + 1, 2);

                        // if hexadecimal ...
                        if (Regex.Match(hexString.ToUpper(), @"^[A-F|0-9]+[A-F|0-9]+$").Success)
                        {
                            // convert to string representation ...
                            outputString += Encoding.ASCII.GetString(new byte[] {System.Convert.ToByte(hexString,16)});
                            x += 3;
                        }
                        else
                        {
                            outputString += s1;
                            ++x;
                        }
                    }
                    else
                    {
                        outputString += s1;
                        ++x;
                    }
                }
            }
            else
            {
                outputString = inputString;
            }

            return outputString.Replace("\n", "\r\n");
        }
    }
}
