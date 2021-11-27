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
    public interface IServerRoutingTable
    {
        void Add(HttpRequestMethod method, string path, Func<IHttpRequest, IHttpResponse> func);

        bool Contains(HttpRequestMethod requestMethod, string path);

        Func<IHttpRequest, IHttpResponse> Get(HttpRequestMethod requestMethod, string path);
    }
}
