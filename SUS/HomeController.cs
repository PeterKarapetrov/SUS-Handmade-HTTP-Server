using SUS.HTTP.Cookies;
using SUS.HTTP.Enums;
using SUS.HTTP.Requests;
using SUS.HTTP.Responses;
using SUS.WebServer.Results;
using System;

namespace Demo.App
{
    public class HomeController
    {
        public IHttpResponse Index(IHttpRequest httpRequest)
        {
            string content = "<h1>Hello World!</h1>";

            if (isLogedIn(httpRequest))
            {
                content = $"<h1>Hello, Mr. {httpRequest.Session.GetParameter("username")}!</h1>";
            }

            HttpResponse httpResponse = new HtmlResult(content, HttpResponseStatusCode.Ok);

            httpResponse.Cookies.AddCookie(new HttpCookie("lang", "en"));

            return httpResponse;
        }

        private bool isLogedIn(IHttpRequest httpRequest)
        {
            return httpRequest.Session.ContainsParameter("username");
        }

        public IHttpResponse Login(IHttpRequest httpRequest)
        {
            httpRequest.Session.AddParameter("username", "Pesho");

            return this.Redirect("/");
        
        }
        public IHttpResponse Logout(IHttpRequest httpRequest)
        {
            httpRequest.Session.ClearParameters();

            return this.Redirect("/");
        }


        private IHttpResponse Redirect(string url)
        {
            return new RedirectResult(url);
        }
    }
}
