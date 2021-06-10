using System;
using System.Linq;
using System.Threading;

namespace DelegateTest
{
    internal delegate void Feedback(int value);
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Feedback feedback = null;
            int i = 0;
            feedback += (int value) =>
            {
                Console.WriteLine($"Item1 = {value} ,{++i},{Thread.CurrentThread.ManagedThreadId}");
            };
            feedback += (int value) =>
            {
                Console.WriteLine($"Item2 = {value} ,{++i}");
            };
            feedback += (int value) =>
            {
                Console.WriteLine($"Item3 = {value} ,{++i}");
            };
            feedback += (int value) =>
            {
                Console.WriteLine($"Item4 = {value} ,{++i}");
            };
            Counter(1,3,feedback);
        }
        
        private static void StaticDelegate()
        {
            Console.WriteLine("---static---");
            Counter(1,3,null);
            Counter(1,3,new Feedback(Program.FeedbackToConsole));

        }

        private static void FeedbackToConsole(int value)
        {
            Console.WriteLine($"Item = {value}");
        }
        private static void FeedbackToConsole2(int value)
        {
            Console.WriteLine($"Item1= {value}");
        }
        private static void FeedbackToConsole3(int value)
        {
            Console.WriteLine($"Item2= {value}");
        }
        private static void Counter(int from, int to, Feedback fbd)
        {
            for(;from <= to; ++from)
            {
                if (fbd != null)
                {
                    var value = Volatile.Read(ref from);
                    // var fbArray = fbd.GetInvocationList();
                    new Thread(() =>
                   {
                     
                       ((Feedback) fbd)(value);

                   }).Start();
                   //fbArray.AsParallel().ForAll((@delegate) =>
                   //{
                   //    ((Feedback)@delegate)(from);
                   //});


                }
            }
        }
    }
}
