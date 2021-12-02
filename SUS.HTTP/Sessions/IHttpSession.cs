using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS.HTTP.Sessions
{
    public interface IHttpSession
    {
        string Id { get; }
        object GetParameter(string name);

        bool ContainsParameter(string name);

        void AddParameter(string name, object parameter);

        void ClearParameters();
    }
}
