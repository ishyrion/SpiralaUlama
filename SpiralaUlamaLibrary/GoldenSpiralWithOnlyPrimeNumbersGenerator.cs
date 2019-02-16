using System;
using System.Collections.Generic;
using System.Text;

namespace SpiralaUlamaLibrary
{ }/*
    
      (x1,y1)n1->n2(x2,y2) 
      (0,0) 1->(fib(0)->fib(1))->2(1,1):  delta_n=1 (x1+x2*n,y1+y2*n) x1=0,y1<0,x2>0,y2=0 delta(x)= x2, delta(y)=-y1 x=delta(x)*sin(90*(x2-xact)/x2),     y=y1+(1-cos(90*(y2-yact)/(-y1)))
      (1,1) 2->(fib(1)->fib(2))->3(0,2):  delta_n=1 (x1-x2*n,y1+y2*n) x1>0,y1=0,x2=0,y2>0 delta(x)= x1, delta(y)= y2 x=x1-(1-cos(90*xact/x1)),    y=y1+sin(90*(y2-yact)/y2))
      (0,2) 3->(fib(2)->fib(3))->5(-2,0): delta_n=2 (x1-x2*n,y1-y2*n) x1=0,y1>0,x2<0,y2=0 delta(x)=-x2, delta(y)= y1 x=-sin(90*(x2-xact)/(-x2)),  y=y1-(1-cos(90*(y2-yact)/y1))
      (-2,0)5->(fib(3)->fib(4))->8(0,-3): delta_n=3 (x1+x2*n,y1-y2*n) x1<0,y1=0,x2=0,y2<0 delta(x)=-x1, delta(y)=-y2 x=x1+(1-cos(90*(x2-act)/(-x1), y=y1+sin(90*(y2-yact)/(-y2)))
     
      (0,0) -> (fib(1),0) -> (0,fib(2)) -> (-fib(3),0) -> (0,-fib(4)) ->
      (fib(n),0) -> (0,fib(n+1)) -> (-fib(n+2),0) -> (0,fib(n+3))
      vf = { {1,0} , {0,1} , {-1,0}, {0,1} }
      for(int actualFibbonaci=0; warunek; actualFibbonaci+=4)
       for(int i=0; i<4; i++)
        x = fibbonaci( actualFibbonaci + i ) * vf[i,0];
        y = fibbonaci( actualFibbonaci + i ) * vf[i,1];
      
      
      



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
*/
