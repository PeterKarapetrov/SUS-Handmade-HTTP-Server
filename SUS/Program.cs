using Demo.App;
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

            serverRoutingTable.Add(HttpRequestMethod.Get, "/", 
                httpRequest => new HomeController().Index(httpRequest));
            serverRoutingTable.Add(HttpRequestMethod.Get, "/login",
                httpRequest => new HomeController().Login(httpRequest));
            serverRoutingTable.Add(HttpRequestMethod.Get, "/logout",
                httpRequest => new HomeController().Logout(httpRequest));

            Server server = new Server(8000, serverRoutingTable);
            server.Run();
        }
    }
}
