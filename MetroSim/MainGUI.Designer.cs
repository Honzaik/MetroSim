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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pLoading = new System.Windows.Forms.PictureBox();
            this.cLinky = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nRychlost = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.nPocetSouprav = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.nKapacita = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pLoading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nRychlost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nPocetSouprav)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nKapacita)).BeginInit();
            this.SuspendLayout();
            // 
            // cZacatek
            // 
            this.cZacatek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cZacatek.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cZacatek.FormattingEnabled = true;
            this.cZacatek.IntegralHeight = false;
            this.cZacatek.Location = new System.Drawing.Point(52, 34);
            this.cZacatek.Name = "cZacatek";
            this.cZacatek.Size = new System.Drawing.Size(282, 31);
            this.cZacatek.TabIndex = 0;
            // 
            // cKonec
            // 
            this.cKonec.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cKonec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cKonec.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cKonec.FormattingEnabled = true;
            this.cKonec.IntegralHeight = false;
            this.cKonec.Location = new System.Drawing.Point(52, 89);
            this.cKonec.Name = "cKonec";
            this.cKonec.Size = new System.Drawing.Size(282, 31);
            this.cKonec.TabIndex = 1;
            // 
            // bStart
            // 
            this.bStart.Font = new System.Drawing.Font("Comic Sans MS", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bStart.Location = new System.Drawing.Point(52, 422);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(282, 90);
            this.bStart.TabIndex = 2;
            this.bStart.Text = "Simuluj";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // lVysledek
            // 
            this.lVysledek.AutoSize = true;
            this.lVysledek.Font = new System.Drawing.Font("Comic Sans MS", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lVysledek.Location = new System.Drawing.Point(119, 556);
            this.lVysledek.Name = "lVysledek";
            this.lVysledek.Size = new System.Drawing.Size(130, 38);
            this.lVysledek.TabIndex = 3;
            this.lVysledek.Text = "Vysledek";
            this.lVysledek.Visible = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(134, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Počáteční stanice";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(134, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Konečná stanice";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(52, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(282, 32);
            this.label3.TabIndex = 6;
            this.label3.Text = "Nastavení";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pLoading
            // 
            this.pLoading.Image = ((System.Drawing.Image)(resources.GetObject("pLoading.Image")));
            this.pLoading.Location = new System.Drawing.Point(137, 529);
            this.pLoading.Name = "pLoading";
            this.pLoading.Size = new System.Drawing.Size(100, 100);
            this.pLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pLoading.TabIndex = 7;
            this.pLoading.TabStop = false;
            this.pLoading.Visible = false;
            // 
            // cLinky
            // 
            this.cLinky.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cLinky.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cLinky.FormattingEnabled = true;
            this.cLinky.Location = new System.Drawing.Point(194, 170);
            this.cLinky.Name = "cLinky";
            this.cLinky.Size = new System.Drawing.Size(55, 31);
            this.cLinky.TabIndex = 8;
            this.cLinky.SelectedIndexChanged += new System.EventHandler(this.cLinky_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(133, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 31);
            this.label4.TabIndex = 9;
            this.label4.Text = "Linka:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(3, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(185, 23);
            this.label5.TabIndex = 10;
            this.label5.Text = "Rychlost soupravy [km/min]:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nRychlost
            // 
            this.nRychlost.DecimalPlaces = 2;
            this.nRychlost.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.nRychlost.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.nRychlost.Location = new System.Drawing.Point(194, 207);
            this.nRychlost.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nRychlost.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.nRychlost.Name = "nRychlost";
            this.nRychlost.Size = new System.Drawing.Size(55, 30);
            this.nRychlost.TabIndex = 11;
            this.nRychlost.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.nRychlost.ValueChanged += new System.EventHandler(this.nastaveni_ValueChanged);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(61, 243);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 32);
            this.label6.TabIndex = 12;
            this.label6.Text = "Počet souprav:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nPocetSouprav
            // 
            this.nPocetSouprav.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.nPocetSouprav.Location = new System.Drawing.Point(195, 245);
            this.nPocetSouprav.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nPocetSouprav.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nPocetSouprav.Name = "nPocetSouprav";
            this.nPocetSouprav.Size = new System.Drawing.Size(54, 30);
            this.nPocetSouprav.TabIndex = 13;
            this.nPocetSouprav.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nPocetSouprav.ValueChanged += new System.EventHandler(this.nastaveni_ValueChanged);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(35, 275);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(153, 32);
            this.label7.TabIndex = 14;
            this.label7.Text = "Kapacita soupravy:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nKapacita
            // 
            this.nKapacita.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.nKapacita.Location = new System.Drawing.Point(194, 281);
            this.nKapacita.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nKapacita.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nKapacita.Name = "nKapacita";
            this.nKapacita.Size = new System.Drawing.Size(54, 30);
            this.nKapacita.TabIndex = 15;
            this.nKapacita.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nKapacita.ValueChanged += new System.EventHandler(this.nastaveni_ValueChanged);
            // 
            // MainGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(394, 641);
            this.Controls.Add(this.nKapacita);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.nPocetSouprav);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.nRychlost);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cLinky);
            this.Controls.Add(this.pLoading);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
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
            ((System.ComponentModel.ISupportInitialize)(this.pLoading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nRychlost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nPocetSouprav)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nKapacita)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cZacatek;
        private System.Windows.Forms.ComboBox cKonec;
        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.Label lVysledek;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pLoading;
        private System.Windows.Forms.ComboBox cLinky;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nRychlost;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nPocetSouprav;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nKapacita;
    }
}

