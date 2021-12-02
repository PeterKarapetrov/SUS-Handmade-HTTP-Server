using SUS.HTTP.Enums;
using SUS.HTTP.Requests;
using SUS.HTTP.Responses;
using SUS.WebServer.Results;

namespace Demo.App
{
    public class HomeController
    {
        public IHttpResponse Index(IHttpRequest request)
        {
            string content = "<h1>Hello World!</h1>";

            return new HtmlResult(content, HttpResponseStatusCode.Ok);
        }
    }
}
