using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace CartaDePorte.Service.UI
{
    public partial class MainStatus : Form
    {
        bool isStarted = false;

        public MainStatus()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isStarted == false)
            {
                this.isStarted = true;
                this.label1.Text = "Iniciando...";
                this.Refresh();
                
                ProcessorServiceHelper.Start();

                this.label1.Text = "Iniciado";
                this.button1.Text = "Detener";
            }
            else
            {
                this.isStarted = false;
                this.label1.Text = "Deteniendo...";
                this.Refresh();

                ProcessorServiceHelper.Stop();

                this.label1.Text = "Detenido";
                this.button1.Text = "Iniciar";
            }            
        }

        private void MainStatus_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SelfInstaller.InstallMe();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SelfInstaller.UninstallMe();
        }
    }
}
