using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumerClientsApi.Clients.CommandLineClients
{
    public class MongodbActivator
    {
        public Process MongodbProcess { get; set; }
        public bool IsMongoSeverRunning { get; set; }
        public MongodbActivator()
        {
            this.MongodbProcess = new Process();
            this.MongodbProcess.StartInfo.FileName = "cmd.exe";
            SetProcessSettings();
            this.IsMongoSeverRunning = false;
        }
        private static MongodbActivator _instance;
        //singleton
        public static MongodbActivator GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MongodbActivator();
            }
            return _instance;
        }
        public void SetProcessSettings()
        {
            this.MongodbProcess.StartInfo.CreateNoWindow = true;
            this.MongodbProcess.StartInfo.RedirectStandardInput = true;
            this.MongodbProcess.StartInfo.RedirectStandardOutput = true;
            this.MongodbProcess.StartInfo.UseShellExecute = false;
        }
        public void ActivateCommand(string command)
        {
            this.MongodbProcess.StandardInput.WriteLine(command);
            this.MongodbProcess.StandardInput.Flush();
        }
        public void ActivateMongodb()
        {
            if (!IsMongoSeverRunning)
            {
                this.MongodbProcess.Start();
                //change to appsettings
                ActivateCommand(@"cd C:\Program Files\MongoDB\Server\5.0\bin");
                ActivateCommand("mongod");
                this.MongodbProcess.StandardInput.Close();
                Console.WriteLine(this.MongodbProcess.StandardOutput.ReadToEnd());
            }
            _ = Process.Start(@"C:\Users\gilie\AppData\Local\MongoDBCompass\MongoDBCompass.exe");
        }
    }
}
