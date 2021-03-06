using SUS.HTTP.Common;
using SUS.HTTP.Enums;
using SUS.HTTP.Requests;
using SUS.HTTP.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS.WebServer.Routing
{
    public class ServerRoutingTable : IServerRoutingTable
    {
        private readonly Dictionary<HttpRequestMethod, Dictionary<string, Func<IHttpRequest, IHttpResponse>>> routes;

        public ServerRoutingTable()
        {
            this.routes = new Dictionary<HttpRequestMethod, Dictionary<string, Func<IHttpRequest, IHttpResponse>>>
            {
                [HttpRequestMethod.Get] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>(),
                [HttpRequestMethod.Post] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>(),
                [HttpRequestMethod.Delete] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>(),
                [HttpRequestMethod.Put] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>()
            };
        }

        public void Add(HttpRequestMethod requestMethod, string path, Func<IHttpRequest, IHttpResponse> func)
        {
            CoreValidator.ThrowIfNull(requestMethod, nameof(requestMethod));
            CoreValidator.ThrowIfNullOrEmpty(path, nameof(path));
            CoreValidator.ThrowIfNull(func, nameof(func));

            this.routes[requestMethod].Add(path, func);
        }

        public bool Contains(HttpRequestMethod requestMethod, string path)
        {
            CoreValidator.ThrowIfNull(requestMethod, nameof(requestMethod));
            CoreValidator.ThrowIfNullOrEmpty(path, nameof(path));

            return this.routes.ContainsKey(requestMethod) && this.routes[requestMethod].ContainsKey(path);
        }

        public Func<IHttpRequest, IHttpResponse> Get(HttpRequestMethod requestMethod, string path)
        {
            CoreValidator.ThrowIfNull(requestMethod, nameof(requestMethod));
            CoreValidator.ThrowIfNullOrEmpty(path, nameof(path));

            return this.routes[requestMethod][path];
        }
    }
}
