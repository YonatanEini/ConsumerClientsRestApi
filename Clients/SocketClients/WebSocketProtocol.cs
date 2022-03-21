using ConsumerClientsApi.AlertMessageEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace ConsumerClientsApi.Clients.SocketClients
{
    public class WebSocketProtocol : SocketClientBase
    {
        public WebSocketServer WebServer { get; set; }
        public string IpAddress { get; set; }
        public WebSocketProtocol(string ip, int portNo) : base(portNo)
        {
            this.IpAddress = ip;
        }
        public override AlertsMessagEnum ActivateServer()
        {
            try
            {
                WebServer = new WebSocketServer("ws://" + this.IpAddress + ":" + base.Port);
                WebServer.AddWebSocketService<ServerResponse>("/DecodedFrames");
                WebServer.Start();
                Console.WriteLine($"Activating Web Socket");
                return AlertsMessagEnum.ok;
            }
            catch (SocketException)
            {
                return AlertsMessagEnum.Exist;
            }
            catch (ArgumentException)
            {
                return AlertsMessagEnum.Error;
            }

        }
        public void StopWebSocketServer()
        {
            WebServer.Stop();
        }
        public class ServerResponse : WebSocketBehavior
        {
            /// <summary>
            /// prints every message
            /// </summary>
            /// <param name="e"></param>
            protected override void OnMessage(MessageEventArgs e)
            {
                Console.WriteLine("Message From Client: " + e.Data);
            }
        }
    }
}
