using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerClientsApi.Clients.WebPageClients
{
    public class WebPageProcessActivator
    {
        public string RequestedUrl { get; set; }
        public Process WebPageProcess { get; set; }
        public WebPageProcessActivator(string url)
        {
            this.RequestedUrl = url;
            this.WebPageProcess = new Process();
            CreateProcessSettings();
        }
        public void CreateProcessSettings()
        {
            this.WebPageProcess.StartInfo.UseShellExecute = true;
            this.WebPageProcess.StartInfo.FileName = this.RequestedUrl;
        }
    }
}
