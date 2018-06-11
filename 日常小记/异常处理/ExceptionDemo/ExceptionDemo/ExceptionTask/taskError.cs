using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace ExceptionTask
{
    public class taskError
    {
        
        /// <summary>
        /// 异常交由主线程处理
        /// </summary>
        public static void testTask1()
        {

            Task task1 = Task.Factory.StartNew(() => {
                Thread.Sleep(1000);
                throw new Exception("ex1");
            });

            Task task2 = Task.Factory.StartNew(() => {
                Thread.Sleep(1000);
                throw new Exception("ex2");
            }).ContinueWith(t => t.Exception, TaskContinuationOptions.OnlyOnFaulted);
            try
            {
                Task.WaitAll(task1, task2);
            }
            catch { }

        }
        /// <summary>
        /// 异常交由UnobservedTaskException事件处理
        /// </summary>
        public static void testTask2()
        {
            ///后台任务，无需等待时
            Task task2 = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                throw new Exception("ex2");
            });
        }
        /// <summary>
        /// 异常交由ContinueWith函数处理
        /// </summary>
        public static void testTask3()
        {
            Task task2 = Task.Factory.StartNew(() => {
                Thread.Sleep(1000);
                throw new Exception("ex2");
            }).ContinueWith(t => 
            t.Exception
            , TaskContinuationOptions.OnlyOnFaulted);
        }

        public static void testParallel()
        {
            Parallel.For(0, 4, i =>
            {
                if (i == 3)
                {
                    throw new Exception("ParallelError");
                }
            });
        }
        
    }
}
