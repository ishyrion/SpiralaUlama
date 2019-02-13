using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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

        /// <summary>
        /// Inicjalizacja:
        /// ukryj 2 okienka: 
        /// - do wypisywania liczb pierwszych
        /// - do rysowania spirali 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            textBoxDoWyswietlaniaLiczbPierwszych.Visibility = System.Windows.Visibility.Hidden;
            gridPanelDoRysowaniaSpirali.Visibility = System.Windows.Visibility.Hidden;
        }

        /// <summary>
        /// dopasuj wielkosc okienka do wyswietlania spirali
        /// </summary>
        private async void DopasujWielkosciOkienkaDoRysowaniaSpirali()
        {
            gridPanelDoRysowaniaSpirali.Width = gridPodstawkaUI.ActualWidth;

            if(!gridPanelDoRysowaniaSpirali.Equals(gridPodstawkaUI))
                gridPanelDoRysowaniaSpirali.Height = gridPodstawkaUI.ActualHeight - 161;
            if(gridPanelDoRysowaniaSpirali.IsVisible)
                await Task.Run(() => NarysujSpiraleUlama());

        }


        /// <summary>
        /// gdy zmienia się wielkość głównego okienka: dopasuj wielkośc okieneka do rysowania spirali
        /// </summary>
        private void WindowMainUI_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // calculates incorrect when window is maximized
            DopasujWielkosciOkienkaDoRysowaniaSpirali();   
            
        }

        /// <summary>
        /// gdy zmienia się Stan głównego okienka (maximized,minimized,normal): dopasuj wielkośc okieneka do rysowania spirali
        /// </summary>
        private void WindowMainUI_StateChanged(object sender, EventArgs e)
        {
            DopasujWielkosciOkienkaDoRysowaniaSpirali();   
        }

       

        ///
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
            await Task.Run( ()=> textBoxDoWyswietlaniaLiczbPierwszych.Dispatcher.InvokeAsync(
                () => textBoxDoWyswietlaniaLiczbPierwszych.Text = PrimeNumbersGenerator.GetSingleton().ToString()));

        }


        private async void ButtonWypiszLiczbyPierwsze_Click(object sender, RoutedEventArgs e)
        {
            gridPanelDoRysowaniaSpirali.Visibility = System.Windows.Visibility.Hidden;
            textBoxDoWyswietlaniaLiczbPierwszych.Visibility = System.Windows.Visibility.Visible;

            await Task.Run(() => WyswietlLiczbyPierwsze() );
        }

        bool odwrocKolorystykeSpirali = true;
        private async void NarysujSpiraleUlama()
        {
            await gridPanelDoRysowaniaSpirali.Dispatcher.InvokeAsync(() =>
            {
            //Debug.WriteLine("dockPanelDoRysowaniaSpirali.Height, dockPanelDoRysowaniaSpirali.Width" +
             //   gridPanelDoRysowaniaSpirali.Height +", "+ gridPanelDoRysowaniaSpirali.Width);
                new SpiralaUlamaGenerator().GenerujPunkty().GetValues(gridPanelDoRysowaniaSpirali.ActualHeight,gridPanelDoRysowaniaSpirali.ActualWidth,
                    out Tuple<List<Tuple<int, int>>, double> toDrawTuple);
                int wielkoscKwadracikaDoNarysowania = Math.Min(20, Math.Max(1, (int)(toDrawTuple.Item2)));// Rectangle(1,1) === pixel // more or less ;)


                Canvas canvas = new Canvas()
                {
                    Width =  gridPanelDoRysowaniaSpirali.ActualWidth-20,
                    Height =  gridPanelDoRysowaniaSpirali.ActualHeight-20,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    Background = odwrocKolorystykeSpirali ? Brushes.Black : Brushes.White
                };
                gridPanelDoRysowaniaSpirali.Children.Clear();
                gridPanelDoRysowaniaSpirali.Children.Add(canvas);


                foreach (var point in toDrawTuple.Item1)
                {
                    Shape figuraDoNarysowania;
                    if (wielkoscKwadracikaDoNarysowania == 1)
                        figuraDoNarysowania = new Rectangle();
                    else // wielkoscKwadracikaDoNarysowania > 1
                        figuraDoNarysowania = new Ellipse();

                    figuraDoNarysowania.Width = wielkoscKwadracikaDoNarysowania;
                    figuraDoNarysowania.Height = wielkoscKwadracikaDoNarysowania;
                    figuraDoNarysowania.Fill = odwrocKolorystykeSpirali ? Brushes.White : Brushes.Black;


                    Canvas.SetLeft(figuraDoNarysowania, point.Item1);
                    Canvas.SetTop(figuraDoNarysowania, point.Item2);
                    canvas.Children.Add(figuraDoNarysowania);
                }

 //               Debug.WriteLine("Rysuje prostokat x = " + point.Item1 + "; y= " + point.Item2 + "; wielkosc = " + wielkoscKwadracikaDoNarysowania);
                 

            });
        }


        private async void ButtonNarysujSpiraleUlama_Click(object sender, RoutedEventArgs e)
        {
            textBoxDoWyswietlaniaLiczbPierwszych.Visibility = System.Windows.Visibility.Hidden;
            gridPanelDoRysowaniaSpirali.Visibility = System.Windows.Visibility.Visible;
            gridPanelDoRysowaniaSpirali.Width = windowMainUI.ActualWidth-30;
            gridPanelDoRysowaniaSpirali.Height =  windowMainUI.ActualHeight - 309;

            await Task.Run(() => NarysujSpiraleUlama());
        }

        private async void ButtonOdwrocKolorystykeRysowaniaSpirali_Click(object sender, RoutedEventArgs e)
        {
            odwrocKolorystykeSpirali = !odwrocKolorystykeSpirali;
            if(gridPanelDoRysowaniaSpirali.Visibility.Equals(System.Windows.Visibility.Visible))
                await Task.Run(() => NarysujSpiraleUlama());
        }

        private async void ButtonNarysujSpiraleNaCalejPowierzchni_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(
                "W ten sposób chowasz wszystko.\n Jest to punkt bez powrotu.\n Czy jesteś pewien/pewna", 
                "Rysowanie spirali na całej powierzchni", 
                System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                textBoxDoWyswietlaniaLiczbPierwszych.Visibility = System.Windows.Visibility.Hidden;
                textBoxInfoOZakresie.Visibility = System.Windows.Visibility.Hidden;
                textBoxInfoOMozliwosciZmianyZakresu.Visibility = System.Windows.Visibility.Hidden;
                textBoxTuWpiszLiczbe.Visibility = System.Windows.Visibility.Hidden;
                buttonZmienZakres.Visibility = System.Windows.Visibility.Hidden;
                buttonWypiszLiczbyPierwsze.Visibility = System.Windows.Visibility.Hidden;
                buttonNarysujSpiraleUlama.Visibility = System.Windows.Visibility.Hidden;
                buttonOdwrocKolorystykeRysowaniaSpirali.Visibility = System.Windows.Visibility.Hidden;
                buttonNarysujSpiraleNaCalejPowierzchni.Visibility = System.Windows.Visibility.Hidden;


                gridPanelDoRysowaniaSpirali = gridPodstawkaUI;
                gridPanelDoRysowaniaSpirali.Visibility = System.Windows.Visibility.Visible;

                await Task.Run(() => NarysujSpiraleUlama());
            }
        }
    }
}
