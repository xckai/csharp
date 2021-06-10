using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniNetCore
{
    public class ApplicationBuilder
    {
        private readonly List<Func<RequestDelegate, RequestDelegate>> _middlewares = new List<Func<RequestDelegate, RequestDelegate>>();

        public RequestDelegate Build()
        {
            return httpContext =>
            {
                RequestDelegate next = _ =>
                {
                    _.Response.StatusCode = 404;
                    return Task.CompletedTask;
                };
                foreach (var middleware in _middlewares)
                {
                    next = middleware(next);
                }

                return next(httpContext);
            };
        }

        public ApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            _middlewares.Add(middleware);
            return this;
        }
    }
}