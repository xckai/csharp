using System.Threading.Tasks;

namespace MiniNetCore
{
    public class WebHost
    {
        private readonly IServer _server;
        private readonly RequestDelegate _handler;
        public WebHost(IServer server, RequestDelegate handler)
        {
            _server = server;
            _handler = handler;
        }

        /// <summary>
        /// 调用Server的启动方法进行启动
        /// </summary>
        /// <returns></returns>
        public Task Run() => _server.RunAsync(_handler);
    }
}