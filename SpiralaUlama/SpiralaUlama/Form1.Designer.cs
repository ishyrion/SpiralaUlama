namespace SpiralaUlama
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.Generuj = new System.Windows.Forms.Button();
            this.textBoxInfoAboutPrimeGen = new System.Windows.Forms.TextBox();
            this.textBoxWpiszZakresLP = new System.Windows.Forms.TextBox();
            this.textBoxOkienkoDoWypisaniaLiczbPierwszych = new System.Windows.Forms.TextBox();
            this.WygenerujSpiraleUlama = new System.Windows.Forms.Button();
            this.WypiszLiczbyPierwsze = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Generuj
            // 
            this.Generuj.Location = new System.Drawing.Point(173, 93);
            this.Generuj.Name = "Generuj";
            this.Generuj.Size = new System.Drawing.Size(75, 23);
            this.Generuj.TabIndex = 0;
            this.Generuj.Text = "Generuj";
            this.Generuj.UseVisualStyleBackColor = true;
            this.Generuj.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxInfoAboutPrimeGen
            // 
            this.textBoxInfoAboutPrimeGen.Location = new System.Drawing.Point(22, 40);
            this.textBoxInfoAboutPrimeGen.Name = "textBoxInfoAboutPrimeGen";
            this.textBoxInfoAboutPrimeGen.ReadOnly = true;
            this.textBoxInfoAboutPrimeGen.Size = new System.Drawing.Size(351, 20);
            this.textBoxInfoAboutPrimeGen.TabIndex = 1;
            this.textBoxInfoAboutPrimeGen.Text = "Wpisz Zakres (Generuj liczby pierwsze (aktualnie) do: 97)";
            this.textBoxInfoAboutPrimeGen.TextChanged += new System.EventHandler(this.textBoxInfoAboutPrimeGen_TextChanged);
            // 
            // textBoxWpiszZakresLP
            // 
            this.textBoxWpiszZakresLP.Location = new System.Drawing.Point(22, 96);
            this.textBoxWpiszZakresLP.Name = "textBoxWpiszZakresLP";
            this.textBoxWpiszZakresLP.Size = new System.Drawing.Size(100, 20);
            this.textBoxWpiszZakresLP.TabIndex = 2;
            this.textBoxWpiszZakresLP.TextChanged += new System.EventHandler(this.textBoxWpiszZakresLP_TextChanged);
            // 
            // textBoxOkienkoDoWypisaniaLiczbPierwszych
            // 
            this.textBoxOkienkoDoWypisaniaLiczbPierwszych.Location = new System.Drawing.Point(20, 200);
            this.textBoxOkienkoDoWypisaniaLiczbPierwszych.Multiline = true;
            this.textBoxOkienkoDoWypisaniaLiczbPierwszych.Name = "textBoxOkienkoDoWypisaniaLiczbPierwszych";
            this.textBoxOkienkoDoWypisaniaLiczbPierwszych.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxOkienkoDoWypisaniaLiczbPierwszych.Size = new System.Drawing.Size(760, 360);
            this.textBoxOkienkoDoWypisaniaLiczbPierwszych.TabIndex = 3;
            this.textBoxOkienkoDoWypisaniaLiczbPierwszych.Visible = false;
            this.textBoxOkienkoDoWypisaniaLiczbPierwszych.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // WygenerujSpiraleUlama
            // 
            this.WygenerujSpiraleUlama.Location = new System.Drawing.Point(173, 138);
            this.WygenerujSpiraleUlama.Name = "WygenerujSpiraleUlama";
            this.WygenerujSpiraleUlama.Size = new System.Drawing.Size(141, 23);
            this.WygenerujSpiraleUlama.TabIndex = 5;
            this.WygenerujSpiraleUlama.Text = "Wygeneruj Spirale Ulama";
            this.WygenerujSpiraleUlama.UseVisualStyleBackColor = true;
            this.WygenerujSpiraleUlama.Click += new System.EventHandler(this.button2_Click);
            // 
            // WypiszLiczbyPierwsze
            // 
            this.WypiszLiczbyPierwsze.Location = new System.Drawing.Point(22, 138);
            this.WypiszLiczbyPierwsze.Name = "WypiszLiczbyPierwsze";
            this.WypiszLiczbyPierwsze.Size = new System.Drawing.Size(136, 23);
            this.WypiszLiczbyPierwsze.TabIndex = 6;
            this.WypiszLiczbyPierwsze.Text = "Wypisz Liczby Pierwsze";
            this.WypiszLiczbyPierwsze.UseVisualStyleBackColor = true;
            this.WypiszLiczbyPierwsze.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.WypiszLiczbyPierwsze);
            this.Controls.Add(this.WygenerujSpiraleUlama);
            this.Controls.Add(this.textBoxOkienkoDoWypisaniaLiczbPierwszych);
            this.Controls.Add(this.textBoxWpiszZakresLP);
            this.Controls.Add(this.textBoxInfoAboutPrimeGen);
            this.Controls.Add(this.Generuj);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Generuj;
        private System.Windows.Forms.TextBox textBoxInfoAboutPrimeGen;
        private System.Windows.Forms.TextBox textBoxWpiszZakresLP;
        private System.Windows.Forms.TextBox textBoxOkienkoDoWypisaniaLiczbPierwszych;
        private System.Windows.Forms.Button WygenerujSpiraleUlama;
        private System.Windows.Forms.Button WypiszLiczbyPierwsze;
    }
}

