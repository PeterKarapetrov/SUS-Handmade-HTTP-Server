using SUS.HTTP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS.HTTP.Headers
{
    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly Dictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, HttpHeader>();
        }

        public void AddHeader(HttpHeader header)
        {
            CoreValidator.ThrowIfNull(header, nameof(header));

            this.headers.Add(header.Key, header);        
        }

        public bool ContainsHeader(string key)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));

            return this.headers.ContainsKey(key);
        }

        public HttpHeader GetHeader(string key)
        {

            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));

            if (ContainsHeader(key))
            {
                return this.headers[key];
            }
            else
            {
                return null;
            }
        }

        public override string ToString() => string.Join("\r\n", this.headers.Values.Select(header => header.ToString()));
    }
}
