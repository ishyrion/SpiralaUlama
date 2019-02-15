using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpiralaUlamaLibrary;

namespace SpiralaUlamaUnitTest
{
    [TestClass]
    public class PrimeNumbersGeneratorTests
    {

        [TestMethod]
        [Timeout(2000)]
        [DataRow(-123,0)]
        [DataRow(-4,0)]
        [DataRow(-3,0)]
        [DataRow(-2,0)]
        [DataRow(-1,0)]
        [DataRow(0,0)]
        [DataRow(1,0)]
        [DataRow(2,1)]
        [DataRow(3,2)]
        [DataRow(4,2)]
        [DataRow(5,3)]
        [DataRow(6,3)]
        [DataRow(7,4)]
        [DataRow(8,4)]
        [DataRow(9,4)]
        [DataRow(10,4)]
        [DataRow(11,5)]
        [DataRow(12,5)]
        [DataRow(13,6)]
        [DataRow(30,10)]
        [DataRow(100,25)]
        [DataRow(200,46)]
        [DataRow(500,95)]
        [DataRow(1000,168)]
        public void IsExpectedNumberOfPrimes(int value,int exptected)
        {
            Assert.AreEqual(exptected, PrimeNumbersGenerator.GetSingleton().GetValuesTo(value).Count());
        }




        [TestMethod]
        [Timeout(2000)]
        [DataRow(1,1)]
        [DataRow(100000,123)]
        public void IsNotExpectedNumberOfPrimes(int value,int expected)
        { 
            Assert.IsFalse(expected.Equals(PrimeNumbersGenerator.GetSingleton().GetValuesTo(value).Count()));
            Assert.AreNotEqual(expected, PrimeNumbersGenerator.GetSingleton().GetValuesTo(value).Count());
        }


        [TestMethod]
        [Timeout(2000)]
        [DataRow(-1)]
        [DataRow(1)]
        [DataRow(10)]
        public void IsTypeOfPrimeNumbersGenerator(int value)
        {
            Assert.IsInstanceOfType(PrimeNumbersGenerator.GetSingleton(), typeof(PrimeNumbersGenerator));
            Assert.IsInstanceOfType(PrimeNumbersGenerator.GetSingleton().GetValuesTo(value), typeof(List<int>));
            Assert.IsInstanceOfType(PrimeNumbersGenerator.GetSingleton().FindPrimesTo(value), typeof(PrimeNumbersGenerator));

        }



        [TestMethod]
        [Timeout(2000)]
        public void ArePrimesNumbersToStringEqualsToExpectedStringOfPrimes()
        {
            Assert.IsTrue(PrimeNumbersGenerator.GetSingleton().FindPrimesTo(1).ToString().Equals(""));
            Assert.IsTrue(PrimeNumbersGenerator.GetSingleton().FindPrimesTo(2).ToString().Equals("2"));
            Assert.IsTrue(PrimeNumbersGenerator.GetSingleton().FindPrimesTo(3).ToString().Equals("2 3"));
            Assert.IsTrue(PrimeNumbersGenerator.GetSingleton().FindPrimesTo(4).ToString().Equals("2 3"));
            Assert.IsTrue(PrimeNumbersGenerator.GetSingleton().FindPrimesTo(5).ToString().Equals("2 3 5"));
            Assert.IsTrue(PrimeNumbersGenerator.GetSingleton().FindPrimesTo(6).ToString().Equals("2 3 5"));
            Assert.IsTrue(PrimeNumbersGenerator.GetSingleton().FindPrimesTo(7).ToString().Equals("2 3 5 7"));
            Assert.IsTrue(PrimeNumbersGenerator.GetSingleton().FindPrimesTo(8).ToString().Equals("2 3 5 7"));
            Assert.IsTrue(PrimeNumbersGenerator.GetSingleton().FindPrimesTo(9).ToString().Equals("2 3 5 7"));
            Assert.IsTrue(PrimeNumbersGenerator.GetSingleton().FindPrimesTo(10).ToString().Equals("2 3 5 7"));
            Assert.IsTrue(PrimeNumbersGenerator.GetSingleton().FindPrimesTo(11).ToString().Equals("2 3 5 7 11"));
            Assert.IsTrue(PrimeNumbersGenerator.GetSingleton().FindPrimesTo(12).ToString().Equals("2 3 5 7 11"));
            Assert.IsTrue(PrimeNumbersGenerator.GetSingleton().FindPrimesTo(13).ToString().Equals("2 3 5 7 11 13"));
            Assert.IsTrue(PrimeNumbersGenerator.GetSingleton().FindPrimesTo(30).ToString().Equals(
                "2 3 5 7 11 13 17 19 23 29"));
            Assert.IsTrue(PrimeNumbersGenerator.GetSingleton().FindPrimesTo(100).ToString().Equals(
                "2 3 5 7 11 13 17 19 23 29 31 37 41 43 47 53 59 61 67 71 73 79 83 89 97"));
            Assert.IsTrue(PrimeNumbersGenerator.GetSingleton().FindPrimesTo(200).ToString().Equals(
                "2 3 5 7 11 13 17 19 23 29 31 37 41 43 47 53 59 61 67 71 73 79 83 89 97 "+
                "101 103 107 109 113 127 131 137 139 149 151 157 163 167 173 179 181 191 193 197 199"));
            Assert.IsTrue(PrimeNumbersGenerator.GetSingleton().FindPrimesTo(500).ToString().Equals(
                "2 3 5 7 11 13 17 19 23 29 31 37 41 43 47 53 59 61 67 71 73 79 83 89 97 " +
                "101 103 107 109 113 127 131 137 139 149 151 157 163 167 173 179 181 191 193 197 199 "+
                "211 223 227 229 233 239 241 251 257 263 269 271 277 281 283 293 307 311 313 317 331 "+
                "337 347 349 353 359 367 373 379 383 389 397 401 409 419 421 431 433 439 443 449 457 "+
                "461 463 467 479 487 491 499"
                ));
            Assert.IsTrue(PrimeNumbersGenerator.GetSingleton().FindPrimesTo(1000).ToString().Equals(
                "2 3 5 7 11 13 17 19 23 29 31 37 41 43 47 53 59 61 67 71 73 79 83 89 97 " +
                "101 103 107 109 113 127 131 137 139 149 151 157 163 167 173 179 181 191 193 197 199 " +
                "211 223 227 229 233 239 241 251 257 263 269 271 277 281 283 293 307 311 313 317 331 " +
                "337 347 349 353 359 367 373 379 383 389 397 401 409 419 421 431 433 439 443 449 457 " +
                "461 463 467 479 487 491 499 " +
                "503 509 521 523 541 547 557 563 569 571 577 587 593 599 601 607 613 617 619 631 641 "+
                "643 647 653 659 661 673 677 683 691 701 709 719 727 733 739 743 751 757 761 769 773 787 "+
                "797 809 811 821 823 827 829 839 853 857 859 863 877 881 883 887 907 911 919 929 937 941 "+
                "947 953 967 971 977 983 991 997"));

        }
    }

}
