using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest
{
    class Program
    {
        static void Main(string[] args)
        {

           // ContinueTask();

            //Action startChildren = () =>
            //{
            //    Console.WriteLine(
            //        $"{DateTime.Now.ToLongTimeString()} -thread :{Thread.CurrentThread.ManagedThreadId} ");
            //    startChildThread();

            //};
            //CallWithTimeout(startChildren, 1000);
           //TaskTest.TaskParentTest();
           while (true)
           {
               try
               {
                   TaskTest.CancellationTokenTest();
               }
               catch (Exception e)
               {
                   Console.WriteLine(e.Message);
               }
           }
            Console.ReadKey();
        }

        public static void ContinueTask()
        {
            var t = TaskTest.Sum(1, 2)
                .ContinueWith(r => TaskTest.DoubleNum(r.Result).ContinueWith(r2 => TaskTest.LogOut(r2.Result)));
        }
        public static void CallWithTimeout(Action action, int timeoutMilliseconds)
        {
            Thread threadToKill = null;
            Action wrappedAction = () =>
            {
                threadToKill = Thread.CurrentThread;
                action();
            };

            var result = wrappedAction.BeginInvoke(null, null);
            if (result.AsyncWaitHandle.WaitOne(timeoutMilliseconds))
            {
                wrappedAction.EndInvoke(result);
            }
            else
            {
                threadToKill.Abort();
                Console.WriteLine(
                    $"{DateTime.Now.ToLongTimeString()} -thread abort , current state:{threadToKill.ThreadState}");
            }
        }

        static void startChildThread()
        {
            var thread = new Thread(delegate ()
            {
                Console.WriteLine($"{DateTime.Now.ToLongTimeString()} -Child thread :{Thread.CurrentThread.ManagedThreadId} ");
                try
                {
                    while (true)
                    {
                        Console.WriteLine($"{DateTime.Now.ToLongTimeString()} -Child thread :{Thread.CurrentThread.ManagedThreadId} ready to sleep, current state: {Thread.CurrentThread.ThreadState}");
                        
                        Thread.Sleep(3000);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine($"{DateTime.Now.ToLongTimeString()} -Child thread :{Thread.CurrentThread.ManagedThreadId} Error: {e.Message}");
                }
            });
            thread.Start();
            thread.Join(2000);
            Console.WriteLine(
                $"{DateTime.Now.ToLongTimeString()} -children thread abort ");
            thread.Abort();


        }
    }

}
