using ConsumerClientsApi.Clients.AngularGraphsApp;
using ConsumerClientsApi.Clients.CommandLineClients;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerClientsApi.SpecialClientsManager
{
    public class ClientsProcessManager
    {
        public bool IsGaugesApplicationOpen { get; set; }
        public bool IsFilterAppOpen { get; set; }
        public ClientsProcessManager()
        {
            this.IsGaugesApplicationOpen = false;
            this.IsFilterAppOpen = false;
        }
        private static ClientsProcessManager _instance;
        //singleton
        public static ClientsProcessManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ClientsProcessManager();
            }
            return _instance;
        }
        public bool OpenGaugesApplication()
        {
            if (!IsGaugesApplicationOpen)
            {
                IsGaugesApplicationOpen = true;
                // appsettings
                Process activateGaugesProcess = Process.Start(@"C:\Users\gilie\source\repos\LiveGagesApplication\LiveGagesApplication\bin\Release\LiveGagesApplication.exe");
                activateGaugesProcess.EnableRaisingEvents = true;
                activateGaugesProcess.Exited += (ProcessSender, exitCode) => { IsGaugesApplicationOpen = false; };
                return true;
            }
            return false;
        }
        public bool OpenFilterApplication()
        {
            if (!IsFilterAppOpen)
            {
                IsFilterAppOpen = true;
                Process activateFilterAppProcess = Process.Start(@"C:\Users\gilie\source\repos\FilterDataApplication\FilterDataApplication\bin\Release\FilterDataApplication.exe");
                activateFilterAppProcess.EnableRaisingEvents = true;
                activateFilterAppProcess.Exited += (ProcessSender, exitCode) => { IsFilterAppOpen = false; };
                return true;
            }
            return false;
        }
        public bool OpenAngularLiveGraphsApp()
        {
            AnguarAppActivate angularAppProcess = AnguarAppActivate.GetInstance();
            if (!angularAppProcess.IsRunning)
            {
                // activating python server on port 5000, appsetting path
                PythonScriptActivate serverActivate = new PythonScriptActivate(5000, @"C:\Users\gilie\PycharmProjects\pandas\Controller.py");
                serverActivate.ActivateServer();
                // activating angular application
                Task.Factory.StartNew(() => angularAppProcess.StartingAngularAppProcessAsync());
                return true;
            }
            return false;
        }
    }
}
