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
using System.Diagnostics;
using System.IO;

namespace AdminForm
{
    public partial class Form1 : Form
    {
        FormAjout ajouter;
        private bool connexion = false;
        private RandomCensures.Stream str;
        public Form1()
        {
            InitializeComponent();
            str = new RandomCensures.Stream();

        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (connexion)
            {
                str.update(sender, e); 
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (connexion)
            {
                str.SendMessage("timerMessage", "Bonjour"); 
            }
        }

        private void CbUniqAbo_CheckedChanged(object sender, EventArgs e)
        {
            str.member = CbUniqAbo.Checked;
            str.MemberOnly();

        }

        private void CbAntiFlood_CheckedChanged(object sender, EventArgs e)
        {
            int floodLimit;
            try
            {
                floodLimit = Int32.Parse(TbNbMess.Text);
            }
            catch (Exception)
            {
                floodLimit = 5;
            }
            str.setAntiflood(CbAntiFlood.Checked, floodLimit);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://twitchapps.com/tmi");
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void connexionBT_MouseClick(object sender, MouseEventArgs e)
        {
            if (!connexion)
            {
                str.Init(uNameTB.Text, oAuthTB.Text);
                connexion = true;
                connexionBT.Text = "Déconnexion";
            }
            else
            {
                str.Disconnect();
                connexion = false;
                connexionBT.Text = "Connexion";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MotBanniUpdate();
            ajouter = new FormAjout(this);
        }

        public void MotBanniUpdate()
        {
            string sMot = File.ReadAllText("Insultes.txt");
            string[] mots = sMot.Split(',');
            LbMotBanni.Items.Clear();
            foreach (string mot in mots)
            {
                LbMotBanni.Items.Add(mot + "\n");
            }
            LbMotBanni.Refresh();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ajouter = new FormAjout(this);
            ajouter.Show();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if (ajouter.affiche)
            {
                ajouter.Focus();
            }
        }
    }
}
