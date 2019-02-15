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
            buttonNarysujSpiraleUlama.Visibility = System.Windows.Visibility.Hidden;
            gridPanelDoRysowaniaSpirali.Visibility = System.Windows.Visibility.Visible;
        }

        /// <summary>
        /// dopasuj wielkosc okienka do wyswietlania spirali
        /// </summary>
        private async void DopasujWielkosciOkienkaDoRysowaniaSpirali()
        {
            // gridPanelDoRysowaniaSpirali.Width = gridPodstawkaUI.ActualWidth;

            // if(!gridPanelDoRysowaniaSpirali.Equals(gridPodstawkaUI))
            //     gridPanelDoRysowaniaSpirali.Height = gridPodstawkaUI.ActualHeight - 161;
            if (gridPanelDoRysowaniaSpirali.IsVisible)
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



        /// <summary>
        /// Nacisnij przycisk -> zmienia się zakres liczb pierwszych, do podanej wartosci w wyznaczonym okienku
        /// </summary>
        private async void ButtonZmienZakres_Click(object sender, RoutedEventArgs e)
        {
            string s = textBoxTuWpiszLiczbe.Text;
            if (Int32.TryParse(s, out int result))
            {
                if (result >= 1000000)
                    if (System.Windows.MessageBox.Show(
                    "Przy zadanej liczbie (zakresie) program moze wolno dzialac.\n"+
                    "Nie jest zalecane wywolywac liczby powyzej 3 milionow",
                    "Czy napewno chcesz kontynuowac?",
                    System.Windows.MessageBoxButton.YesNo) == MessageBoxResult.No)
                        return;

                await Task.Run(() => PrimeNumbersGenerator.GetSingleton().FindPrimesTo(result));
                await textBoxInfoOZakresie.Dispatcher.InvokeAsync(
                    () => textBoxInfoOZakresie.Text = $"Zakres Liczb do: {result}.");


                if (textBoxDoWyswietlaniaLiczbPierwszych.IsVisible)
                    await textBoxDoWyswietlaniaLiczbPierwszych.Dispatcher.InvokeAsync(() => WyswietlLiczbyPierwsze());
                if (gridPanelDoRysowaniaSpirali.IsVisible)
                    await gridPanelDoRysowaniaSpirali.Dispatcher.InvokeAsync( () => NarysujSpiraleUlama());           
            }
            else
            {
                MessageBoxResult messageBoxWrongInput = System.Windows.MessageBox.Show(
                    "Upewnij się, że podałeś dane w poprawnej formie: \n"+
                    "tzn: liczba całkowita z zakresu (-2147483648, 2147483647)\n"+
                    "jednak aby istniały liczby pierwsze, zalecane są liczby >= 2",
                    "Wprowadzono niepoprawne dane",
                    System.Windows.MessageBoxButton.OK);
            }
        }


        /// <summary>
        /// pomocnicza Klassa służąca do przechowywania zapasowych danych, aby dalo się cofnać full screen: F11+F11 -> powrot do stanu pierwotnego
        /// </summary>
        static class RememberParametrsBeforeF11{
            public static WindowState state;
            public static WindowStyle style;
            public static ResizeMode resizeMode;
            public static double width;
            public static double height;
            public static double top;
            public static double left;

            /// <summary>
            /// zapamietuje parametry podanego okienka
            /// </summary>
            /// <param name="window">okienko do zapisania wartosci</param>
            public static void Save(Window window)
            {
                state = window.WindowState;
                style = window.WindowStyle;
                resizeMode = window.ResizeMode;

                width = window.Width;
                height = window.Height;
                top = window.Top;
                left = window.Left;
            }

            /// <summary>
            /// odczytuje zapisane parametry do podanego okienka
            /// </summary>
            /// <param name="window">okienko do przywrocenia zapisanych parametrow</param>
            public static void Load(Window window)
            {
                window.WindowState = state;
                window.WindowStyle = style;
                window.ResizeMode = resizeMode;

                window.Width = width;
                window.Height = height;
                window.Top = top;
                window.Left = left;
            }
        }

        /// <summary>
        /// idz do full screen mode - F11
        /// </summary>
        private void WindowGoFullScreenMode()
        {
            RememberParametrsBeforeF11.Save(windowMainUI);

            windowMainUI.Topmost = true;
            windowMainUI.Width = SystemParameters.PrimaryScreenWidth;
            windowMainUI.Height = SystemParameters.PrimaryScreenHeight;
            windowMainUI.Top = 0;
            windowMainUI.Left = 0;


            windowMainUI.WindowState = System.Windows.WindowState.Normal;
            windowMainUI.WindowStyle = WindowStyle.None;
            windowMainUI.ResizeMode = System.Windows.ResizeMode.NoResize;
        }

        /// <summary>
        /// idz do NOT full screen mode - F11
        /// </summary>
        private void WindowGoNormalScreenMode()
        {
            windowMainUI.Topmost = false;
            RememberParametrsBeforeF11.Load(windowMainUI);
        }

        /// <summary>
        /// Co się stanie jak nacisniemy F11: full screen / powrot do poprzedniego stanu
        /// </summary>
        private async void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F11)
                if (!windowMainUI.Height.Equals(SystemParameters.PrimaryScreenHeight))
                    await windowMainUI.Dispatcher.InvokeAsync(() => WindowGoFullScreenMode());
                else
                    await windowMainUI.Dispatcher.InvokeAsync(() => WindowGoNormalScreenMode());
        }





        /// <summary>
        /// w odpowiednim przeznaczonym do tego okienku, wyswietl liczby pierwsze
        /// </summary>
        private async void WyswietlLiczbyPierwsze()
        {
            await Task.Run( ()=> textBoxDoWyswietlaniaLiczbPierwszych.Dispatcher.InvokeAsync(
                () => textBoxDoWyswietlaniaLiczbPierwszych.Text = PrimeNumbersGenerator.GetSingleton().ToString()));

        }


        /// <summary>
        /// nacisnij przycisk -> wyswietl liczby pierwsze
        /// </summary>
        private async void ButtonWypiszLiczbyPierwsze_Click(object sender, RoutedEventArgs e)
        {
            gridPanelDoRysowaniaSpirali.Visibility = System.Windows.Visibility.Hidden;
            buttonWypiszLiczbyPierwsze.Visibility = System.Windows.Visibility.Hidden;
            buttonOdwrocKolorystykeRysowaniaSpirali.Visibility = System.Windows.Visibility.Hidden;
            buttonNarysujSpiraleNaCalejPowierzchni.Visibility = System.Windows.Visibility.Hidden;
            buttonNarysujSpiraleUlama.Visibility = System.Windows.Visibility.Visible;

            textBoxDoWyswietlaniaLiczbPierwszych.Visibility = System.Windows.Visibility.Visible;

            await Task.Run(() => WyswietlLiczbyPierwsze() );
        }



        bool odwrocKolorystykeSpirali = true;
        /// <summary>
        /// rysuje Spirale Ulama
        /// </summary>
        private async void NarysujSpiraleUlama()
        {
            await gridPanelDoRysowaniaSpirali.Dispatcher.InvokeAsync(() =>
            {
            //Debug.WriteLine("dockPanelDoRysowaniaSpirali.Height, dockPanelDoRysowaniaSpirali.Width" +
             //   gridPanelDoRysowaniaSpirali.Height +", "+ gridPanelDoRysowaniaSpirali.Width);
                SpiralaUlamaGenerator.GetSingleton().GetValues(gridPanelDoRysowaniaSpirali.ActualWidth,gridPanelDoRysowaniaSpirali.ActualHeight,
                    out Tuple<List<Tuple<int, int>>, int> toDrawTuple);
                int wielkoscKwadracikaDoNarysowania = toDrawTuple.Item2;// Rectangle(1,1) === pixel // more or less ;)


                Canvas canvas = new Canvas()
                {
                    Width =  gridPanelDoRysowaniaSpirali.ActualWidth,
                    Height =  gridPanelDoRysowaniaSpirali.ActualHeight,
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

        /// <summary>
        /// nacisnij przycisk -> rysuje spirale ulama
        /// </summary>
        private async void ButtonNarysujSpiraleUlama_Click(object sender, RoutedEventArgs e)
        {
            textBoxDoWyswietlaniaLiczbPierwszych.Visibility = System.Windows.Visibility.Hidden;
            gridPanelDoRysowaniaSpirali.Visibility = System.Windows.Visibility.Visible;
            buttonNarysujSpiraleUlama.Visibility = System.Windows.Visibility.Hidden;
            buttonWypiszLiczbyPierwsze.Visibility = System.Windows.Visibility.Visible;
            buttonOdwrocKolorystykeRysowaniaSpirali.Visibility = System.Windows.Visibility.Visible;
            buttonNarysujSpiraleNaCalejPowierzchni.Visibility = System.Windows.Visibility.Visible;

            //gridPanelDoRysowaniaSpirali.Width = windowMainUI.ActualWidth-30;
            //gridPanelDoRysowaniaSpirali.Height =  windowMainUI.ActualHeight - 309;

            await Task.Run(() => NarysujSpiraleUlama());
        }

        /// <summary>
        /// nacisnij przycisk -> odwraca kolorystyke rysowanej spirali (czarne tlo + biale punkty) lub (biale tlo + czarne punkty)
        /// </summary>
        private async void ButtonOdwrocKolorystykeRysowaniaSpirali_Click(object sender, RoutedEventArgs e)
        {
            odwrocKolorystykeSpirali = !odwrocKolorystykeSpirali;
            if(gridPanelDoRysowaniaSpirali.Visibility.Equals(System.Windows.Visibility.Visible))
                await Task.Run(() => NarysujSpiraleUlama());
        }


        /// <summary>
        /// nacisnij przycisk -> rysuje spirale na calej powierzchi naszego okienka
        /// </summary>
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
                buttonFullScreenMode.Visibility = System.Windows.Visibility.Hidden;

                gridPanelDoRysowaniaSpirali = gridPodstawkaUI;
                gridPanelDoRysowaniaSpirali.Visibility = System.Windows.Visibility.Visible;

                await Task.Run(() => NarysujSpiraleUlama());
            }
        }

        /// <summary>
        /// Nacisnij przycisk -> idz do Full Screen mode / poprzedniego mode == F11
        /// </summary>
        private async void ButtonFullScreenMode_Click(object sender, RoutedEventArgs e)
        {
            await windowMainUI.Dispatcher.InvokeAsync(() =>
            {
                if (!windowMainUI.Height.Equals(SystemParameters.PrimaryScreenHeight))
                    WindowGoFullScreenMode();
                else
                    WindowGoNormalScreenMode();
            });
        }

        private async void TextBoxTuWpiszLiczbe_Enter(object sender, TextChangedEventArgs e)
        {
            await textBoxTuWpiszLiczbe.Dispatcher.InvokeAsync(() => textBoxTuWpiszLiczbe.Clear());
        }

        private  async void TextBoxTuWpiszLiczbe_GotFocus(object sender, RoutedEventArgs e)
        {
            await textBoxTuWpiszLiczbe.Dispatcher.InvokeAsync(() => {
                if (!Int32.TryParse(textBoxTuWpiszLiczbe.Text, out int result))
                    textBoxTuWpiszLiczbe.Clear();
                    });
        }

        private async void TextBoxTuWpiszLiczbe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                await buttonZmienZakres.Dispatcher.InvokeAsync(() => ButtonZmienZakres_Click(sender, new RoutedEventArgs()));

        }

    }
}
