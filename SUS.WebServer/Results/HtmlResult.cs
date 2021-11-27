using SUS.HTTP.Enums;
using SUS.HTTP.Headers;
using SUS.HTTP.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS.WebServer.Results
{
    public class HtmlResult : HttpResponse
    {
        public HtmlResult(string content, HttpResponseStatusCode responseStatusCode)
            :base(responseStatusCode)
        {
            this.AddHeader(new HttpHeader("Content-Type", "text/html; charset=utf-8"));
            this.Content = Encoding.UTF8.GetBytes(content);
        }
    }
}
