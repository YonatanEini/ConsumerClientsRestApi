using ConsumerClientsApi.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerClientsApi.EndPintRequestModule
{
    public class EndPointProperties
    {
        public ConsumerClientsEnum RequestedClientType { get; set; }
        public int PortNumber { get; set; }
        public string IpAddress { get; set; }
        public EndPointProperties()
        {
            this.RequestedClientType = ConsumerClientsEnum.Tcp;
            this.PortNumber = 0;
            this.IpAddress = " ";
        }
        public EndPointProperties(ConsumerClientsEnum clientType, int port, string ip)
        {
            this.RequestedClientType = clientType;
            this.PortNumber = port;
            this.IpAddress = ip;
        }
    }
}
