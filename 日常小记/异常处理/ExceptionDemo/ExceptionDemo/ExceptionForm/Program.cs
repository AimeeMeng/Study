using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
namespace ExceptionForm
{
    static class Program
    {
        
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        static void Application_ThreadException(object sender,ThreadExceptionEventArgs e)
        {
            Exception error = e.Exception as Exception;
            if (e.Exception is AggregateException)
            {
                AggregateException ex = e.Exception as AggregateException;
                foreach (var item in ex.InnerExceptions)
                {

                }
            }
            //记录日志  
            logHelper.log(error.Message);
        }
        //有异常未被捕获时触发
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ///调试状态下，由于调试机制问题，会让异常重复抛出，所以该方法会重复触发
            ///但是运行状态下，由多少个未处理异常就会触发几次，重新抛出也不会再触发
            Exception error = e.ExceptionObject as Exception;
            //记录日志  
            logHelper.log(error.Message);
            
        }
    }
}
