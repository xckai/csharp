using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using RefitTest;

namespace Refit
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var thread1 = new Thread(async () =>
            {

                var i = 1;
                var watch = new Stopwatch();
                watch.Start();
                while (true)
                {
                    if (i > 10000)
                    {
                        watch.Stop();
                        Console.WriteLine("Thread {0}, cost {1} ms", Thread.CurrentThread.ManagedThreadId, watch.ElapsedMilliseconds);
                        break;
                    }
                    await GetDetail();
                    ++i;
                }
            });
            var thread2 = new Thread(async () =>
            {
                var i = 1;
                var watch = new Stopwatch();
                watch.Start();
                while (true)
                {
                    if (i > 10000)
                    {
                        watch.Stop();
                        Console.WriteLine("Thread {0}, cost {1} ms",Thread.CurrentThread.ManagedThreadId, watch.ElapsedMilliseconds);
                        break;
                    }
                    await GetDetail();
                    ++i;
                }
            });
            var thread3 = new Thread(async () =>
            {
                var i = 1;
                var watch = new Stopwatch();
                watch.Start();
                while (true)
                {
                    if (i > 1000)
                    {
                        watch.Stop();
                        Console.WriteLine("Thread {0}, cost {1} ms", Thread.CurrentThread.ManagedThreadId, watch.ElapsedMilliseconds);
                        break;
                    }
                    await GetDetail();
                    ++i;
                }
            });
            thread1.Start();
            thread2.Start();
           // thread3.Start();
            Console.ReadKey();
        }

        static async Task<string> GetDetail()
        {
            var githubApi = RestService.For<IGithubApi>("http://localhost:5000/");
            var res = await githubApi.GetUsers();
            return res;
        }
    }
}
