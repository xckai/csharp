using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;

namespace csharp
{
    class LoggerInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine(("called "));
            var methodName = invocation.Method.Name;
            invocation.Proceed();
            Console.WriteLine(("called "+methodName));
        }
    }
}
