using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiralaUlama
{
    class PrimeNumbers
    {
        private List<int> primeNumbers;
        private static int MaximumSizeOfSieve=1000000;


        public PrimeNumbers(int i = 2) {
            primeNumbers = new List<int>{ 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };
            if(i>=101)
                FindPrimesToNumber(i);
        }





        public List<int> FindPrimesToNumber(int maximumValue)
        {
            // za mały zasięg
            if (maximumValue < 2)
                return new List<int>();

            // już mamy obliczone liczby pierwsze do tego zakresu
            if (maximumValue <= primeNumbers.Last())
            {
                List<int> whatToReturn = new List<int>();
                for (int i = 0; i < primeNumbers.Count() && maximumValue < primeNumbers[i]; i++)
                    whatToReturn.Add(primeNumbers[i]);

                return whatToReturn;
            }

            // inaczej, trzeba wyliczyc nowe liczby pierwsze
            IncreaseListOfPrimesTo(maximumValue);


            List<int> returnedList = new List<int>();
            for (int i = 0; i < primeNumbers.Count() && maximumValue < primeNumbers[i]; i++)
                returnedList.Add(primeNumbers[i]);

            return returnedList;
        }


        /// <summary>
        /// Do obliczeń dużych liczb pierwszych posłużymy się sitem Eratostenesa
        /// 
        /// https://pl.wikipedia.org/wiki/Sito_Eratostenesa
        /// </summary>
        private void IncreaseListOfPrimesTo(int maximumValue)
        {
            int howBigSieve = 2;
            int maximumPrimeInSieve = 0;
            while (maximumPrimeInSieve+1 < primeNumbers.Count() && howBigSieve * primeNumbers[maximumPrimeInSieve+1] <= MaximumSizeOfSieve)
                howBigSieve *= primeNumbers[maximumPrimeInSieve++];

            bool[] sieve = new bool[howBigSieve + 1];
            for (int i = 0; i < sieve.Length; i++)
                sieve[i] = true;
            sieve[0] = false;

            for (int i = 0; i <= maximumPrimeInSieve; i++)
                for (int j = primeNumbers[i]; j < sieve.Length; j += primeNumbers[i])
                    sieve[j] = false;

            int howManyCadidatsInSieve = 0;
            for (int i = 0; i < sieve.Length; i++)
                if (sieve[i])
                    howManyCadidatsInSieve++;

            int[] candidatsFromSieve = new int[howManyCadidatsInSieve];
            for (int i = 0, actualCandidat = 0; i < sieve.Length; i++)
                if (sieve[i])
                    candidatsFromSieve[actualCandidat++] = i;




            for (int i = 0; i <= maximumValue; i += howBigSieve)
                for(int j=0; j<candidatsFromSieve.Length; j++) {
                    int actualCandidateForPrime = i + candidatsFromSieve[j];
                    int actualCandidateForPrimeSqrt = (int)Math.Sqrt((double)actualCandidateForPrime);

                    int range = 0;
                    while (actualCandidateForPrimeSqrt >= primeNumbers[range] && actualCandidateForPrime % primeNumbers[range]!=0)
                        range++;

                    if (actualCandidateForPrime % primeNumbers[range] != 0)
                        primeNumbers.Add(actualCandidateForPrime);
                }

        }





    }
}
