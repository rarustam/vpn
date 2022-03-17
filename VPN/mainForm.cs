using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPN
{
    public partial class mainForm : Form
    {
        // Перемение для час, минут и секунд
        int timeSec, timeMin, timeHour;
        bool timeActive;
        private readonly loginForm userLogin;
        public mainForm(loginForm login)
        {
            InitializeComponent();
            userLogin = login;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (timeActive)
            {
                timeSec++;
                if (timeSec >= 60)
                {
                    timeMin++;
                    timeSec = 0;
                    if (timeMin >= 60)
                    {
                        timeHour++;
                        timeMin = 0;
                    }
                }
            }
            DrowTime();
        }

        private void DrowTime()
        {
            if (timeSec <= 9)
                lblTimeSec.Text = "0" + String.Format("{0,00}", timeSec);
            else
                lblTimeSec.Text = String.Format("{0,00}", timeSec);
            if (timeMin <= 9)
                lblTimeMin.Text = "0" + String.Format("{0,00}", timeMin);
            else
                lblTimeMin.Text = String.Format("{0,00}", timeMin);
            if (timeHour <= 9)
                lblTimeHour.Text = "0" + String.Format("{0,00}", timeHour);
            else
                lblTimeHour.Text = String.Format("{0,00}", timeHour);
        }

        private void btnDiscon_Click(object sender, EventArgs e)
        {
            lblTimeSec.Text = "00";
            lblTimeMin.Text = "00";

            timeActive = false;
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            timeActive = true;
            label6.Text = "Имя ползователя:" + "  " + loginForm.SetValueForLogin.Trim();
            label4.Text = "Имя VPN:" + "  " + loginForm.SetValueForVPN.Trim();
        }
    }
}
