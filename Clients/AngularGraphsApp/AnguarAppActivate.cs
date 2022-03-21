using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsumerClientsApi.Clients.AngularGraphsApp
{
    public class AnguarAppActivate
    {
        public Process AngularCmdActivateProcess { get; set; }
        public Process WebPageProcess { get; set; }
        public bool IsRunning { get; set; }
        public AnguarAppActivate()
        {
            this.AngularCmdActivateProcess = new Process();
            this.WebPageProcess = new Process();
            this.IsRunning = false;
        }
        private static AnguarAppActivate _instance;
        //singleton
        public static AnguarAppActivate GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AnguarAppActivate();
            }
            return _instance;
        }
        public void SetProcessSetings()
        {
            //angular app process settings
            this.AngularCmdActivateProcess.StartInfo.FileName = "cmd.exe";
            this.AngularCmdActivateProcess.StartInfo.CreateNoWindow = false;
            this.AngularCmdActivateProcess.StartInfo.RedirectStandardInput = true;
            this.AngularCmdActivateProcess.StartInfo.RedirectStandardOutput = true;
            this.AngularCmdActivateProcess.StartInfo.UseShellExecute = false;
            this.AngularCmdActivateProcess.EnableRaisingEvents = true;
            this.WebPageProcess.StartInfo.UseShellExecute = true;
            //web page process settings
            //change to appsettings
            this.WebPageProcess.StartInfo.FileName = "http://localhost:4402";
        }
        /// <summary>
        /// Activates The Angular App (ng serve)
        /// </summary>
        /// <returns></returns>
        public async Task ActivateAngularProcessAsync()
        {
            this.IsRunning = true;
            //change path to appsettings
            await this.AngularCmdActivateProcess.StandardInput.WriteLineAsync(@"cd C:\Users\iaf\live-updated-graphs\live-updated-graphs-app");
            this.AngularCmdActivateProcess.StandardInput.Flush();
            this.AngularCmdActivateProcess.Exited += (ProcessSender, exitCode) => { IsRunning = false; };
            await this.AngularCmdActivateProcess.StandardInput.WriteLineAsync("ng serve  --port 4402");
            this.AngularCmdActivateProcess.StandardInput.Close();
            Console.WriteLine(this.AngularCmdActivateProcess.StandardOutput.ReadToEnd());
        }
        /// <summary>
        /// Activates The Angular App (ng serve) And Opens The App In The Browser
        /// </summary>
        /// <returns></returns>
        public async Task StartingAngularAppProcessAsync()
        {
            SetProcessSetings();
            //opening the angular web page (in the browser)
            this.WebPageProcess.Start();
            this.AngularCmdActivateProcess.Start();
            //cmd actions - cd to angular app path and activating the angular project (ng server in the cmd)
            await ActivateAngularProcessAsync();
        }
    }
}
