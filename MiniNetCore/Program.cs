using System;
using System.Net;
using System.Threading.Tasks;

namespace MiniNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Task.Run(() =>
            {
                new WebHostBuilder().UseHttpListener(new string[] {"http://localhost:5000/"}).Build().Run();
            });
            Console.ReadKey();
        }

        static async void RunServer()
        {
            var httpServer = new HttpListener();
            httpServer.Prefixes.Add("http://localhost:5000/");
            httpServer.Start();
            while (true)
            {

                var context = await httpServer.GetContextAsync();
                Console.WriteLine("{0} {1} HTTP/{2}",context.Request.HttpMethod,context.Request.RawUrl, context.Request.ProtocolVersion);
                context.Response.StatusCode = 200;
                context.Response.ContentType = "text/plain";
                var body = System.Text.Encoding.UTF8.GetBytes("haha");
                context.Response.OutputStream.Write(body,0, body.Length);
                context.Response.OutputStream.Close();
            }
        }
    }
}
