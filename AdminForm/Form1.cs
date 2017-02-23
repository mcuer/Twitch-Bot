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

        /// <summary>
        /// Timer lancant la mise à jour de la récéption des messages par le bot (Stream)
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (connexion)
            {
                str.update(sender, e);
            }
        }

        /// <summary>
        /// Timer de test des messages périodiques
        /// </summary>
        /// TODO à supprimer proprement
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (connexion)
            {
                str.SendMessage("timerMessage", "Bonjour");
            }
        }

        /// <summary>
        /// Action sur le CheckBox demandant au bot de changer l'accessibilité du chat au followers seulement ou non
        /// </summary>
        private void CbUniqAbo_CheckedChanged(object sender, EventArgs e)
        {
            str.member = CbUniqAbo.Checked;
            str.MemberOnly();

        }

        /// <summary>
        /// Action sur le CheckBox activant ou desactivant le controle du flood
        /// </summary>
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

        /// <summary>
        /// Ouvre le navigateur web par défaut et envoie l'utilisateur sur la page permettant d'optenir l'identifiant oAuth
        /// </summary>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://twitchapps.com/tmi");
        }
        
        /// <summary>
        /// Boutton connexion, Lance la connexion si pas déjà connecté, sinon coupe la connexion
        /// </summary>
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
                str.Dispose();
                connexion = false;
                connexionBT.Text = "Connexion";
            }
        }

        /// <summary>
        /// Initialisation du windows form
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            MotBanniUpdate();
            ajouter = new FormAjout(this);
        }

        /// <summary>
        /// Mise à jour du ListBox contenant les insultes
        /// </summary>
        public void MotBanniUpdate()
        {
            string sMot = File.ReadAllText("Insultes.txt");
            string[] mots = sMot.Split(',');
            LbMotBanni.Items.Clear();
            foreach (string mot in mots)
            {
                LbMotBanni.Items.Add(mot);
            }
            LbMotBanni.Refresh();
        }

        /// <summary>
        /// Force le focus sur la fenetre d'ajout d'insultes tant qu'elle est affiché
        /// </summary>
        private void Form1_Click(object sender, EventArgs e)
        {
            if (ajouter.affiche)
            {
                ajouter.Focus();
            }
        }

        /// <summary>
        /// Suppression de l'insulte séléctionné dans le ListBox
        /// </summary>
        private void button2_MouseClick(object sender, MouseEventArgs e)
        {
            if (LbMotBanni.SelectedIndex >= 0)
            {
            string sMot = File.ReadAllText("Insultes.txt");
            string[] mots = sMot.Split(',');
                string motASupprimer = LbMotBanni.Items[LbMotBanni.SelectedIndex].ToString(); 
            string tampList = "";
            foreach (string mot  in mots)
            {
                if (!mot.Equals(motASupprimer))
                {
                    tampList += "," + mot.TrimEnd('\n').TrimStart('\n'); 
                }
            }
            File.WriteAllText("Insultes.txt", tampList.TrimEnd(',').TrimStart(','));
            this.MotBanniUpdate();
            }
        }

        /// <summary>
        /// Affiche la fenetre d'ajout d'insultes
        /// </summary>
        private void btAjouter_MouseClick(object sender, MouseEventArgs e)
        {
            ajouter = new FormAjout(this);
            ajouter.Show();
        }
        
    }
}
