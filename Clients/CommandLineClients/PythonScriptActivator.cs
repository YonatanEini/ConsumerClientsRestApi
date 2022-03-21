using ConsumerClientsApi.AlertMessageEnum;
using ConsumerClientsApi.Clients.SocketClients;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerClientsApi.Clients.CommandLineClients
{
    public class PythonScriptActivate : SocketClientBase
    {
        public ProcessStartInfo PythonProcess { get; set; }
        public string PythonFilePath { get; set; }
        public PythonScriptActivate(int port, string path) : base(port)
        {
            this.PythonProcess = new ProcessStartInfo();
            this.PythonFilePath = path;
            AddProcessSettings();
        }
        public void AddProcessSettings()
        {
            //path to python.exe, move to appsettings
            this.PythonProcess.FileName = @"C:\Users\gilie\PycharmProjects\PythonGal\venv\Scripts\python.exe";
            //path to python file
            this.PythonProcess.Arguments = this.PythonFilePath + $" {Port}";
            this.PythonProcess.UseShellExecute = false;
            this.PythonProcess.RedirectStandardOutput = true;
            this.PythonProcess.RedirectStandardInput = true;

        }
        public override AlertsMessagEnum ActivateServer()
        {
            try
            {
                Task.Factory.StartNew(() => StartProcess());
                return AlertsMessagEnum.ok;
            }
            catch (InvalidOperationException)
            {
                return AlertsMessagEnum.Error;
            }

        }
        public void StartProcess()
        {
            using (Process process = Process.Start(this.PythonProcess))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.Write(result);
                };
            };
        }
    }
}
