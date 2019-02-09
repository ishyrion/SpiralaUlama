using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiralaUlama
{

    class SpiralaUlama
    {
        private List<Tuple<int,int>> punkty; /// punkty na spirali ulama
        private int x, y; /// aktualne polozonie przy generowanie spirali
        private int minx, miny; /// minimalne wartosci x,y w spirali ulama ( w programie, przyjmuje ona dosc kwadratowa postac)
        private int maxx, maxy; /// maxymalne wartosci x,y w spirali ulama
        private int value; /// aktualna wartosc liczbowa przy generacji ulama 1..value..maximumValue
        private int maximumValue; /// wartosc do ktorej darzymy; ekstremum naszej funkcji
        private List<int> primeList; /// lista liczb pierwszych przez ktore bedziemy przechodzic
        private int actualPrime; /// wskaznik do kolejnej wartosci liczby pierwszej; minimalna liczba pierwsza ktora jest wieksza od value


        ///inicjacja
        public SpiralaUlama()
        {
            punkty = new List<Tuple<int,int>>();
            maximumValue = PrimeNumbers.GetSingleton().MaximumValue;
            primeList = PrimeNumbers.GetSingleton().ReturnPrimeNumbersTo(maximumValue);

            x = y = minx = miny = maxy = maxx = 0;
            value = 1;
            actualPrime = 0;
        }


        ///sprawdzamy czy aktualna wartosc na spirali (value) jest kolejna liczba pierwsza
        private bool checkIsPrime(int value)
        {
            if(value == primeList[actualPrime]){
                actualPrime++;
                return true;
            }
            return false;
        }

        /// majac podany vektor poruszania sie naszej spirali (vx,vy), poruszamy sie, dopoki nie wykroczymy poza aktualne granice spirali lub nie osiagniemy maxymalnej wartosci liczbowej
        private void goInDirection(int vx, int vy)
        {
/*            Debug.WriteLine("Count = " + primeList.Count + "; Count() = " + primeList.Count());
            foreach (var prime in primeList)
                Debug.Write(prime + " ");
            Debug.WriteLine("");
*/

            ///wykonuj petle, dopoki nie wykroczymy poza spirale => musimy zmienic wektor poruszania się
           while( minx <= x && miny <= y && maxx >= x && maxy >= y )
            {
                x += vx;
                y += vy;
                value++;

                if (value > maximumValue || actualPrime >= primeList.Count)
                    return ;

 //               Debug.Write("Sprawdzam liczbe: " + value);

                if (checkIsPrime(value))
    //            {
                    punkty.Add ( new Tuple<int, int>( x,y) );
  /*                 Debug.WriteLine(" TAK");
                }
                else
                {
                    Debug.WriteLine(" Nie");
                }
  */          }

           ///jesli vector poruszania sie wyprowadzil nas poza spirale i musimy zmienic kierunek, to musimy tez uaktualnic wartosc, poza ktora nasza spirala wykroczyla
            if (minx > x) minx = x;
            if (miny > y) miny = y;
            if (maxx < x) maxx = x;
            if (maxy < y) maxy = y;
        }

        /// <summary>
        /// Generuje punkty w spirali 
        /// </summary>
        public void GenerujPunkty()
        {
            /// x++  y       prawo
            /// x    y++     gora
            /// x--  y       lewo
            /// x    y--     dol
            ///  vektory poruszania się naszej spirali
            int[,] vectorOfSpiralaMoves = { { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 } };

      //      Debug.WriteLine("Generowanie Punktow W spirali");

            while (value <= maximumValue)
                for (int vectorsTab = 0; vectorsTab < 4; vectorsTab++)
                    goInDirection(vectorOfSpiralaMoves[vectorsTab, 0], vectorOfSpiralaMoves[vectorsTab, 1]);


     //       Debug.WriteLine("Wygenerowane Punkty W spirali");
    }

        /// <summary>
        /// Generuje liste i gdzie dokladnie ja wyswietlic dla zadanej wysokosci i szerokosci pojemnika 
        /// </summary>
        /// <param name="maxHeight"></param>
        /// <param name="maxWidth"></param>
        /// <returns></returns>
        public Tuple<List<Tuple<int,int>>,double> GetValues(int maxHeight, int maxWidth)
        {
//            int maxymalnyRozmiarPanelu = Math.Min(panelHeight, panelWidth);
//            int maxymalnyRozmiarSpirali = Math.Max(maxy - miny, maxx - minx);
            double ratioX = ((double)maxWidth) / ((double)(maxx - minx));
            double ratioY = ((double)maxHeight) / ((double)(maxy - miny));
            //           Debug.WriteLine("Ratio = " + ratio);
            double srodekx =   maxWidth  / 2;
            double srodeky =   maxHeight  / 2;


            //for(int i=0; i<punkty.Count(); i++){
            var listToReturn = new List<Tuple<int, int>>();
            foreach(var actualnyPunkt in punkty) 
                listToReturn.Add(new Tuple<int,int>((int)(srodekx + actualnyPunkt.Item1 * ratioX), (int)(srodeky + actualnyPunkt.Item2 * ratioY)));

            return new Tuple< List<Tuple<int,int>>,double > (listToReturn, Math.Min(ratioX,ratioY));
        }

    }
}
