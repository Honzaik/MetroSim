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
            this.SuspendLayout();
            // 
            // cZacatek
            // 
            this.cZacatek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cZacatek.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cZacatek.FormattingEnabled = true;
            this.cZacatek.Location = new System.Drawing.Point(150, 180);
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
            this.cKonec.Location = new System.Drawing.Point(600, 180);
            this.cKonec.Name = "cKonec";
            this.cKonec.Size = new System.Drawing.Size(250, 31);
            this.cKonec.TabIndex = 1;
            // 
            // MainGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1084, 561);
            this.Controls.Add(this.cKonec);
            this.Controls.Add(this.cZacatek);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainGUI";
            this.Text = "MetroSim";
            this.Load += new System.EventHandler(this.MainGUI_load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cZacatek;
        private System.Windows.Forms.ComboBox cKonec;
    }
}

