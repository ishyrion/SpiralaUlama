using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiralaUlama
{
    class PrimeNumbers
    {
        private List<int> primeNumbers = new List<int> { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };
        private int maximumValue = 97;
        public int MaximumValue { get=> maximumValue; set=> maximumValue=value; }
        static private PrimeNumbers singletonPrimeNumbers;
        static public PrimeNumbers GetSingleton()
        {
            if (singletonPrimeNumbers is null)
                singletonPrimeNumbers = new PrimeNumbers();
            return singletonPrimeNumbers;
        }

        public PrimeNumbers(int i = 2) {
            if(i>=101)
                FindPrimesToNumber(i);
            singletonPrimeNumbers = this;
        }


        private async Task<string> connectAllPrimes()
        {
            string primesToString = "";
            for (var indexOfActualPrime = 0; indexOfActualPrime < primeNumbers.Count() && primeNumbers[indexOfActualPrime] <= maximumValue; indexOfActualPrime++)
                primesToString += primeNumbers[indexOfActualPrime] + " ";

            return primesToString;
        }

        //To Test Only
        public  async Task<string> ToString()
        {
            return await connectAllPrimes();
        }


        public List<int> ReturnPrimeNumbersTo(int maximumValue=0)
        {
            if (maximumValue > this.maximumValue)
            {
                IncreaseListOfPrimesTo(maximumValue);
                this.maximumValue = maximumValue;
            }

            int index = 0;
            while( index < primeNumbers.Count && maximumValue >= primeNumbers[index])
                index++;

            return primeNumbers.GetRange(0,index);
        }



        public async Task<int> FindPrimesToNumber(int maximumValue)
        {
            // już mamy obliczone liczby pierwsze do tego zakresu
            if (maximumValue > this.maximumValue)
            {
                IncreaseListOfPrimesTo(maximumValue);
                this.maximumValue = maximumValue;
            }
            return maximumValue;
        }


        /// <summary>
        /// Do obliczeń dużych liczb pierwszych posłużymy się sitem Eratostenesa
        /// 
        /// https://pl.wikipedia.org/wiki/Sito_Eratostenesa
        /// </summary>
        private void IncreaseListOfPrimesTo(int maximumValue)
        {
            int MaximumSizeOfSieve = Math.Max(210,(int)Math.Sqrt(maximumValue));
            int sizeOfSieve = 2;
            int indexOfMaximumPrimeInSieve = 0;
            Debug.Write("numbers in Sieve: 2 ");
            while (indexOfMaximumPrimeInSieve + 1 < primeNumbers.Count() && sizeOfSieve * primeNumbers[indexOfMaximumPrimeInSieve + 1] <= MaximumSizeOfSieve)
            {
                sizeOfSieve *= primeNumbers[++indexOfMaximumPrimeInSieve];
                Debug.Write(primeNumbers[indexOfMaximumPrimeInSieve] + " ");
            }
            Debug.WriteLine("");
            Debug.WriteLine("sizeOfSieve = "+ sizeOfSieve);

            bool[] sieve = new bool[sizeOfSieve + 1];
            for (int i = 0; i < sieve.Length; i++)
                sieve[i] = true;
            sieve[0] = false;

            for (int i = 0; i <= indexOfMaximumPrimeInSieve; i++)
                for (int j = primeNumbers[i]; j < sieve.Length; j += primeNumbers[i])
                    sieve[j] = false;

            int howManyCadidatsInSieve = 0;
            for (int i = 0; i < sieve.Length; i++)
                if (sieve[i])
                    howManyCadidatsInSieve++;

            int[] candidatsFromSieve = new int[howManyCadidatsInSieve];
            Debug.Write("Candidats in Sieve: ");
            for (int i = 0, actualCandidat = 0; i < sieve.Length; i++)
                if (sieve[i])
                {
                    candidatsFromSieve[actualCandidat++] = i;
                    Debug.Write(i + " ");
                }
            Debug.WriteLine("");
            Debug.WriteLine("Numbers of Candidats = " + candidatsFromSieve.Length);
            Debug.WriteLine("Founded Primes: ");

            for (int i = 0; i <= maximumValue; i += sizeOfSieve)
                for(int j=0; j<candidatsFromSieve.Length; j++) {
                    int actualCandidateForPrime = i + candidatsFromSieve[j];
                    if (actualCandidateForPrime <= 1)
                        continue;
                    int actualCandidateForPrimeSqrt = (int)Math.Sqrt(actualCandidateForPrime);

                    int range = 0;
                    while (actualCandidateForPrimeSqrt >= primeNumbers[range] && actualCandidateForPrime % primeNumbers[range]!=0)
                        range++;

                    if (actualCandidateForPrime % primeNumbers[range] != 0)
                    {
                        primeNumbers.Add(actualCandidateForPrime);
                        Debug.Write(actualCandidateForPrime + " ");
                    }
                }

        }





    }
}
