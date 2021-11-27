using SUS.HTTP.Enums;
using SUS.HTTP.Headers;
using SUS.HTTP.Requests;
using SUS.HTTP.Responses;
using SUS.WebServer;
using SUS.WebServer.Results;
using SUS.WebServer.Routing;
using System;
using System.Text;

namespace SUS
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            serverRoutingTable.Add(HttpRequestMethod.Get, "/", httpRequest =>
            {
                return new HtmlResult("<h1>Hello World!</h1>", HttpResponseStatusCode.Ok);
            });

            Server server = new Server(8000, serverRoutingTable);
            server.Run();
        }
    }
}
