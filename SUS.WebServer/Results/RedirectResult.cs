using SUS.HTTP.Headers;
using SUS.HTTP.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS.WebServer.Results
{
    public class RedirectResult : HttpResponse
    {
        public RedirectResult(string location)
            :base(HTTP.Enums.HttpResponseStatusCode.SeeOther)
        {
            this.Headers.AddHeader(new HttpHeader("Location", location));
        }
    }
}
