using System.ComponentModel;

namespace AdminForm
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.LbMotBanni = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CbUniqAbo = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CbAntiFlood = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TbNbMess = new System.Windows.Forms.TextBox();
            this.Bplus = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.LbFrequence = new System.Windows.Forms.ListBox();
            this.LbMessagePeriodique = new System.Windows.Forms.ListBox();
            this.uNameTB = new System.Windows.Forms.TextBox();
            this.oAuthTB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.connexionBT = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mot Banni";
            // 
            // LbMotBanni
            // 
            this.LbMotBanni.FormattingEnabled = true;
            this.LbMotBanni.Location = new System.Drawing.Point(12, 103);
            this.LbMotBanni.Name = "LbMotBanni";
            this.LbMotBanni.Size = new System.Drawing.Size(234, 160);
            this.LbMotBanni.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(315, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Message Periodique";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 278);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Uniquement Abonnées ";
            // 
            // CbUniqAbo
            // 
            this.CbUniqAbo.AutoSize = true;
            this.CbUniqAbo.Location = new System.Drawing.Point(136, 278);
            this.CbUniqAbo.Name = "CbUniqAbo";
            this.CbUniqAbo.Size = new System.Drawing.Size(15, 14);
            this.CbUniqAbo.TabIndex = 5;
            this.CbUniqAbo.UseVisualStyleBackColor = true;
            this.CbUniqAbo.CheckedChanged += new System.EventHandler(this.CbUniqAbo_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 299);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Anti Flood";
            // 
            // CbAntiFlood
            // 
            this.CbAntiFlood.AutoSize = true;
            this.CbAntiFlood.Location = new System.Drawing.Point(136, 299);
            this.CbAntiFlood.Name = "CbAntiFlood";
            this.CbAntiFlood.Size = new System.Drawing.Size(15, 14);
            this.CbAntiFlood.TabIndex = 7;
            this.CbAntiFlood.UseVisualStyleBackColor = true;
            this.CbAntiFlood.CheckedChanged += new System.EventHandler(this.CbAntiFlood_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(194, 296);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Nb Message";
            // 
            // TbNbMess
            // 
            this.TbNbMess.Location = new System.Drawing.Point(284, 293);
            this.TbNbMess.Name = "TbNbMess";
            this.TbNbMess.Size = new System.Drawing.Size(100, 20);
            this.TbNbMess.TabIndex = 9;
            this.TbNbMess.Text = "5";
            this.TbNbMess.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TbNbMess.TextChanged += new System.EventHandler(this.CbAntiFlood_CheckedChanged);
            // 
            // Bplus
            // 
            this.Bplus.Location = new System.Drawing.Point(528, 77);
            this.Bplus.Name = "Bplus";
            this.Bplus.Size = new System.Drawing.Size(26, 23);
            this.Bplus.TabIndex = 10;
            this.Bplus.Text = "+";
            this.Bplus.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 100000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // LbFrequence
            // 
            this.LbFrequence.FormattingEnabled = true;
            this.LbFrequence.Location = new System.Drawing.Point(505, 103);
            this.LbFrequence.Name = "LbFrequence";
            this.LbFrequence.Size = new System.Drawing.Size(69, 160);
            this.LbFrequence.TabIndex = 11;
            // 
            // LbMessagePeriodique
            // 
            this.LbMessagePeriodique.FormattingEnabled = true;
            this.LbMessagePeriodique.Location = new System.Drawing.Point(315, 103);
            this.LbMessagePeriodique.Name = "LbMessagePeriodique";
            this.LbMessagePeriodique.Size = new System.Drawing.Size(184, 160);
            this.LbMessagePeriodique.TabIndex = 3;
            // 
            // uNameTB
            // 
            this.uNameTB.Location = new System.Drawing.Point(15, 34);
            this.uNameTB.Name = "uNameTB";
            this.uNameTB.Size = new System.Drawing.Size(147, 20);
            this.uNameTB.TabIndex = 12;
            // 
            // oAuthTB
            // 
            this.oAuthTB.Location = new System.Drawing.Point(168, 34);
            this.oAuthTB.Name = "oAuthTB";
            this.oAuthTB.Size = new System.Drawing.Size(147, 20);
            this.oAuthTB.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "UserName :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(165, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "oAuth :";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(173, 60);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(112, 13);
            this.linkLabel1.TabIndex = 16;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Générer son oAuth ici!";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // connexionBT
            // 
            this.connexionBT.Location = new System.Drawing.Point(341, 23);
            this.connexionBT.Name = "connexionBT";
            this.connexionBT.Size = new System.Drawing.Size(104, 37);
            this.connexionBT.TabIndex = 17;
            this.connexionBT.Text = "Connexion";
            this.connexionBT.UseVisualStyleBackColor = true;
            this.connexionBT.Click += new System.EventHandler(this.button1_Click);
            this.connexionBT.MouseClick += new System.Windows.Forms.MouseEventHandler(this.connexionBT_MouseClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 331);
            this.Controls.Add(this.connexionBT);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.oAuthTB);
            this.Controls.Add(this.uNameTB);
            this.Controls.Add(this.LbFrequence);
            this.Controls.Add(this.Bplus);
            this.Controls.Add(this.TbNbMess);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CbAntiFlood);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CbUniqAbo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LbMessagePeriodique);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LbMotBanni);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LbMotBanni;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox CbUniqAbo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox CbAntiFlood;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TbNbMess;
        private System.Windows.Forms.Button Bplus;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ListBox LbFrequence;
        private System.Windows.Forms.ListBox LbMessagePeriodique;
        private System.Windows.Forms.TextBox uNameTB;
        private System.Windows.Forms.TextBox oAuthTB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button connexionBT;
    }
}

