using SUS.HTTP.Enums;
using SUS.HTTP.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS.HTTP.Responses
{
    public interface IHttpResponse
    {
        HttpResponseStatusCode StatusCode { get; set; }

        IHttpHeaderCollection Headers { get;  }

        byte[] Content { get; set; }

        void AddHeader(HttpHeader header);

        byte[] GetBytes();
    }
}
