using ConsumerClientsApi.AlertMessageEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerClientsApi.Clients.SocketClients
{
    public class UdpProtocolServer : SocketClientBase
    {
        public UdpClient UdpServer { get; set; }
        public UdpProtocolServer(int port) : base(port)
        {
            this.UdpServer = null;
        }
        public override AlertsMessagEnum ActivateServer()
        {
            try
            {
                IPEndPoint ipep = new IPEndPoint(IPAddress.Any, base.Port);
                this.UdpServer = new UdpClient(ipep);
                _ = Task.Factory.StartNew(() => AcceptCalls());
                Console.WriteLine($"Activating Udp Server on Port {base.Port}");
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
        /// <summary>
        /// recive data from udp client and prints it
        /// </summary>
        public void AcceptCalls()
        {
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                byte[] data = this.UdpServer.Receive(ref sender);
                Console.WriteLine(Encoding.ASCII.GetString(data, 0, data.Length));
            }
        }
    }
}
