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
        private List<Point> punkty;
        private int x, y;
        private int minx, miny;
        private int maxx, maxy;
        private int value;
        private int maximumValue;
        private List<int> primeList;
        private int actualPrime;

        public SpiralaUlama()
        {
            punkty = new List<Point>();
            maximumValue = PrimeNumbers.GetSingleton().MaximumValue;
            primeList = PrimeNumbers.GetSingleton().ReturnPrimeNumbersTo(maximumValue);

            x = y = minx = miny = maxy = maxx = 0;
            value = 1;
            actualPrime = 0;
        }

        private bool checkIsPrime(int value)
        {
            if(value == primeList[actualPrime]){
                actualPrime++;
                return true;
            }
            return false;
        }


        private List<Point> goInDirection(int vx, int vy)
        {
            var toReturn = new List<Point>();

            while( minx <= x && miny <= y && maxx >= x && maxy >= y )
            {
                x += vx;
                y += vy;
                value++;

                if (value > maximumValue || actualPrime >= primeList.Count)
                    return toReturn;

                if (checkIsPrime(value))
                    toReturn.Add(new Point(x, y, value));
            }

            if (minx > x) minx = x;
            if (miny > y) miny = y;
            if (maxx < x) maxx = x;
            if (maxy < y) maxy = y;

            return toReturn;
        }


        public List<Point> GenerujPunkty()
        {
            // x++  y
            // x    y++
            // x--  y
            // x    y--
            int[,] vectorOfSpiralaMoves = { { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 } };

            Debug.WriteLine("Generowanie Punktow W spirali");

            while (value <= maximumValue)
                for (int vectorsTab = 0; vectorsTab < 4; vectorsTab++)
                    punkty.AddRange(goInDirection(vectorOfSpiralaMoves[vectorsTab, 0], vectorOfSpiralaMoves[vectorsTab, 1]));


            Debug.WriteLine("Wygenerowane Punkty W spirali");
            return punkty;
    }


        public void Narysuj(System.Windows.Forms.Panel panel)
        {
            int maxymalnyRozmiarPanelu = Math.Min(panel.Height, panel.Width);
            int maxymalnyRozmiarSpirali = Math.Max(maxy - miny, maxx - minx);
            double ratio = ((double)maxymalnyRozmiarPanelu) / ((double)maxymalnyRozmiarSpirali);
            double srodekx =   panel.Width  / 2;
            double srodeky =   panel.Height  / 2;


            panel.Refresh();
            Graphics g = panel.CreateGraphics();

            //for(int i=0; i<punkty.Count(); i++){
            foreach(Point actualnyPunkt in punkty) { 
                // narysuj liczbe pierwsza
                double x = srodekx + actualnyPunkt.x * ratio;
                double y = srodeky + actualnyPunkt.y * ratio;

                Debug.WriteLine("Rysowanie " + actualnyPunkt.value + " na pozycji(x,y): (" + x + "," + y + ");");
                g.FillRectangle(Brushes.Black, (int)x, (int)y, 10, 10);  //used 1,1 for a pixel only
            }

            Debug.WriteLine("MaximumValue = "+PrimeNumbers.GetSingleton().MaximumValue +" "+maximumValue);
        }

    }
}
