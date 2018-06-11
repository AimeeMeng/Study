using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ExceptionForm
{
    public class logHelper
    {
        private static object iolock = new object();

        public static void log(string msg)
        {
            lock (iolock)
            {
                using (StreamWriter writer = File.AppendText("error.txt"))
                {
                    writer.WriteLine(msg);
                }
            }
        }
    }
}
