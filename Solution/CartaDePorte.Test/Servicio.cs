using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Timers;

namespace CartaDePorte.Test
{
    public class Servicio
    {
        Timer timer = new Timer();
        Timer timerToken = new Timer();
        Timer timerEstadosCDP = new Timer();
        Timer timerActualizadorAFIP = new Timer();

        public void OnStart(string[] args)
        {
            int iTimer = 1;// Convert.ToInt32(ConfigurationManager.AppSettings["timer"]);
            int iTimerToken = 2;// Convert.ToInt32(ConfigurationManager.AppSettings["timerToken"]);
            int iTimerEstadosCDP = 3;// Convert.ToInt32(ConfigurationManager.AppSettings["timerEstadosCDP"]);
            int iTimerActualizadorAFIP = 4;// Convert.ToInt32(ConfigurationManager.AppSettings["timerActualizadorAFIP"]);

            Console.WriteLine("OnStart Service");
            timer = new Timer();
            timer.Interval = iTimer * 1000;
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Enabled = true;
            timer.Start();

            timerToken = new Timer();
            timerToken.Interval = iTimerToken * 1000;
            timerToken.Elapsed += new ElapsedEventHandler(OnTimedTokenEvent);
            timerToken.Enabled = true;
            timerToken.Start();

            timerEstadosCDP = new Timer();
            timerEstadosCDP.Interval = iTimerEstadosCDP * 1000;
            timerEstadosCDP.Elapsed += new ElapsedEventHandler(OnTimedEstadosEvent);
            timerEstadosCDP.Enabled = true;
            timerEstadosCDP.Start();

            timerActualizadorAFIP = new Timer();
            timerActualizadorAFIP.Interval = iTimerActualizadorAFIP * 1000;
            timerActualizadorAFIP.Elapsed += new ElapsedEventHandler(OnTimedUpdateEvent);
            timerActualizadorAFIP.Enabled = true;
            timerActualizadorAFIP.Start();

        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {

        }
        private void OnTimedTokenEvent(object source, ElapsedEventArgs e)
        {
        }
        private void OnTimedEstadosEvent(object source, ElapsedEventArgs e)
        {
        }
        private void OnTimedUpdateEvent(object source, ElapsedEventArgs e)
        {

        }

    }
}
