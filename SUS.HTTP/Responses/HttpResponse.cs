using SUS.HTTP.Common;
using SUS.HTTP.Enums;
using SUS.HTTP.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS.HTTP.Responses
{
    public class HttpResponse : IHttpResponse
    {
        public HttpResponse()
        {
            this.Headers = new HttpHeaderCollection();
            this.Content = new byte[0];
        }

        public HttpResponse(HttpResponseStatusCode statusCode)
            : this()
        {
            CoreValidator.ThrowIfNull(statusCode, nameof(statusCode));
            this.StatusCode = statusCode;
        }

        public HttpResponseStatusCode StatusCode { get; set; }

        public IHttpHeaderCollection Headers { get; }

        public byte[] Content { get; set; }

        public void AddHeader(HttpHeader header)
        {
            CoreValidator.ThrowIfNull(header, nameof(header));
            this.Headers.AddHeader(header);
        }

        public byte[] GetBytes()
        {
            byte[] responseLine = Encoding.UTF8.GetBytes(this.ToString());

            byte[] result = new byte[responseLine.Length + this.Content.Length];

            Array.ConstrainedCopy(responseLine, 0, result, 0, responseLine.Length);
            Array.ConstrainedCopy(this.Content, 0, result, responseLine.Length , this.Content.Length);

            return result;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result
                .Append($"{GlobalConstants.HttpOneProtocolFragment} {(int)this.StatusCode} {this.StatusCode.ToString()}")
                .Append(GlobalConstants.HttpNewLine)
                .Append(this.Headers)
                .Append(GlobalConstants.HttpNewLine);

            result.Append(GlobalConstants.HttpNewLine);

            return result.ToString();
        }
    }
}
