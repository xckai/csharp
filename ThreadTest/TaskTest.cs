using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest
{
    public static  class TaskTest
    {
        public static  Task<int> Sum(int x, int y)
        {
            var t = new Task<int>(()=>x+y);
            t.Start();
            return t;
        }

        public static Task<int> DoubleNum(int x)
        {
            var t = new Task<int>(() => x*2);
            t.Start();
            return t;
        }

        public static void LogOut(object x)
        {
            Console.WriteLine(x.ToString());
        }

        public static void TaskParentTest()
        {
            var parent = new Task<int []>((() =>
            {
                var res = new int[2];
                int sumRes = -1;
                int addRes = -1;
                new Task((() =>
                {
                    Thread.Sleep(2000);
                    res[0] = 1 * 2;
                }), TaskCreationOptions.AttachedToParent).Start();
                new Task((() => res[1] = 1 + 2), TaskCreationOptions.AttachedToParent).Start();
                return res;
            }));
            parent.ContinueWith(pTask => Parallel.ForEach(pTask.Result, Console.WriteLine));
            parent.Start();
        }

        private static async Task<TResult> WithCancellationAsync<TResult>(this Task<TResult> originTask,
            CancellationToken ct)
        {
            var cancelTask = new TaskCompletionSource<int>();
            using (ct.Register(t => ((TaskCompletionSource<int>)t).TrySetResult(-1), cancelTask))
            {
                Task any = await Task.WhenAny(originTask, cancelTask.Task);
                if (any == cancelTask.Task) ct.ThrowIfCancellationRequested();
            }
            return await originTask;
        }
        private static async Task<TResult> WithTimeoutAsync<TResult>(this Task<TResult> originTask,int timeout = 5000,
            CancellationToken ct = default)
        {
            if (ct == default)
            {
                ct = new CancellationTokenSource(timeout).Token;
            }
            else
            {
                ct = CancellationTokenSource.CreateLinkedTokenSource(ct, new CancellationTokenSource(timeout).Token)
                    .Token;
            }
            var cancelTask = new TaskCompletionSource<int>();
            using (ct.Register(t => ((TaskCompletionSource<int>)t).TrySetResult(-1), cancelTask))
            {
                Task any = await Task.WhenAny(originTask, cancelTask.Task);
                if (any == cancelTask.Task) ct.ThrowIfCancellationRequested();
            }
            return await originTask;
        }
        private static  TResult WithCancellation<TResult>(this Task<TResult> originTask,
            CancellationToken ct)
        {
            try
            {
                return originTask.WithCancellationAsync(ct).Result;
            }
            catch (AggregateException aggregateException)
            {
                if (aggregateException.InnerException != null) throw aggregateException.InnerException;
                throw aggregateException.InnerExceptions.First();
            }
        }
        private static TResult WithCancelTimeout<TResult>(this Task<TResult> originTask,int timeout,
            CancellationToken ct = default)
        {
            try
            {
                return originTask.WithTimeoutAsync(timeout,ct).Result;
            }
            catch (AggregateException aggregateException)
            {
                if (aggregateException.InnerException != null) throw aggregateException.InnerException;
                throw aggregateException.InnerExceptions.First();
            }
        }
        public static  void CancellationTokenTest()
        {
            var cts = new CancellationTokenSource(3000);
            var t1= new Task<int>(() =>
            {
                Thread.Sleep(4000);
                Console.WriteLine("t1");
                return 1;
            });
            t1.Start();
            var res =t1.WithCancelTimeout(100, cts.Token);
            Console.WriteLine(res);
        }
    }
}