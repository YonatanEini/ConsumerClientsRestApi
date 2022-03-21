using ConsumerClientsApi.AlertMessageEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerClientsApi.Clients.SocketClients
{
    public abstract class SocketClientBase
    {
        public int Port { get; set; }
        public SocketClientBase()
        {
            Port = 0;
        }
        public SocketClientBase(int portNumber)
        {
            Port = portNumber;
        }
        public abstract AlertsMessagEnum ActivateServer();

    }
}
