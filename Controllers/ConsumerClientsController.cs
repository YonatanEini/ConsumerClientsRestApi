using ConsumerClientsApi.AlertMessageEnum;
using ConsumerClientsApi.Clients.ClientsFactory;
using ConsumerClientsApi.Clients.CommandLineClients;
using ConsumerClientsApi.Clients.SocketClients;
using ConsumerClientsApi.Clients.WebPageClients;
using ConsumerClientsApi.EndPintRequestModule;
using ConsumerClientsApi.Models;
using ConsumerClientsApi.SpecialClientsManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConsumerClientsApi.Controllers
{
    [Route("api/[controller]")]
    public class ConsumerClientsController : Controller
    {
        [HttpPost]
        public async Task CreateRequestedEndPointAsync([FromBody] EndPointProperties properties)
        {
            if (ModelState.IsValid)
            {
                ConsumerCientsFactory clientsFactory = ConsumerCientsFactory.GetInstance();
                SocketClientBase client = clientsFactory.GetClient(properties.RequestedClientType, properties.PortNumber, properties.IpAddress);
                AlertsMessagEnum alertType = await Task.Factory.StartNew(() => client.ActivateServer());
                if (alertType == AlertsMessagEnum.ok)
                {
                    Response.StatusCode = (int)HttpStatusCode.OK;
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }
        [HttpPost("ActivateMongoCompass")]
        public void ActivateMongodb()
        {
            MongodbActivator mongoActivator = MongodbActivator.GetInstance();
            mongoActivator.ActivateMongodb();
        }
        [HttpPost("OpenSplunkHttpEventCollector")]
        public void OpenSplunk()
        {
            WebPageProcessActivator webPageActivator = new WebPageProcessActivator("http://127.0.0.1:8000/en-US/app/search/search");
            webPageActivator.WebPageProcess.Start();
        }
        [HttpGet("OpenRequestedApplication/{requestedType}")]
        public void OpenRequestedApp(SpecialApplicationClientsEnum requestedType)
        {
            if (ModelState.IsValid)
            {
                ClientsProcessManager processManager = ClientsProcessManager.GetInstance();
                bool result = false;
                switch (requestedType)
                {
                    case SpecialApplicationClientsEnum.GaugesApp:
                        result = processManager.OpenGaugesApplication();
                        break;
                    case SpecialApplicationClientsEnum.FilterApp:
                        result = processManager.OpenFilterApplication();
                        break;
                    default:
                        result = processManager.OpenAngularLiveGraphsApp();
                        break;
                }
                if (result)
                {
                    Response.StatusCode = (int)HttpStatusCode.OK;
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
            }
        }
    }
}
