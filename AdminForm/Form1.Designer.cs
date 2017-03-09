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
            this.LbMotBanni = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CbUniqAbo = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CbAntiFlood = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TbNbMess = new System.Windows.Forms.TextBox();
            this.Bplus = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.LbMessagePeriodique = new System.Windows.Forms.ListBox();
            this.uNameTB = new System.Windows.Forms.TextBox();
            this.oAuthTB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.connexionBT = new System.Windows.Forms.Button();
            this.btAjouter = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pauseBT = new System.Windows.Forms.Button();
            this.lbVote = new System.Windows.Forms.ListBox();
            this.btAjoutVoteValue = new System.Windows.Forms.Button();
            this.btSupprVoteValue = new System.Windows.Forms.Button();
            this.btLancerVote = new System.Windows.Forms.Button();
            this.btReinitVote = new System.Windows.Forms.Button();
            this.tbStopVote = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // LbMotBanni
            // 
            this.LbMotBanni.FormattingEnabled = true;
            this.LbMotBanni.Location = new System.Drawing.Point(6, 49);
            this.LbMotBanni.Name = "LbMotBanni";
            this.LbMotBanni.Size = new System.Drawing.Size(191, 160);
            this.LbMotBanni.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 353);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Uniquement Abonnés ";
            // 
            // CbUniqAbo
            // 
            this.CbUniqAbo.AutoSize = true;
            this.CbUniqAbo.Location = new System.Drawing.Point(139, 352);
            this.CbUniqAbo.Name = "CbUniqAbo";
            this.CbUniqAbo.Size = new System.Drawing.Size(15, 14);
            this.CbUniqAbo.TabIndex = 5;
            this.CbUniqAbo.UseVisualStyleBackColor = true;
            this.CbUniqAbo.CheckedChanged += new System.EventHandler(this.CbUniqAbo_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 386);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Anti Flood";
            // 
            // CbAntiFlood
            // 
            this.CbAntiFlood.AutoSize = true;
            this.CbAntiFlood.Location = new System.Drawing.Point(139, 385);
            this.CbAntiFlood.Name = "CbAntiFlood";
            this.CbAntiFlood.Size = new System.Drawing.Size(15, 14);
            this.CbAntiFlood.TabIndex = 7;
            this.CbAntiFlood.UseVisualStyleBackColor = true;
            this.CbAntiFlood.CheckedChanged += new System.EventHandler(this.CbAntiFlood_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(174, 386);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Nb Message";
            // 
            // TbNbMess
            // 
            this.TbNbMess.Location = new System.Drawing.Point(271, 379);
            this.TbNbMess.Name = "TbNbMess";
            this.TbNbMess.Size = new System.Drawing.Size(100, 20);
            this.TbNbMess.TabIndex = 9;
            this.TbNbMess.Text = "5";
            this.TbNbMess.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TbNbMess.TextChanged += new System.EventHandler(this.CbAntiFlood_CheckedChanged);
            // 
            // Bplus
            // 
            this.Bplus.Location = new System.Drawing.Point(8, 19);
            this.Bplus.Name = "Bplus";
            this.Bplus.Size = new System.Drawing.Size(64, 23);
            this.Bplus.TabIndex = 10;
            this.Bplus.Text = "Ajouter";
            this.Bplus.UseVisualStyleBackColor = true;
            this.Bplus.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Bplus_MouseClick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 10000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // LbMessagePeriodique
            // 
            this.LbMessagePeriodique.FormattingEnabled = true;
            this.LbMessagePeriodique.Location = new System.Drawing.Point(8, 48);
            this.LbMessagePeriodique.Name = "LbMessagePeriodique";
            this.LbMessagePeriodique.Size = new System.Drawing.Size(240, 160);
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
            this.connexionBT.MouseClick += new System.Windows.Forms.MouseEventHandler(this.connexionBT_MouseClick);
            // 
            // btAjouter
            // 
            this.btAjouter.Location = new System.Drawing.Point(6, 20);
            this.btAjouter.Name = "btAjouter";
            this.btAjouter.Size = new System.Drawing.Size(64, 23);
            this.btAjouter.TabIndex = 18;
            this.btAjouter.Text = "Ajouter";
            this.btAjouter.UseVisualStyleBackColor = true;
            this.btAjouter.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btAjouter_MouseClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(76, 20);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(64, 23);
            this.button2.TabIndex = 19;
            this.button2.Text = "Supprimer";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button2_MouseClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(78, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 23);
            this.button1.TabIndex = 20;
            this.button1.Text = "Supprimer";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button1_MouseClick);
            // 
            // pauseBT
            // 
            this.pauseBT.Location = new System.Drawing.Point(463, 23);
            this.pauseBT.Name = "pauseBT";
            this.pauseBT.Size = new System.Drawing.Size(104, 37);
            this.pauseBT.TabIndex = 21;
            this.pauseBT.Text = "Start bot";
            this.pauseBT.UseVisualStyleBackColor = true;
            this.pauseBT.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pauseBT_MouseClick);
            // 
            // lbVote
            // 
            this.lbVote.FormattingEnabled = true;
            this.lbVote.Location = new System.Drawing.Point(6, 48);
            this.lbVote.Name = "lbVote";
            this.lbVote.Size = new System.Drawing.Size(214, 160);
            this.lbVote.TabIndex = 22;
            // 
            // btAjoutVoteValue
            // 
            this.btAjoutVoteValue.Location = new System.Drawing.Point(86, 19);
            this.btAjoutVoteValue.Name = "btAjoutVoteValue";
            this.btAjoutVoteValue.Size = new System.Drawing.Size(64, 23);
            this.btAjoutVoteValue.TabIndex = 24;
            this.btAjoutVoteValue.Text = "Ajouter";
            this.btAjoutVoteValue.UseVisualStyleBackColor = true;
            this.btAjoutVoteValue.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btAjoutVoteValue_MouseClick);
            // 
            // btSupprVoteValue
            // 
            this.btSupprVoteValue.Location = new System.Drawing.Point(156, 20);
            this.btSupprVoteValue.Name = "btSupprVoteValue";
            this.btSupprVoteValue.Size = new System.Drawing.Size(64, 23);
            this.btSupprVoteValue.TabIndex = 25;
            this.btSupprVoteValue.Text = "Supprimer";
            this.btSupprVoteValue.UseVisualStyleBackColor = true;
            this.btSupprVoteValue.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btSupprVoteValue_MouseClick);
            // 
            // btLancerVote
            // 
            this.btLancerVote.Location = new System.Drawing.Point(86, 214);
            this.btLancerVote.Name = "btLancerVote";
            this.btLancerVote.Size = new System.Drawing.Size(64, 23);
            this.btLancerVote.TabIndex = 26;
            this.btLancerVote.Text = "Lancer";
            this.btLancerVote.UseVisualStyleBackColor = true;
            this.btLancerVote.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btLancerVote_MouseClick);
            // 
            // btReinitVote
            // 
            this.btReinitVote.Location = new System.Drawing.Point(6, 214);
            this.btReinitVote.Name = "btReinitVote";
            this.btReinitVote.Size = new System.Drawing.Size(68, 23);
            this.btReinitVote.TabIndex = 27;
            this.btReinitVote.Text = "Réinitialiser";
            this.btReinitVote.UseVisualStyleBackColor = true;
            this.btReinitVote.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btReinitVote_MouseClick);
            // 
            // tbStopVote
            // 
            this.tbStopVote.Location = new System.Drawing.Point(156, 214);
            this.tbStopVote.Name = "tbStopVote";
            this.tbStopVote.Size = new System.Drawing.Size(64, 23);
            this.tbStopVote.TabIndex = 28;
            this.tbStopVote.Text = "Arrêter";
            this.tbStopVote.UseVisualStyleBackColor = true;
            this.tbStopVote.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbStopVote_MouseClick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 215);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(98, 28);
            this.button3.TabIndex = 29;
            this.button3.Text = "Ouvrir";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button3_MouseClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Fichier texte|*.txt";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.btAjouter);
            this.groupBox1.Controls.Add(this.LbMotBanni);
            this.groupBox1.Location = new System.Drawing.Point(12, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(209, 252);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mot(s) banni(s)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.Bplus);
            this.groupBox2.Controls.Add(this.LbMessagePeriodique);
            this.groupBox2.Location = new System.Drawing.Point(227, 76);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(259, 252);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Message(s) périodique(s)";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.tbStopVote);
            this.groupBox3.Controls.Add(this.btReinitVote);
            this.groupBox3.Controls.Add(this.btLancerVote);
            this.groupBox3.Controls.Add(this.btSupprVoteValue);
            this.groupBox3.Controls.Add(this.btAjoutVoteValue);
            this.groupBox3.Controls.Add(this.lbVote);
            this.groupBox3.Location = new System.Drawing.Point(492, 76);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(227, 252);
            this.groupBox3.TabIndex = 32;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Vote";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Possibilitée(s)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(171, 352);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "label2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 412);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pauseBT);
            this.Controls.Add(this.connexionBT);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.oAuthTB);
            this.Controls.Add(this.uNameTB);
            this.Controls.Add(this.TbNbMess);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CbAntiFlood);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CbUniqAbo);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LbMotBanni;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox CbUniqAbo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox CbAntiFlood;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TbNbMess;
        private System.Windows.Forms.Button Bplus;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ListBox LbMessagePeriodique;
        private System.Windows.Forms.TextBox uNameTB;
        private System.Windows.Forms.TextBox oAuthTB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button connexionBT;
        private System.Windows.Forms.Button btAjouter;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button pauseBT;
        private System.Windows.Forms.ListBox lbVote;
        private System.Windows.Forms.Button btAjoutVoteValue;
        private System.Windows.Forms.Button btSupprVoteValue;
        private System.Windows.Forms.Button btLancerVote;
        private System.Windows.Forms.Button btReinitVote;
        private System.Windows.Forms.Button tbStopVote;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

