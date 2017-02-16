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
            this.LbMessagePeriodique = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CbUniqAbo = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CbAntiFlood = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TbNbMess = new System.Windows.Forms.TextBox();
            this.Bplus = new System.Windows.Forms.Button();
            this.LbFrequence = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mot Banni";
            // 
            // LbMotBanni
            // 
            this.LbMotBanni.FormattingEnabled = true;
            this.LbMotBanni.Location = new System.Drawing.Point(12, 55);
            this.LbMotBanni.Name = "LbMotBanni";
            this.LbMotBanni.Size = new System.Drawing.Size(234, 160);
            this.LbMotBanni.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(350, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Message Periodique";
            // 
            // LbMessagePeriodique
            // 
            this.LbMessagePeriodique.FormattingEnabled = true;
            this.LbMessagePeriodique.Location = new System.Drawing.Point(318, 55);
            this.LbMessagePeriodique.Name = "LbMessagePeriodique";
            this.LbMessagePeriodique.Size = new System.Drawing.Size(184, 160);
            this.LbMessagePeriodique.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 230);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Uniquement Abonnées ";
            // 
            // CbUniqAbo
            // 
            this.CbUniqAbo.AutoSize = true;
            this.CbUniqAbo.Location = new System.Drawing.Point(136, 230);
            this.CbUniqAbo.Name = "CbUniqAbo";
            this.CbUniqAbo.Size = new System.Drawing.Size(15, 14);
            this.CbUniqAbo.TabIndex = 5;
            this.CbUniqAbo.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 251);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Anti Flood";
            // 
            // CbAntiFlood
            // 
            this.CbAntiFlood.AutoSize = true;
            this.CbAntiFlood.Location = new System.Drawing.Point(136, 251);
            this.CbAntiFlood.Name = "CbAntiFlood";
            this.CbAntiFlood.Size = new System.Drawing.Size(15, 14);
            this.CbAntiFlood.TabIndex = 7;
            this.CbAntiFlood.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(194, 248);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Nb Message";
            // 
            // TbNbMess
            // 
            this.TbNbMess.Location = new System.Drawing.Point(284, 245);
            this.TbNbMess.Name = "TbNbMess";
            this.TbNbMess.Size = new System.Drawing.Size(100, 20);
            this.TbNbMess.TabIndex = 9;
            // 
            // Bplus
            // 
            this.Bplus.Location = new System.Drawing.Point(532, 9);
            this.Bplus.Name = "Bplus";
            this.Bplus.Size = new System.Drawing.Size(26, 23);
            this.Bplus.TabIndex = 10;
            this.Bplus.Text = "+";
            this.Bplus.UseVisualStyleBackColor = true;
            // 
            // LbFrequence
            // 
            this.LbFrequence.FormattingEnabled = true;
            this.LbFrequence.Location = new System.Drawing.Point(505, 55);
            this.LbFrequence.Name = "LbFrequence";
            this.LbFrequence.Size = new System.Drawing.Size(69, 160);
            this.LbFrequence.TabIndex = 11;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 278);
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
        private System.Windows.Forms.ListBox LbMessagePeriodique;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox CbUniqAbo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox CbAntiFlood;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TbNbMess;
        private System.Windows.Forms.Button Bplus;
        private System.Windows.Forms.ListBox LbFrequence;
        private System.Windows.Forms.Timer timer1;
    }
}

