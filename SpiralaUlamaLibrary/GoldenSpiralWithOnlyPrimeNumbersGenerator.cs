using System;
using System.Collections.Generic;
using System.Text;

namespace SpiralaUlamaLibrary
{
    /*
     * (x1,y1)n1->n2(x2,y2) 
     * (0,0)1->(fib(0)->fib(1))->2(1,1): delta_n=1 (x1+x2*n,y1+y2*n) (x1+sin(90*(x2-xact))), y1+(1-cos(90*(y2-yact)))
     * (1,1)2->(fib(1)->fib(2))->3(0,2): delta_n=1 (x1-x2*n,y1+y2*n) (x1-(1-cos(90*(x2-xact))), y1+sin(90*(y2-yact)))
     * (0,2)3->(fib(2)->fib(3))->5(-2,0): delta_n=2 (x1-x2*n,y1-y2*n) (x1-sin(90*(x2-xact))), y1-(1-cos(90*(y2-yact)))
     * (-2,0)5->(fib(3)->fib(4))->8(1,-3): delta_n=3 (x1+x2*n,y1-y2*n) (x1+(1-cos(90*(x2-act))), y+sin(90*(y2-yact)))
     */

        /// <summary>
        /// Ciekawe zagadnienie. Możliwość dalszej eskalacji programu.
        /// </summary>
        /// <remarks>
        /// <para>Ta klasa będzie generowała punkty na złotej spirali:</para>
        /// <para>przy pomocy liczb fibonnaciego, będziemy generować kolejne punkty (tylko liczby pierwsze ) na złotej spirali</para>
        /// </remarks>
    class GoldenSpiralWithOnlyPrimeNumbersGenerator
    {
    }
}
