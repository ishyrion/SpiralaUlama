using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiralaUlamaLibrary
{
    /// <summary>
    /// Klasa służąca do generowania liczb pierwszych
    /// </summary>
    public class PrimeNumbersGenerator
    {
        private List<int> primeNumbers = new List<int> { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };///Lista liczb pierwszych
        private int maximumValue = 97;///maxymalna wartosc do której generujemy liczby pierwsze, prywatna
        public int MaximumValue { get => maximumValue; set => maximumValue = value; }///dostępność do maximumValue 
        static private PrimeNumbersGenerator singletonPrimeNumbersGenerator;

        /// <summary>
        /// Potrzebujemy tylko 1 obiektu wyliczającego liczby pierwsze, dlatego zrobilismy go Singletonem.
        /// </summary>
        /// <returns>Singleton == bedziemy zwracać wskaźnik do tego obiektu</returns>
        static public PrimeNumbersGenerator GetSingleton()
        {
            if (singletonPrimeNumbersGenerator is null)
                singletonPrimeNumbersGenerator = new PrimeNumbersGenerator();
            return singletonPrimeNumbersGenerator;
        }

        /// <summary>
        /// konstruktor; prywatny, bo chcemy zeby sie odwolywac do tej Klasy przez Singleton
        /// </summary>
        /// <param name="maximumValue">generuj liczby do podanego parametru</param>
        private PrimeNumbersGenerator(int maximumValue= 2)
        {
            if (this.maximumValue < maximumValue)
                FindPrimesTo( maximumValue);
            singletonPrimeNumbersGenerator = this;
        }


        /// <summary>
        /// return prime list as string
        /// ponieważ tych liczb pierwszych może byc sporo, aby szybciej zsumować liczbyPierwsze.ToString() posluzymy się StringBuilderem
        /// </summary>
        /// <returns></returns>
        override public string ToString()
        {
            StringBuilder builderPrimesToString = new StringBuilder();

            for (var indexOfActualPrime = 0; indexOfActualPrime < primeNumbers.Count() && primeNumbers[indexOfActualPrime] <= maximumValue; indexOfActualPrime++)
                builderPrimesToString.Append(primeNumbers[indexOfActualPrime].ToString()).Append(" ");

            return builderPrimesToString.ToString().Trim();
        }

        /// <summary>
        /// return list of prime numbers to value given in parramentr
        /// </summary>
        /// <param name="maximumValue">maxymalna wartosc do której  zwracamy liczby pierwsze</param>
        /// <returns>zwraca liste liczb pierwszych do podanej wartosci ( domyslnie do 0)</returns>
        public List<int> GetValuesTo(int maximumValue = 0)
        {
            if (maximumValue > primeNumbers.Last())
                IncreaseListOfPrimesTo(maximumValue);

            int index = 0;
            while (index < primeNumbers.Count && maximumValue >= primeNumbers[index])
                index++;

            return primeNumbers.GetRange(0, index);
        }


        /// <summary>
        /// znajdz liczby pierwsze do zakresu podanego w parametrze
        /// </summary>
        /// <param name="maximumValue">maxymalna wartosc do której  wyszukujemy liczby pierwsze</param>
        /// <returns>zwraca wskaznik do aktualnego obiektu = Klasy PrimeNumbersGenerator</returns>
        public PrimeNumbersGenerator FindPrimesTo(int maximumValue)
        {
            // jesli mamy juz obliczone liczby pierwsze do tego zakresu, to nie musimy tego robic
            if (maximumValue > primeNumbers.Last())
                IncreaseListOfPrimesTo(maximumValue);

            this.maximumValue = maximumValue;
            return this;
        }


        /// <summary>
        /// Do obliczeń dużych liczb pierwszych posłużymy się sitem Eratostenesa
        /// 
        /// https://pl.wikipedia.org/wiki/Sito_Eratostenesa
        /// </summary>
        /// <param name="maximumValue">maxymalna wartosc do której  wyszukujemy liczby pierwsze</param>
        /// <returns>zwraca wskaznik do aktualnego obiektu = Klasy PrimeNumbersGenerator</returns>
        private PrimeNumbersGenerator IncreaseListOfPrimesTo(int maximumValue)
        {
            if (maximumValue < primeNumbers.Last())
                return this;

            int MaximumSizeOfSieve = Math.Max(210, (int)Math.Sqrt(maximumValue));
            int sizeOfSieve = 2;
            int indexOfMaximumPrimeInSieve = 0;
            while (indexOfMaximumPrimeInSieve + 1 < primeNumbers.Count && sizeOfSieve * (int)primeNumbers[indexOfMaximumPrimeInSieve + 1] <= MaximumSizeOfSieve)
                sizeOfSieve *= (int)primeNumbers[++indexOfMaximumPrimeInSieve];

            bool[] sieve = new bool[sizeOfSieve + 1];
            for (int i = 0; i < sieve.Length; i++)
                sieve[i] = true;
            sieve[0] = false;

            for (int i = 0; i <= indexOfMaximumPrimeInSieve; i++)
                for (int j=(int)primeNumbers[i], c = j; j < sieve.Length; j += c)
                    sieve[j] = false;

            int howManyCadidatsInSieve = 0;
            for (int i = 0; i < sieve.Length; i++)
                if (sieve[i])
                    howManyCadidatsInSieve++;

            int[] candidatsFromSieve = new int[howManyCadidatsInSieve];
            for (int i = 0, actualCandidat = 0; i < sieve.Length; i++)
                if (sieve[i])
                    candidatsFromSieve[actualCandidat++] = (int)i;


            for (int i = 0, sizeOfSieveUL = (int)sizeOfSieve; i <= maximumValue; i += sizeOfSieveUL)
                for (int j = 0; j < candidatsFromSieve.Length; j++)
                {
                    int actualCandidateForPrime = i + candidatsFromSieve[j];
                    if (actualCandidateForPrime <= primeNumbers.Last())
                        continue;
                    int actualCandidateForPrimeSqrt = (int)Math.Sqrt(actualCandidateForPrime);

                    int range = 0;
                    while (actualCandidateForPrimeSqrt >= primeNumbers[range] && actualCandidateForPrime % primeNumbers[range] != 0)
                        range++;

                    if (actualCandidateForPrime % primeNumbers[range] != 0)
                        primeNumbers.Add(actualCandidateForPrime);

                }
            return this;
        }
    }
}
