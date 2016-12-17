using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Configuration;
using CartaDePorte.Core.Servicios;
using CartaDePorte.Core.DAO;

namespace CartaDePorte.Service
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
        Timer timerToken = new Timer();
        Timer timerEstadosCDP = new Timer();
        Timer timerActualizadorAFIP = new Timer();
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                int iTimer = Convert.ToInt32(ConfigurationManager.AppSettings["timer"]);
                int iTimerToken = Convert.ToInt32(ConfigurationManager.AppSettings["timerToken"]);
                int iTimerEstadosCDP = Convert.ToInt32(ConfigurationManager.AppSettings["timerEstadosCDP"]);
                int iTimerActualizadorAFIP = Convert.ToInt32(ConfigurationManager.AppSettings["timerActualizadorAFIP"]);

                Console.WriteLine("OnStart Service");
                timer = new Timer();
                timer.Interval = iTimer * 60000;
                timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                timer.Enabled = true;
                timer.Start();

                timerToken = new Timer();
                timerToken.Interval = iTimerToken * 60000;
                timerToken.Elapsed += new ElapsedEventHandler(OnTimedTokenEvent);
                timerToken.Enabled = true;
                timerToken.Start();

                timerEstadosCDP = new Timer();
                timerEstadosCDP.Interval = iTimerEstadosCDP * 60000;
                timerEstadosCDP.Elapsed += new ElapsedEventHandler(OnTimedEstadosEvent);
                timerEstadosCDP.Enabled = true;
                timerEstadosCDP.Start();

                timerActualizadorAFIP = new Timer();
                timerActualizadorAFIP.Interval = iTimerActualizadorAFIP * 60000;
                timerActualizadorAFIP.Elapsed += new ElapsedEventHandler(OnTimedUpdateEvent);
                timerActualizadorAFIP.Enabled = true;
                timerActualizadorAFIP.Start();

                CartaDePorte.Common.Tools.Logger.InfoFormat("Servicio de Carta de Porte start Ok." + " " + EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                CartaDePorte.Common.Tools.Logger.InfoFormat(ex.Message + " " + EventLogEntryType.Error);
            }
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            var ws = new wsSAP();
            ws.PrefacturaSAP(true); // true= busca a anular
            ws.PrefacturaSAP(false); // false = busca a prefacturar
        }

        private void OnTimedTokenEvent(object source, ElapsedEventArgs e)
        {
            AfipAuthDAO.Instance.RenovarTokenAfip();
        }

        private void OnTimedEstadosEvent(object source, ElapsedEventArgs e)
        {
            SolicitudDAO.Instance.ConsultaDeEstadosAFIP();
        }

        private void OnTimedUpdateEvent(object source, ElapsedEventArgs e)
        {
            wsAfip_v3 wsa = new wsAfip_v3();
            wsa.ActualizarCosecha();
            wsa.ActualizarEspecie();
        }

        protected override void OnStop()
        {
        }

        public void ServiceStart(string[] args)
        {
            OnStart(args);
        }

        private Boolean EsDiaHabil()
        {
            Boolean habil = true;

            DayOfWeek dia = DateTime.Now.DayOfWeek;
            if (dia == DayOfWeek.Saturday || dia == DayOfWeek.Sunday)
                habil = false;

            return habil;
        }

        private Boolean EsLunes()
        {
            DayOfWeek dia = DateTime.Now.DayOfWeek;
            if (dia == DayOfWeek.Monday)
                return true;

            return false;
        }

        private Boolean SuperaLaHora(int hora)
        {
            if (DateTime.Now.Hour >= hora)
                return true;

            return false;
        }
    }
}
