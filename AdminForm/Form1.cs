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
        FormAjout ajouterInsultes;
        FormAjoutPerio ajouterMessagesPeriodique;
        FormAjoutVoteValues ajouterVoteValues;
        private int i;
        private bool connexion = false;
        private bool pause = true;
        private bool voteStarted = false;
        private RandomCensures.Stream str;


        public Form1()
        {
            InitializeComponent();
            str = new RandomCensures.Stream();
        }

        /// <summary>
        /// Timer de test des messages périodiques
        /// </summary>
        /// TODO à supprimer proprement
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (connexion && pause != true)
            {
                
                if(i < LbMessagePeriodique.Items.Count)
                {
                    str.SendMessage("timerMessage", LbMessagePeriodique.Items[i].ToString());
                    i++;
                }
                else
                {
                    i = 0;
                }
             }
        }

        /// <summary>
        /// Action sur le CheckBox demandant au bot de changer l'accessibilité du chat au followers seulement ou non
        /// </summary>
        private void CbUniqAbo_CheckedChanged(object sender, EventArgs e)
        {
            if (pause != true)
            {
                str.member = CbUniqAbo.Checked;
                str.MemberOnly(); 
            }

        }

        /// <summary>
        /// Action sur le CheckBox activant ou desactivant le controle du flood
        /// </summary>
        private void CbAntiFlood_CheckedChanged(object sender, EventArgs e)
        {
            if (pause != true)
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
                connexionBT.FlatAppearance.BorderColor = Color.Green;
            }
            else
            {
                str.Dispose();
                connexion = false;
                this.pauseBT_MouseClick(sender, e);
                connexionBT.Text = "Connexion";
                connexionBT.FlatAppearance.BorderColor = Color.Red;
            }
        }

        /// <summary>
        /// Initialisation du windows form
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            int i = 0;
            MotBanniUpdate();
            MessagePeriodiqueUpdate();
            ajouterInsultes = new FormAjout();
            ajouterMessagesPeriodique = new FormAjoutPerio();
            ajouterVoteValues = new FormAjoutVoteValues();
            connexionBT.FlatStyle = FlatStyle.Flat;
            connexionBT.FlatAppearance.BorderColor = Color.Red;
            pauseBT.FlatStyle = FlatStyle.Flat;
            pauseBT.FlatAppearance.BorderColor = Color.Red;
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
        /// Mise à jour du ListBox contenant les messagesPeriodiques
        /// </summary>
        public void MessagePeriodiqueUpdate()
        {
            string sMot = File.ReadAllText("MessagesPerio.txt");
            string[] mots = sMot.Split(',');
            LbMessagePeriodique.Items.Clear();
            foreach (string mot in mots)
            {
                LbMessagePeriodique.Items.Add(mot);
            }
            LbMessagePeriodique.Refresh();
        }
        
        /// <summary>
        /// Suppression de l'insulte sélectionné dans le ListBox
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
            ajouterInsultes = new FormAjout();
            DialogResult diag = ajouterInsultes.ShowDialog();
            if (diag == DialogResult.OK)
            {
                string sMot = File.ReadAllText("Insultes.txt");
                string[] mots = sMot.Split(',');
                string ajoutMot = ajouterInsultes.getMots();
                string[] splitAjoutMot = ajoutMot.Split(',');
                string trimedWord;
                bool inList;
                foreach (string motAAjouter in splitAjoutMot)
                {
                    trimedWord = motAAjouter.TrimStart(' ').TrimEnd(' ');
                    mots = sMot.Split(',');
                    inList = false;
                    foreach (string mot in mots)
                    {
                        if (trimedWord.Equals(mot))
                        {
                            inList = true;
                        }
                    }
                    if (!inList)
                    {
                        sMot += "," + trimedWord;
                    }
                }
                File.WriteAllText("Insultes.txt", sMot);
                MotBanniUpdate();
            }
        }

        /// <summary>
        /// Affiche la fenetre d'ajout de messages periodique
        /// </summary>
        private void Bplus_MouseClick(object sender, MouseEventArgs e)
        {
            ajouterMessagesPeriodique = new FormAjoutPerio();
            DialogResult diag = ajouterMessagesPeriodique.ShowDialog();
            if (diag == DialogResult.OK)
            {
                string sMot = File.ReadAllText("MessagesPerio.txt");
                string[] mots = sMot.Split(',');
                string ajoutMot = ajouterMessagesPeriodique.getMessagesPerio();
                string[] splitAjoutMot = ajoutMot.Split(',');
                string trimedWord;
                bool inList;
                foreach (string motAAjouter in splitAjoutMot)
                {
                    trimedWord = motAAjouter.TrimStart(' ').TrimEnd(' ');
                    mots = sMot.Split(',');
                    inList = false;
                    foreach (string mot in mots)
                    {
                        if (trimedWord.Equals(mot))
                        {
                            inList = true;
                        }
                    }
                    if (!inList)
                    {
                        sMot += "," + trimedWord;
                    }
                }
                File.WriteAllText("MessagesPerio.txt", sMot);
                MessagePeriodiqueUpdate();
            }
        }

        /// <summary>
        /// Supprimer le message sélectionné dans le ListBox
        /// </summary>
        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            if (LbMessagePeriodique.SelectedIndex >= 0)
            {
                string sMessage = File.ReadAllText("MessagesPerio.txt");
                string[] messages = sMessage.Split(',');
                string messageASupprimer = LbMessagePeriodique.Items[LbMessagePeriodique.SelectedIndex].ToString();
                string tampList = "";
                foreach (string message in messages)
                {
                    if (!message.Equals(messageASupprimer))
                    {
                        tampList += "," + message.TrimEnd('\n').TrimStart('\n');
                    }
                }
                File.WriteAllText("MessagesPerio.txt", tampList.TrimEnd(',').TrimStart(','));
                this.MessagePeriodiqueUpdate();
            }
        }

        /// <summary>
        /// Met le bot en pause
        /// </summary>
        private void pauseBT_MouseClick(object sender, MouseEventArgs e)
        {
            if (pause)
            {
                str.start();
                pause = false;
                pauseBT.Text = "Pause bot";
                pauseBT.FlatAppearance.BorderColor = Color.Green;
            }else
            {
                str.stop();
                pause = true;
                pauseBT.Text = "Start bot";
                pauseBT.FlatAppearance.BorderColor = Color.Red;
            }
        }

        /// <summary>
        /// Remet à zero le listBox contenant les valeurs possible pour le vote
        /// </summary>
        private void btReinitVote_MouseClick(object sender, MouseEventArgs e)
        {
            lbVote.Items.Clear();
        }

        /// <summary>
        /// Lance le vote si aucun vote n'est déjà lancé
        /// </summary>
        private void btLancerVote_MouseClick(object sender, MouseEventArgs e)
        {
            if (pause != true)
            {
                string[] voteValues = new string[lbVote.Items.Count];
                for (int i = 0; i < lbVote.Items.Count; i++)
                {
                    voteValues[i] = lbVote.Items[i].ToString();
                }
                str.vote = new Vote(str, voteValues, 0);
                str.vote.voteStart();
                voteStarted = true;
            }
        }

        /// <summary>
        /// Ouvre la fenetre d'ajout de valeurs possible pour le prochain vote
        /// </summary>
        private void btAjoutVoteValue_MouseClick(object sender, MouseEventArgs e)
        {
            ajouterVoteValues = new FormAjoutVoteValues();
            DialogResult diag = ajouterVoteValues.ShowDialog();
            if (diag == DialogResult.OK)
            {
                string voteValues = ajouterVoteValues.getVoteValues();
                string[] splitValues = voteValues.Split(',');
                foreach (string value in splitValues)
                {
                    string trimedValue = value.TrimStart(' ').TrimEnd(' ');
                    if (!lbVote.Items.Contains(trimedValue))
                    {
                        lbVote.Items.Add(trimedValue);
                    }
                }
            }
        }

        /// <summary>
        /// Supprime la ligne sélectionné dans la listBox lbVote
        /// </summary>
        private void btSupprVoteValue_MouseClick(object sender, MouseEventArgs e)
        {
            lbVote.Items.RemoveAt(lbVote.SelectedIndex);
        }

        /// <summary>
        /// Met fin au vote précédemment lancé
        /// </summary>
        private void tbStopVote_MouseClick(object sender, MouseEventArgs e)
        {
            if (voteStarted && pause != true)
            {
                str.vote.voteStop();
                voteStarted = false;
            }
        }
    }
}
