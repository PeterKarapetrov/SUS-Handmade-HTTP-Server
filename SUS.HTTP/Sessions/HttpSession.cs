using SUS.HTTP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS.HTTP.Sessions
{
    public class HttpSession : IHttpSession
    {
        private Dictionary<string, object> SessionParameters;

        public HttpSession(string id)
        {
            this.Id = id;
            this.SessionParameters = new Dictionary<string, object>();
        }

        public string Id { get; }

        public void AddParameter(string name, object parameter)
        {
            CoreValidator.ThrowIfNull(parameter, nameof(parameter));
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));

            this.SessionParameters.Add(name, parameter);
        }

        public void ClearParameters()
        {
            this.SessionParameters.Clear();
        }

        public bool ContainsParameter(string name)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));

            return this.SessionParameters.Keys.Count > 0;
        }

        public object GetParameter(string name)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));

            return this.SessionParameters[name];
        }
    }
}
