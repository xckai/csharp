using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

namespace MiniNetCore
{
    public interface IHttpRequestFeature
    {
        Uri Url { get; }

        NameValueCollection Headers { get; }

        Stream Body { get; }
    }

    /// <summary>
    /// HttpContext与Server之间的抽象层之IHttpResponseFeature
    /// </summary>
    public interface IHttpResponseFeature
    {
        int StatusCode { get; set; }

        NameValueCollection Headers { get; }

        Stream Body { get; }
    }
    public interface IFeatureCollection : IDictionary<Type, object> { }
    public class FeatureCollection : Dictionary<Type, object>, IFeatureCollection { }
    public static partial class Extensions
    {
        public static T Get<T>(this IFeatureCollection features) => features.TryGetValue(typeof(T), out var value) ? (T)value : default(T);

        public static IFeatureCollection Set<T>(this IFeatureCollection features, T feature)
        {
            features[typeof(T)] = feature;
            return features;
        }
    }

}