using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using SpiralaUlamaLibrary;

namespace SpiralaUlamaWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            textBoxDoWyswietlaniaLiczbPierwszych.Visibility = System.Windows.Visibility.Hidden;
            ImageDoRysowaniaSpirali.Visibility = System.Windows.Visibility.Hidden;
        }
        
        private void WindowMainUI_SizeChanged(object sender, SizeChangedEventArgs e)
        {
    /*        // calculates incorrect when window is maximized
            textBoxDoWyswietlaniaLiczbPierwszych.Width = windowMainUI.Width - 30;
            textBoxDoWyswietlaniaLiczbPierwszych.Height = windowMainUI.Height - 150;
            ImageDoRysowaniaSpirali.Width = windowMainUI.Width - 30;
            ImageDoRysowaniaSpirali.Height = windowMainUI.Height - 150;
  */      }

        private void WindowMainUI_StateChanged(object sender, EventArgs e)
        {
/*            if ( System.Windows.WindowState.Maximized.Equals(2) )
            {
                textBoxDoWyswietlaniaLiczbPierwszych.Width = 1666;// windowMainUI.Width - 30;
                textBoxDoWyswietlaniaLiczbPierwszych.Height = 1666;// windowMainUI.Height - 150;
                ImageDoRysowaniaSpirali.Width = 1666;// windowMainUI.Width - 30;
                ImageDoRysowaniaSpirali.Height = 1666;// windowMainUI.Height - 150;

            }
            else
            {
                textBoxDoWyswietlaniaLiczbPierwszych.Width = windowMainUI.Width - 30;
                textBoxDoWyswietlaniaLiczbPierwszych.Height = windowMainUI.Height - 150;
                ImageDoRysowaniaSpirali.Width = windowMainUI.Width - 30;
                ImageDoRysowaniaSpirali.Height = windowMainUI.Height - 150;
            }
*/        }

       


        private async void ButtonZmienZakres_Click(object sender, RoutedEventArgs e)
        {
            string s = textBoxTuWpiszLiczbe.Text;
            if (Int32.TryParse(s, out int result))
            {
                await Task.Run(() => PrimeNumbersGenerator.GetSingleton().FindPrimesTo(result));
                await textBoxInfoOZakresie.Dispatcher.InvokeAsync(
                    () => textBoxInfoOZakresie.Text = $"Aktualny zakres Liczb Pierwszych do: {result})");
            }
        }


        private async void WyswietlLiczbyPierwsze()
        {
            await textBoxDoWyswietlaniaLiczbPierwszych.Dispatcher.InvokeAsync(
                () => textBoxDoWyswietlaniaLiczbPierwszych.Text = PrimeNumbersGenerator.GetSingleton().ToString());

        }


        private async void ButtonWypiszLiczbyPierwsze_Click(object sender, RoutedEventArgs e)
        {
            textBoxDoWyswietlaniaLiczbPierwszych.Visibility = System.Windows.Visibility.Visible;
            ImageDoRysowaniaSpirali.Visibility = System.Windows.Visibility.Hidden;
            await Task.Run(() => WyswietlLiczbyPierwsze() );
        }


        private async void NarysujSpiraleUlama()
        {
            SpiralaUlamaGenerator spiralaUlama = new SpiralaUlamaGenerator();
            spiralaUlama.GenerujPunkty();
            Tuple<List<Tuple<int, int>>, double> toDrawList;
            await ImageDoRysowaniaSpirali.Dispatcher.InvokeAsync(
                () => spiralaUlama.GetValues(ImageDoRysowaniaSpirali.Height, ImageDoRysowaniaSpirali.Width, out toDrawList));


            await ImageDoRysowaniaSpirali.Dispatcher.InvokeAsync(()=>
            {
     /*           ImageDoRysowaniaSpirali.UpdateDefaultStyle();
                Graphics g = panel1.CreateGraphics();

                //      Debug.WriteLine("Rysowanie " + actualnyPunkt.value + " na pozycji(x,y): (" + x + "," + y + ");");
                int wielkoscKwadracikaDoNarysowania = Math.Min(10, Math.Max(1, (int)toDrawList.Item2));// Rectangle(1,1) === pixel // more or less ;)

                //      Debug.WriteLine("Rysuje Punkty Spirali: ");
                foreach (var point in toDrawList.Item1)
                    //      {
                    g.FillRectangle(Brushes.Black, (int)point.Item1, (int)point.Item2, wielkoscKwadracikaDoNarysowania, wielkoscKwadracikaDoNarysowania);
                //          Debug.WriteLine("x = " + (int)point.Item1 + "; y = " + (int)point.Item2 + "; wielkoscKwadracikaDoNarysowania = " + wielkoscKwadracikaDoNarysowania);
                //       } */
            });

        }

        /*
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            panel1.Width = textBoxOkienkoDoWypisaniaLiczbPierwszych.Width = this.Width - 40;
            panel1.Height = textBoxOkienkoDoWypisaniaLiczbPierwszych.Height = this.Height - 240;
        }
        */

        private async void ButtonNarysujSpiraleUlama_Click(object sender, RoutedEventArgs e)
        {
            textBoxDoWyswietlaniaLiczbPierwszych.Visibility = System.Windows.Visibility.Hidden;
            ImageDoRysowaniaSpirali.Visibility = System.Windows.Visibility.Visible;
            await Task.Run(() => NarysujSpiraleUlama());
        }
    }
}
