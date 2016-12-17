using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace CartaDePorte.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (Environment.MachineName.ToUpper() == "WI7-SIS22N-ADM" && Environment.UserName.ToUpper().Contains("SPOSZALSKI"))
                RunDebug();

            if (args.Length == 0)
                RunService();
            else
            {
                string param = args[0].ToString().ToUpper();

                switch (param)
                {
                    case "INSTALL":
                    case "I":
                        //SelfInstaller.InstallMe();
                        break;
                    case "UNINSTALL":
                    case "U":
                        //SelfInstaller.UninstallMe();
                        break;
                    case "HELP":
                    case "/HELP":
                    case "?":
                    case "/?":

                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("");
                        sb.AppendLine("Opciones:");
                        sb.AppendLine("");
                        sb.AppendLine("  INSTALL, I   : Instala el servicio.");
                        sb.AppendLine("  UNINSTALL, U : Desinstala el servicio.");
                        sb.AppendLine("  WIN          : Ejecuta como aplicacion windows, SI ejecuta procesos.");
                        sb.AppendLine("");
                        MessageBox.Show(sb.ToString());

                        break;
                    default:
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new UI.MainStatus());
                        break;
                }
            }
        }

        private static void RunService()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] { new ProcessorService() };
            ServiceBase.Run(ServicesToRun);
        }

        private static void RunDebug()
        {
            string[] sArgs;
            sArgs = System.Environment.GetCommandLineArgs();
            Service1 oService = new Service1();
            oService.ServiceStart(sArgs);
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
            return;
        }
    }
}