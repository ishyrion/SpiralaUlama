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

        private async void button1_Click(object sender, EventArgs e)
        {
            string s = textBoxWpiszZakresLP.Text;
            int result;
            if (Int32.TryParse(s, out result))
            {
                int toWhere = await PrimeNumbers.GetSingleton().FindPrimesToNumber(result);
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

        private  async void button3_Click(object sender, EventArgs e)
        {
            textBoxOkienkoDoWypisaniaLiczbPierwszych.Text = await PrimeNumbers.GetSingleton().ToString();
            panel1.Visible = false;
            textBoxOkienkoDoWypisaniaLiczbPierwszych.Visible = true;
        }

        void narysujSpiraleUlama()
        {
            SpiralaUlama spiralaUlama = new SpiralaUlama();
            spiralaUlama.GenerujPunkty();
            var toDrawList = spiralaUlama.GetValues(panel1.Height, panel1.Width);



            panel1.Refresh();
            Graphics g = panel1.CreateGraphics();

            //      Debug.WriteLine("Rysowanie " + actualnyPunkt.value + " na pozycji(x,y): (" + x + "," + y + ");");
            int wielkoscKwadracikaDoNarysowania = Math.Min(10, Math.Max(1, (int)toDrawList.Item2));// Rectangle(1,1) === pixel // more or less ;)

            foreach(var point in toDrawList.Item1)
            g.FillRectangle(Brushes.Black, (int)point.Item1, (int)point.Item2, wielkoscKwadracikaDoNarysowania, wielkoscKwadracikaDoNarysowania);

        }


        private void button2_Click(object sender, EventArgs e)
        {        
            textBoxOkienkoDoWypisaniaLiczbPierwszych.Visible = false;
            panel1.Visible = true;
            narysujSpiraleUlama();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            panel1.Width = textBoxOkienkoDoWypisaniaLiczbPierwszych.Width = this.Width-40;
            panel1.Height = textBoxOkienkoDoWypisaniaLiczbPierwszych.Height = this.Height - 240;     
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
