namespace MetroSim
{
    partial class MainGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainGUI));
            this.cZacatek = new System.Windows.Forms.ComboBox();
            this.cKonec = new System.Windows.Forms.ComboBox();
            this.bStart = new System.Windows.Forms.Button();
            this.lVysledek = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cZacatek
            // 
            this.cZacatek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cZacatek.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cZacatek.FormattingEnabled = true;
            this.cZacatek.IntegralHeight = false;
            this.cZacatek.Location = new System.Drawing.Point(180, 180);
            this.cZacatek.Name = "cZacatek";
            this.cZacatek.Size = new System.Drawing.Size(250, 31);
            this.cZacatek.TabIndex = 0;
            // 
            // cKonec
            // 
            this.cKonec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cKonec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cKonec.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cKonec.FormattingEnabled = true;
            this.cKonec.IntegralHeight = false;
            this.cKonec.Location = new System.Drawing.Point(670, 180);
            this.cKonec.Name = "cKonec";
            this.cKonec.Size = new System.Drawing.Size(250, 31);
            this.cKonec.TabIndex = 1;
            // 
            // bStart
            // 
            this.bStart.Font = new System.Drawing.Font("Comic Sans MS", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bStart.Location = new System.Drawing.Point(430, 300);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(240, 90);
            this.bStart.TabIndex = 2;
            this.bStart.Text = "Simuluj";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // lVysledek
            // 
            this.lVysledek.AutoSize = true;
            this.lVysledek.Location = new System.Drawing.Point(530, 438);
            this.lVysledek.Name = "lVysledek";
            this.lVysledek.Size = new System.Drawing.Size(35, 13);
            this.lVysledek.TabIndex = 3;
            this.lVysledek.Text = "label1";
            this.lVysledek.Visible = false;
            // 
            // MainGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1084, 561);
            this.Controls.Add(this.lVysledek);
            this.Controls.Add(this.bStart);
            this.Controls.Add(this.cKonec);
            this.Controls.Add(this.cZacatek);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainGUI";
            this.Text = "MetroSim";
            this.Load += new System.EventHandler(this.MainGUI_load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cZacatek;
        private System.Windows.Forms.ComboBox cKonec;
        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.Label lVysledek;
    }
}

