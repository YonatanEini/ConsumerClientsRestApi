using ConsumerClientsApi.Clients.CommandLineClients;
using ConsumerClientsApi.Clients.SocketClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerClientsApi.Clients.ClientsFactory
{
    public class ConsumerCientsFactory
    {
        private ConsumerCientsFactory() { }
        private static ConsumerCientsFactory _instance;
        //singleton
        public static ConsumerCientsFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ConsumerCientsFactory();
            }
            return _instance;
        }
        public SocketClientBase GetClient(ConsumerClientsEnum client, int port, string ipAddress)
        {
            SocketClientBase consumerClient;
            switch (client)
            {
                case ConsumerClientsEnum.Tcp:
                    consumerClient = new TcpProtocolServer(port);
                    break;
                case ConsumerClientsEnum.Udp:
                    consumerClient = new UdpProtocolServer(port);
                    break;
                /*
            case ConsumerClientsEnum.Http:
                consumerClient = new HttpProtocolServer(port);
                break;
                */
                case ConsumerClientsEnum.PythonHttp:
                    // change to appsettings
                    consumerClient = new PythonScriptActivate(port, @"C:\Users\gilie\PycharmProjects\pandas\Controller.py");
                    break;
                default:
                    consumerClient = new WebSocketProtocol(ipAddress, port);
                    break;
            }
            return consumerClient;
        }
    }
}