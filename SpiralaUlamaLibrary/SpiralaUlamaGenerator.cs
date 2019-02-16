using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Documents;

namespace SpiralaUlamaLibrary
{
    /// <summary>
    /// <para>Klasa służąca do:</para>
    /// <list type="doCzegoSluzyKlasa">
    /// <item>
    /// <term>GenerujPunkty</term>
    /// <para><description>1.) Generowania punktów na spirali ulama. Metoda:</description></para>
    /// </item>
    /// <item>
    /// <para>2.) Obliczaniu i zwracaniu gdzie maja leżeć na obiekcie o zadanych maxymalnych parametrach x,y. Metoda: <see cref="SpiralaUlamaGenerator.GetValues"/> </para>
    /// </item>
    /// </list>
    /// <para>Uzywa osobnej klasy <c>PrimeNumbersGenerator</c> do wyliczenia listy punktow pierwszych,
    /// sluzacych za podstawe w Spirali Ulama.</para>
    /// <para>Klasa: <see cref="PrimeNumbersGenerator"/> Metoda: 
    /// <see cref="PrimeNumbersGenerator.GetValuesTo"/></para>
    /// </summary>
    /// <remarks>
    /// <para>Dla zadanych maxymalnych rozmiarow planszy do wypisywania,
    /// dokladnie oblicza polozenie generowanych punktow (dla zadanej rozmiarami planszy).</para>
    /// </remarks>
    public class SpiralaUlamaGenerator
    {
        /// <summary>
        /// do tej <c>List</c> generujemy punkty spirali ulama
        /// </summary>
        private List<Tuple<int,int>> punkty;
        /// <summary>
        /// aktualne polozonie przy generowanie spirali
        /// </summary>
        private int x, y;
        /// <summary>
        /// aktualnie minimalne wartosci x,y w aktualnie powiekszajacej się(aktualnie generowanej) spirali ulama ( w praktyce przyjmuje ona dosc kwadratowa postac)
        /// </summary>
        private int minx, miny;
        /// <summary>
        ///  aktualnie maxymalne wartosci x,y w aktualnie powiekszajacej się(aktualnie generowanej) spirali ulama ( w praktyce przyjmuje ona dosc kwadratowa postac)
        /// </summary>
        private int maxx, maxy;
        /// <summary>
        /// aktualna wartosc liczbowa przy generacji ulama 1..value..maximumValue
        /// </summary>
        private int value;
        /// <summary>
        /// wartosc do ktorej darzymy; ekstremum naszej funkcji
        /// </summary>
        private int maximumValue;
        /// <summary>
        /// lista liczb pierwszych przez ktore bedziemy przechodzic
        /// </summary>
        private List<int> primeList;
        /// <summary>
        /// wskaznik do wartosci w Liscie liczb pierwszych do kolejnej wartosci liczby pierwszej; minimalna liczba pierwsza ktora jest wieksza od value
        /// </summary>
        private int actualPrime;
        /// <summary>
        /// singleton danej Klasy
        /// </summary>
        static private SpiralaUlamaGenerator singletonSpiralaUlamaGenerator; 


        ///<summary>
        ///Konstruktor prywatny; 
        ///<para>Chcemy aby się odwoływać do klasy tylko przez Singleton</para>
        ///</summary>
        private SpiralaUlamaGenerator() { }
       

        /// <summary>
        /// Chcemy aby była tylko 1 instancja tej klasy, wieć uzywamy wzorca: Singleton
        /// </summary>
        /// <returns>zwraca wskaznik do singletona tej klasy</returns>
        static public SpiralaUlamaGenerator GetSingleton()
        {
            if (singletonSpiralaUlamaGenerator is null)
                singletonSpiralaUlamaGenerator = new SpiralaUlamaGenerator();
            return singletonSpiralaUlamaGenerator;
        }






        /// <summary>
        /// Generuje punkty w spirali 
        /// Posiadajac 4 vektory poruszania sie na spirali (prawo, gora, lewo, dol) wykonujemy kolejne ruchy dopoki nie osiagniemy maxymalnej zadanej wartosci liczbowej
        /// </summary>
        /// <param name="maxHeight">maksymalna wysokosc zrodla, sluzacego za podstawe do rysowania spirali</param>
        /// <param name="maxWidth">maksymalna szerokosc zrodla, sluzacego za podstawe do rysowania spirali</param>
        /// <param name="toReturn">do tego Tupletu zwrocimy dane(troche szybsze dzialanie)</param>
        /// <param name="maximumValueOrNull">jest nie jest nullem, do tej wartosci wygenerujemy spirale</param>
        /// 
        /// <returns>zwraca wskaznik do aktualnego obiektu = siebie = spirali ulama</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when one parameter maxHeight or maxWidth 
        ///  is lesser than 0.</exception>
        public void GetValues(double maxWidth, double maxHeight, out Tuple<List<Tuple<int, int>>, int> toReturn, int? maximumValueOrNull = null)
        {
            if (maxHeight < 0 || maxWidth < 0) // Cos jest nie tak! Rysowanie na planszy o ujemnych wartosciach ???
                throw new System.ArgumentOutOfRangeException();

            //INICJALIZACJA:
            x = y = minx = miny = maxy = maxx = 0;
            value = 1;
            if (maximumValueOrNull is null)
                maximumValue = PrimeNumbersGenerator.GetSingleton().MaximumValue;
            else
                maximumValue = (int)maximumValueOrNull;

            double maximumValueSqrt = Math.Sqrt(maximumValue) + 1;

            actualPrime = 0;
            var isAlreadyOccupied = new bool[(int)maxWidth + 1, (int)maxHeight + 1];
            punkty = new List<Tuple<int, int>>();
            primeList = PrimeNumbersGenerator.GetSingleton().GetValuesTo(maximumValue);



            double srodekx = maxWidth / 2;
            double srodeky = maxHeight / 2;

            double ratioX = maxWidth / Math.Max(1, 2 + maximumValueSqrt);
            double ratioY = maxHeight / Math.Max(1, 2 + maximumValueSqrt);

            int wielkoscKwadracikaDoNarysowania = (int)Math.Min(30, Math.Max(1, Math.Min(ratioX, ratioY)));

            //WYLICZANIE:

            // x++  y       prawo
            // x    y++     gora
            // x--  y       lewo
            // x    y--     dol
            //  vektory poruszania się naszej spirali
            int[,] vectorOfSpiralaMoves = { { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 } };

            while (value <= maximumValue && actualPrime < primeList.Count)
                for (int vectorsTab = 0; vectorsTab < 4; vectorsTab++)
                {

                    //wykonuj petle, dopoki nie wykroczymy poza spirale => musimy zmienic wektor poruszania się
                    while (minx <= x && miny <= y && maxx >= x && maxy >= y && value <= maximumValue && actualPrime < primeList.Count)
                    {
                        x += vectorOfSpiralaMoves[vectorsTab, 0];
                        y += vectorOfSpiralaMoves[vectorsTab, 1];
                        value++;

                 //       Debug.WriteLine("x, y, value" + x + "," + y + "," + value);

                        if (value == primeList[actualPrime])
                        {
                            actualPrime++;
                            int xValueOnScreen = (int)(srodekx + x  * ratioX);
                            int yValueOnScreen = (int)(srodeky - y  * ratioY);

                            if (!isAlreadyOccupied[xValueOnScreen, yValueOnScreen])
                            {
                                isAlreadyOccupied[xValueOnScreen, yValueOnScreen] = true;
                                punkty.Add(new Tuple<int, int>(xValueOnScreen, yValueOnScreen));
                            }
                        }
                    }

                    //jesli vector poruszania sie wyprowadzil nas poza spirale i musimy zmienic kierunek, to musimy tez uaktualnic wartosc, poza ktora nasza spirala wykroczyla
                    if (minx > x) minx = x;
                    if (miny > y) miny = y;
                    if (maxx < x) maxx = x;
                    if (maxy < y) maxy = y;
                }


           // Debug.WriteLine("Spirala wygenerowana");
            toReturn = new Tuple<List<Tuple<int, int>>,int>(punkty, wielkoscKwadracikaDoNarysowania);
        }

    }
}
