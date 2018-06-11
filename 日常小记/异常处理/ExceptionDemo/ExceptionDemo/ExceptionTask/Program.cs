using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace ExceptionTask
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskScheduler.UnobservedTaskException += (s, ex) =>
            {

                //设置所有未觉察异常被觉察
                ex.SetObserved();
                //异常处理代码
            };

            #region task异常处理
            //taskError.testTask1();
            //taskError.testTask2();
            //taskError.testTask3();
            try
            {
                taskError.testParallel();
            }
            catch (AggregateException ex)
            {
                foreach (Exception e in ex.InnerExceptions)
                {
                    Console.WriteLine("发生异常：" + e.Message);
                }
            }
            catch (Exception ex)
            {

            }
            

            #endregion

            #region  async异常处理

            //asyncError.testAsync1();

            //asyncError.testAsync2();

            //asyncError.testAsync3();

            #endregion
            while (true)
            {
                Thread.Sleep(1000);
                GC.Collect();
            }
        } 
    }
}
