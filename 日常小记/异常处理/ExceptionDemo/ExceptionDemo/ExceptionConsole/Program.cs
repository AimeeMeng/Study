using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
namespace ExceptionConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string maintrace = GetTraceStr();
            Console.WriteLine(string.Format("主线程调用栈：\r\n{0}",maintrace));

            Thread thread = new Thread(new ThreadStart(ThreadStartMethord));
            thread.Start();

            Console.ReadKey();
        }
        public static string GetTraceStr()
        {
            StackTrace trace = new StackTrace();
            string str = trace.ToString();
            return str;
        }

        public StackFrame[] GetTraceFrame()
        {
            StackTrace trace = new StackTrace();
            StackFrame[] frame = trace.GetFrames();
            return frame;
        }

        public static void ThreadStartMethord()
        {
            string subtrace = GetTraceStr();
            Console.WriteLine(string.Format("主线程调用栈：\r\n{0}", subtrace));
            
        }

        
    }
}
