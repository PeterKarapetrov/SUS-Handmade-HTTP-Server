using SUS.HTTP.Common;
using SUS.HTTP.Cookies;
using SUS.HTTP.Enums;
using SUS.HTTP.Exceptions;
using SUS.HTTP.Extensions;
using SUS.HTTP.Headers;
using SUS.HTTP.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS.HTTP.Requests
{
    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            CoreValidator.ThrowIfNull(requestString, nameof(requestString));

            this.FormData = new Dictionary<string, object>();
            this.QueryData = new Dictionary<string, object>();
            this.Headers = new HttpHeaderCollection();
            this.Cookies = new HttpCookieCollection();

            this.ParseRequest(requestString);
        }

        public string Path { get; private set; }

        public string Url { get; private set; }

        public IHttpSession Session { get; set; }

        public Dictionary<string, object> FormData { get; }

        public Dictionary<string, object> QueryData { get; }

        public IHttpHeaderCollection Headers { get;  }

        public IHttpCookieCollection Cookies { get;  }

        public HttpRequestMethod RequestMethod { get; private set; }

        private void ParseRequest(string requestString)
        {
            string[] splitRequestContent = requestString.Split(new[] { GlobalConstants.HttpNewLine }, StringSplitOptions.None);

            string[] requestLine = splitRequestContent[0].Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (!this.IsValidRequestLine(requestLine))
            {
                throw new BadRequestException();            
            }

            this.ParseRequestMethod(requestLine);
            this.ParseRequestUrl(requestLine);
            this.ParseRequestPath();

            this.ParseRequestHeaders(splitRequestContent.Skip(1).ToArray());

            this.ParseRequestCookies();
            
            this.ParseRequestParameters(splitRequestContent[splitRequestContent.Length - 1]);
        }

        private void ParseRequestParameters(string formData)
        {
            if (formData != "")
            {
                this.ParseQueryParameters();
                this.ParseFormDataParameters(formData);
            }           
        }

        private void ParseRequestCookies()
        {
            if (this.Headers.ContainsHeader(HttpHeader.Cookie))
            {
                var cookies = this.Headers.GetHeader(HttpHeader.Cookie).Value.Split("; ", StringSplitOptions.RemoveEmptyEntries);

                foreach (var cookie in cookies)
                {
                    var cookieTokens = cookie.Split("=", StringSplitOptions.RemoveEmptyEntries);

                    var newCookie = new HttpCookie(cookieTokens[0], cookieTokens[1], false);

                    this.Cookies.AddCookie(newCookie);
                }
            }      
        }

        private void ParseRequestHeaders(string[] unparsedHeaders)
        {
            foreach (string row in unparsedHeaders)
            {
                if (row != "")
                {
                    string headerKey = row.Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries)[0];
                    string headerValue = row.Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries)[1];

                    HttpHeader header = new HttpHeader(headerKey, headerValue);

                    this.Headers.AddHeader(header);
                }
                else
                {
                    break;
                }
            }

            if (!this.Headers.ContainsHeader("Host"))
            {
                throw new BadRequestException();
            }
        }

        private void ParseRequestPath()
        {
            this.Path = this.Url.Split(new[] { '?' }, StringSplitOptions.RemoveEmptyEntries)[0];
        }

        private void ParseRequestUrl(string[] requestLine)
        {
            this.Url = requestLine[1];
        }

        private void ParseRequestMethod(string[] requestLine)
        {
            this.RequestMethod = (HttpRequestMethod) Enum.Parse(typeof(HttpRequestMethod), requestLine[0].Capitalize());
        }

        private bool IsValidRequestLine(string[] requestLine)
        {
            if (requestLine.Length != 3)
            {
                return false;
            }

            if (requestLine[2] != GlobalConstants.HttpOneProtocolFragment)
            {
                return false;
            }

            return true;
        }

        private bool IsValidRequestQueryString(string queryString, string[] queryParameters)
        {
            return true;
        }

        private void ParseQueryParameters()
        {
            string queryString = this.Url.Split(new[] { '?', '#' }, StringSplitOptions.RemoveEmptyEntries)[1];

            string[] queryParameters = queryString.Split(new[] { '=', '&' }, StringSplitOptions.RemoveEmptyEntries);

            IsValidRequestQueryString(queryString, queryParameters);

            for (int i = 0; i < queryParameters.Length; i += 2)
            {
                this.QueryData.Add(queryParameters[i], queryParameters[i + 1]);
            }
        }

        private void ParseFormDataParameters(string formData)
        {
            string[] keyValuePairs = formData.Split(new[] { '=', '&' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < keyValuePairs.Length; i+=2)
            {
                this.FormData.Add(keyValuePairs[i], keyValuePairs[i + 1]);
            }
        }
    }
}
