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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
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
            this.textBoxInfoAboutPrimeGen.Location = new System.Drawing.Point(22, 60);
            this.textBoxInfoAboutPrimeGen.Name = "textBoxInfoAboutPrimeGen";
            this.textBoxInfoAboutPrimeGen.ReadOnly = true;
            this.textBoxInfoAboutPrimeGen.Size = new System.Drawing.Size(351, 20);
            this.textBoxInfoAboutPrimeGen.TabIndex = 1;
            this.textBoxInfoAboutPrimeGen.Text = "Wpisz Zakres (Generuj liczby pierwsze od 2,3,..,97,.. do:<PodanyZakres>)";
            this.textBoxInfoAboutPrimeGen.TextChanged += new System.EventHandler(this.textBoxInfoAboutPrimeGen_TextChanged);
            // 
            // textBoxWpiszZakresLP
            // 
            this.textBoxWpiszZakresLP.Location = new System.Drawing.Point(22, 96);
            this.textBoxWpiszZakresLP.Name = "textBoxWpiszZakresLP";
            this.textBoxWpiszZakresLP.Size = new System.Drawing.Size(100, 20);
            this.textBoxWpiszZakresLP.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(22, 138);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "WypiszLiczbyPierwsze";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(164, 138);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(151, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Wygeneruj Spirale Ulama";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 517);
            this.Controls.Add(this.textBoxWpiszZakresLP);
            this.Controls.Add(this.textBoxInfoAboutPrimeGen);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Generuj);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Generuj;
        private System.Windows.Forms.TextBox textBoxInfoAboutPrimeGen;
        private System.Windows.Forms.TextBox textBoxWpiszZakresLP;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

