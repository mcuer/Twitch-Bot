using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminForm
{
    public partial class FormAjout : Form
    {
        Form1 Origin;
        bool inList;
        public bool affiche;
        public FormAjout(Form1 origin)
        {
            this.Origin = origin;
            affiche = true;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sMot = File.ReadAllText("Insultes.txt");
            string[] ajoutMot = ajoutMotTB.Text.Split(',');
            string[] mots = sMot.Split(',');
            string trimedWord;
            foreach (string motAAjouter in ajoutMot)
            {
                trimedWord = motAAjouter.TrimStart(' ').TrimEnd(' ');
                inList = false;
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
            affiche = false;
            Origin.MotBanniUpdate();
            this.Dispose();
        }

        private void FormAjout_FormClosing(object sender, FormClosingEventArgs e)
        {
            affiche = false;
            this.Dispose();
        }
    }
}
