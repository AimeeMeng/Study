using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionTask
{
    public class asyncError
    {
        private static async Task ThrowAfter(int timeout, Exception ex)
        {
            await Task.Delay(timeout);
            throw ex;
        }

        private static async Task Error1()
        {
            var t1 = ThrowAfter(1000, new NotSupportedException("Error 1"));
            var t2 = ThrowAfter(2000, new NotImplementedException("Error 2"));
            await Task.WhenAll(t1, t2);
        }

        private static async Task Error2()
        {
            Task all = null;
            var t1 = ThrowAfter(1000, new NotSupportedException("Error 1"));
            var t2 = ThrowAfter(2000, new NotImplementedException("Error 2"));
            try
            {
                await (all = Task.WhenAll(t1, t2));
            }
            catch (Exception ex) {
                //抛出所有异常
                throw all.Exception;
            }
           
        }

        /// <summary>
        /// 异常由UnobservedTaskException事件处理
        /// </summary>
        public static void testAsync1()
        {
            Task test = Error1();
        }

        /// <summary>
        /// 异常由ContinueWith处理
        /// </summary>
        public static void testAsync2()
        {
            Task test = Error1();
            test.ContinueWith(t =>
            {
                Exception ex = test.Exception;
            });
        }
        /// <summary>
        /// 处理所有异常
        /// </summary>
        public static void testAsync3()
        {
            Task test = Error2();
            test.ContinueWith(t =>
            {
                Exception ex = test.Exception;
            });
        }
    }
}
