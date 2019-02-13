using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpiralaUlamaLibrary
{
    /// <summary>
    /// Klasa służąca do generowania punktów na spirali ulama, ktora uzywa osobnej klasy do wyliczenia punktow pierwszych,
    /// sluzacych za podstawe w Spirali Ulama.
    /// Dla zadanych maxymalnych rozmiarow planszy do wypisywania, dokladnie oblicza polozenie tych punktow na planszy.
    /// </summary>
    public class SpiralaUlamaGenerator
    {
        private List<Tuple<int,int>> punkty; /// punkty na spirali ulama
        private int x, y; /// aktualne polozonie przy generowanie spirali
        private int minx, miny; /// minimalne wartosci x,y w spirali ulama ( w programie, przyjmuje ona dosc kwadratowa postac)
        private int maxx, maxy; /// maxymalne wartosci x,y w spirali ulama
        private int value; /// aktualna wartosc liczbowa przy generacji ulama 1..value..maximumValue
        private readonly int  maximumValue; /// wartosc do ktorej darzymy; ekstremum naszej funkcji
        private List<int> primeList; /// lista liczb pierwszych przez ktore bedziemy przechodzic
        private int actualPrime; /// wskaznik do wartosci w Liscie liczb pierwszych do kolejnej wartosci liczby pierwszej; minimalna liczba pierwsza ktora jest wieksza od value


        ///inicjacja
        public SpiralaUlamaGenerator()
        {
            punkty = new List<Tuple<int, int>>();
            maximumValue = PrimeNumbersGenerator.GetSingleton().MaximumValue;
            PrimeNumbersGenerator.GetSingleton().GetValuesTo(out primeList, maximumValue);

            x = y = minx = miny = maxy = maxx = 0;
            value = 1;
            actualPrime = 0;
        }


        ///sprawdzamy czy aktualna wartosc na spirali (value) jest kolejna liczba pierwsza
        private bool CheckIsPrime(ref int value)
        {
            if (value == primeList[actualPrime])
            {
                actualPrime++;
                return true;
            }
            return false;
        }

        /// majac podany vektor poruszania sie naszej spirali (vx,vy), poruszamy sie, dopoki nie wykroczymy poza aktualne granice spirali lub nie osiagniemy maxymalnej wartosci liczbowej

        
        ///
        private void GoInDirection(int vx, int vy)
        {


            ///wykonuj petle, dopoki nie wykroczymy poza spirale => musimy zmienic wektor poruszania się
            while (minx <= x && miny <= y && maxx >= x && maxy >= y)
            {
                x += vx;
                y += vy;
                value++;

                if (value > maximumValue || actualPrime >= primeList.Count)
                    return;


                if (CheckIsPrime(ref value))                   
                    punkty.Add(new Tuple<int, int>(x, y));
    
            }

            ///jesli vector poruszania sie wyprowadzil nas poza spirale i musimy zmienic kierunek, to musimy tez uaktualnic wartosc, poza ktora nasza spirala wykroczyla
            if (minx > x) minx = x;
            if (miny > y) miny = y;
            if (maxx < x) maxx = x;
            if (maxy < y) maxy = y;
        }

        /// <summary>
        /// Generuje punkty w spirali 
        /// Posiadajac 4 vektory poruszania sie na spirali (prawo, gora, lewo, dol) wykonujemy kolejne ruchy dopoki nie osiagniemy maxymalnej zadanej wartosci liczbowej
        /// </summary>
        /// <returns>zwraca wskaznik do aktualnego obiektu = siebie = spirali ulama</returns>
        public SpiralaUlamaGenerator GenerujPunkty()
        {
            /// x++  y       prawo
            /// x    y++     gora
            /// x--  y       lewo
            /// x    y--     dol
            ///  vektory poruszania się naszej spirali
            int[,] vectorOfSpiralaMoves = { { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 } };


            while (value <= maximumValue)
                for (int vectorsTab = 0; vectorsTab < 4; vectorsTab++)
                    GoInDirection(vectorOfSpiralaMoves[vectorsTab, 0], vectorOfSpiralaMoves[vectorsTab, 1]);

            return this;
        }

        /// <summary>
        /// Generuje liste i gdzie dokladnie ja wyswietlic dla zadanej wysokosci i szerokosci pojemnika 
        /// </summary>
        /// <param name="maxHeight">maksymalna wysokosc zrodla, sluzacego za podstawe do rysowania spirali</param>
        /// <param name="maxWidth">maksymalna szerokosc zrodla, sluzacego za podstawe do rysowania spirali</param>
        /// <param name="toReturn">lista do której zapisujemy wartosci punktow do rysowania spirali; aby dzialala szybciej referencja</param>
        /// <returns>zwraca wskaznik do aktualnego obiektu = siebie = spirali ulama</returns>
        public SpiralaUlamaGenerator GetValues(double maxHeight, double maxWidth, out Tuple<List<Tuple<int, int>>, double> toReturn)
        {
            if(maxHeight < 22)
                maxHeight = 22;
            if (maxWidth < 22)
                maxWidth = 22;

            maxHeight -= 20;
            maxWidth  -= 20;
            double ratioX = maxWidth / ((double)(2+maxx - minx));
            double ratioY = maxHeight / ((double)(2+maxy - miny));

            double srodekx = maxWidth / 2;
            double srodeky = maxHeight / 2;

            var isAlready = new bool[(int)maxHeight, (int)maxWidth];
   
            var listToReturn = new List<Tuple<int, int>>();
            foreach (var actualnyPunkt in punkty)
                {
                var doDodania = new Tuple<int, int>(
                    (int)( srodekx + (double)actualnyPunkt.Item1 * ratioX), 
                    (int)( srodeky + (double)actualnyPunkt.Item2 * ratioY));
                if( !isAlready[ doDodania.Item2, doDodania.Item1 ])
                    listToReturn.Add(doDodania);
                }

              
            toReturn = new Tuple<List<Tuple<int, int>>, double>(listToReturn, Math.Min(ratioX, ratioY));
            return this;
        }

    }
}
