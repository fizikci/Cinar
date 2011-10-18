using System;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Threading;
//using System.IO;

namespace Cinar.CMS.Library.Handlers
{
    public class Console : GenericHandler
    {
        public override bool RequiresAuthorization
        {
            get { return true; }
        }

        public override string RequiredRole
        {
            get { return "Designer"; }
        }

        public override void ProcessRequest()
        {
            string cmd = context.Request["cmd"];
            StringBuilder sb = new StringBuilder();

            if (cmd != "hello")
                sb.AppendLine();

            try
            {
                if (!String.IsNullOrEmpty(cmd))
                {
                    if (cmd.StartsWith("!"))
                    {
                        sb.Append(executeProcess(cmd.Substring(1)));
                    }
                    else
                    {

                        string[] cmdParams = cmd.Split(' ');
                        object res = null;

                        string methodName = cmdParams[0].ToLower().Substring(0, 1).ToUpper() + cmdParams[0].Substring(1);

                        MethodInfo mi = typeof(ConsoleCommands).GetMethod(methodName);

                        if (mi == null)
                            throw new Exception("No such command.");

                        int methodParamCount = mi.GetParameters().Length;

                        if (methodParamCount > 0)
                        {
                            string[] cmdParamParts = new string[methodParamCount];
                            for (int i = 0; i < methodParamCount; i++)
                                cmdParamParts[i] = cmdParams.Length > i + 1 ? cmdParams[i + 1] : "";
                            for (int i = methodParamCount; i < cmdParams.Length - 1; i++)
                                cmdParamParts[methodParamCount - 1] += " " + cmdParams[i + 1];
                            res = mi.Invoke(null, cmdParamParts);
                        }
                        else
                        {
                            res = mi.Invoke(null, null);
                        }

                        if (res != null)
                            sb.Append(res.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                sb.Append("ERROR: " + Provider.ToString(ex, false) + "\n");
            }

            sb.AppendFormat("\n{0} #> ", DateTime.Now.ToShortTimeString());

            context.Response.Write(sb.ToString());
        }

        private string executeProcess(string command)
        {
            command = command.Trim();

            ProcessStartInfo pi = new ProcessStartInfo(Environment.GetEnvironmentVariable("COMSPEC"), @"/C " + command);
            pi.RedirectStandardOutput = true;
            pi.UseShellExecute = false;
            pi.WindowStyle = ProcessWindowStyle.Hidden;
            pi.StandardOutputEncoding = Encoding.UTF8;

            Process process = Process.Start(pi);
            Thread.Sleep(1000);
            string output = "\n" + (process.HasExited ? process.StandardOutput.ReadToEnd() : "process killed because of timeout");
            if(!process.HasExited) process.Kill();
            
            return output;
        }

    }
}