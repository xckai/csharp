using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniNetCore
{
  
   
    public class WebHostBuilder
    {
        private IServer _server;
        private readonly List<Action<ApplicationBuilder>> _configures = new List<Action<ApplicationBuilder>>();
        public WebHostBuilder Configure(Action<ApplicationBuilder> configure)
        {
            _configures.Add(configure);
            return this;
        }
        public WebHostBuilder UseServer(IServer server)
        {
            this._server = server;
            return this;
        }

        public WebHost Build()
        {
            var appBulider = new ApplicationBuilder();
            foreach (var configure in _configures)
            {
                configure(appBulider);
            }
            return new WebHost(_server, appBulider.Build());
        }
    }
}