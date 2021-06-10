using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;

namespace WebTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        private IComponentContext _componentContext;//Autofac上下文

        public HelloWorldController( IComponentContext componentContext)
        {
            this._componentContext = componentContext;
        }

        private IScopedService service;
        [HttpGet("service")]
        public string GetService()
        {
            var serviceInstance = _componentContext.Resolve<CommonGuid>();
            Console.WriteLine();
            Console.WriteLine("singleton " + serviceInstance.GetGuid);

          
            Console.WriteLine("===========分割线=====================");
            Console.WriteLine();
            Console.WriteLine();

            return "成功";
        }
        [HttpGet]
        public string Index()
        {
            return "This is my default action...";
        }

        // 
        // GET: /HelloWorld/Welcome/ 
        [HttpGet("welcome")]
        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
    }
}
