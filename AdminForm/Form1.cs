﻿using System;
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
            str = new Stream("randomcensures", "oauth:gpbf3w9ou1afc08fx3wd3f4by6urei");
            //str = new Stream("randomcensures", "oauth:gpbf3w9ou1afc08fx3wd3f4by6urei");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            str.update(sender, e);
        }
    }
}
