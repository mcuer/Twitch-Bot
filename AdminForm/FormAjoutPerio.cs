﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminForm
{
    public partial class FormAjoutPerio : Form
    {
        public FormAjoutPerio()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Bouton validant l'ajout des mots dans la liste
        /// met à jour le ListBox et le fichier Insultes.txt puis ferme la fenetre
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        public string getMessagesPerio()
        {
            return ajoutMotTB.Text;
        }
    }
}
