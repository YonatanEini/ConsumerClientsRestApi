using ConsumerClientsApi.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerClientsApi.AlertMessageEnum
{
    public class AlertMessageFactory
    {
        private AlertMessageFactory() { }
        private static AlertMessageFactory _instance;
        //singleton
        public static AlertMessageFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AlertMessageFactory();

            }

            return _instance;
        }
        public string GetAlertMessage(AlertsMessagEnum alertType, ConsumerClientsEnum clientType)
        {
            string alertMessage;
            switch (alertType)
            {
                case AlertsMessagEnum.ok:
                    alertMessage = $"{clientType} client has sucessfully Created!";
                    break;
                case AlertsMessagEnum.Exist:
                    alertMessage = "you already have a client with the same properties!";
                    break;
                default:
                    alertMessage = "Invalid Client Properties!";
                    break;
            }
            return alertMessage;
        }
    }
}
