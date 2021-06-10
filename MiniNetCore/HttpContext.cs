using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MiniNetCore
{
    public class HttpContext
    {
        /// <summary>
        /// Http请求对象
        /// </summary>
        public HttpRequest Request { get; }

        /// <summary>
        /// Http响应对象
        /// </summary>
        public HttpResponse Response { get; }

        public HttpContext(IFeatureCollection features)
        {
            Request = new HttpRequest(features);
            Response = new HttpResponse(features);
        }
    }
    public class HttpRequest
    {
        public HttpRequest(IFeatureCollection features) => _requestFeature = features.Get<IHttpRequestFeature>();

        private readonly IHttpRequestFeature _requestFeature;

        private Uri Url => _requestFeature.Url;

        private NameValueCollection Headers => _requestFeature.Headers;

        private Stream Body => _requestFeature.Body;
    }

    public class HttpResponse
    {
        private readonly IHttpResponseFeature _feature;
        public HttpResponse(IFeatureCollection features) => _feature = features.Get<IHttpResponseFeature>();
        public NameValueCollection Headers => _feature.Headers;
        public Stream Body => _feature.Body;
        public int StatusCode { get => _feature.StatusCode; set => _feature.StatusCode = value; }
    }
    public static partial class Extensions
    {
        /// <summary>
        /// 为HttpResponse对象扩展响应输出方法
        /// </summary>
        /// <param name="response">HttpResponse</param>
        /// <param name="contents">输出内容</param>
        /// <returns>Task</returns>
        public static Task WriteAsync(this HttpResponse response, string contents)
        {
            var buffer = Encoding.UTF8.GetBytes(contents);
            return response.Body.WriteAsync(buffer, 0, buffer.Length);
        }
    }
    public delegate Task RequestDelegate(HttpContext context);
}