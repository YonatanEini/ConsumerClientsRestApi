using ConsumerClientsApi.AlertMessageEnum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerClientsApi.Clients.SocketClients
{
    public class TcpProtocolServer : SocketClientBase
    {
        public TcpListener Server { get; set; }
        public TcpProtocolServer(int portNo) : base(portNo)
        {
        }
        public override AlertsMessagEnum ActivateServer()
        {
            try
            {
                Server = new TcpListener(IPAddress.Any, base.Port);
                Server.Start();
                AcceptConnection();
                Console.WriteLine($"Activating Tcp Server on Port {Port}");
                return AlertsMessagEnum.ok;
            }
            catch (SocketException)
            {
                return AlertsMessagEnum.Exist;
            }
            catch (ArgumentOutOfRangeException)
            {
                return AlertsMessagEnum.Error;
            }
        }
        public void AcceptConnection()
        {
            this.Server.BeginAcceptTcpClient(HandleConnection, this.Server);
        }
        public void HandleConnection(IAsyncResult result)
        {
            AcceptConnection();
            TcpClient client = Server.EndAcceptTcpClient(result);
            _ = Task.Factory.StartNew(() => ReadData(client));
        }
        public void ReadData(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            byte[] data = new byte[1024];
            int bytes = ns.Read(data, 0, data.Length);
            while (bytes > 0)
            {
                string client_data = Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine(client_data);
                try
                {
                    bytes = ns.Read(data, 0, data.Length);
                }
                catch (IOException)
                {
                    bytes = 0;
                    Console.WriteLine("Client has dissconnect the server");
                }
            }
        }
    }
}
