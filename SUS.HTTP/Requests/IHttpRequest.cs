using SUS.HTTP.Cookies;
using SUS.HTTP.Enums;
using SUS.HTTP.Headers;
using SUS.HTTP.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS.HTTP.Requests
{
    public interface IHttpRequest
    {
        string Path { get; }

        string Url { get; }

        IHttpSession Session { get; set; }

        Dictionary<string, object> FormData { get;  }

        Dictionary<string, object> QueryData{ get; }

        IHttpHeaderCollection Headers { get; }

        HttpRequestMethod RequestMethod { get;  }

        IHttpCookieCollection Cookies { get; }
    }
}
