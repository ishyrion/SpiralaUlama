using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralaUlama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = textBoxWpiszZakresLP.Text;
            int result;
            if (Int32.TryParse(s, out result))
            {
                int toWhere = PrimeNumbers.SingletonPrimeNumbers().FindPrimesToNumber(result);
                textBoxInfoAboutPrimeGen.Text = $"Wpisz Zakres (Generuj liczby pierwsze (aktualnie) do: {toWhere})";
            }

        }

        private void textBoxInfoAboutPrimeGen_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxWpiszZakresLP_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var primesList = PrimeNumbers.SingletonPrimeNumbers().ReturnPrimeNumbersTo(PrimeNumbers.SingletonPrimeNumbers().MaximumValue);
            string stringWithPrimes = "";
            foreach (var pierwsza in primesList)
                stringWithPrimes += pierwsza + " ";

            textBoxOkienkoDoWypisaniaLiczbPierwszych.Text = stringWithPrimes;
            textBoxOkienkoDoWypisaniaLiczbPierwszych.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            textBoxOkienkoDoWypisaniaLiczbPierwszych.Width = this.Width-40;
            textBoxOkienkoDoWypisaniaLiczbPierwszych.Height = this.Height - 240;
        }
    }
}
