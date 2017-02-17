using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RandomCensures;

namespace AdminForm
{
    public partial class Form1 : Form
    {
        private Stream str;
        public Form1()
        {
            InitializeComponent();
            //str = new Stream("randomcensures", "oauth:gpbf3w9ou1afc08fx3wd3f4by6urei");
            str = new Stream("Lolmarie69", "oauth:hx1am6hufq5yxhoeqbjaf9jqrix2rx");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            str.update(sender, e);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            str.SendMessage("timerMessage","Bonjour");
        }

        private void CbUniqAbo_CheckedChanged(object sender, EventArgs e)
        {
            str.Member = true;
            str.Reconnect();

        }
    }
}
