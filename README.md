# SpiralaUlama
SpiralaUlama

wiki info :
https://pl.wikipedia.org/wiki/Spirala_Ulama

krótki filmik:
https://pl.khanacademy.org/computing/computer-science/cryptography/comp-number-theory/v/prime-number-theorem-the-density-of-primes

krótki opis:
Wypiszmy Liczby w Spirali, zaczynając od środka ( od 1 ) i poruszając się po spirali będziemy wypisywać kolejne liczby.
Coś na kształt:
<pre><code>
              3    4 3    5 4 3    5 4 3    5 4 3    5 4 3    5 4 3    5 4 3       5 4 3       5 4 3 12
1 -> 1 2 -> 1 2 -> 1 2 ->   1 2 -> 6 1 2 -> 6 1 2 -> 6 1 2 -> 6 1 2 -> 6 1 2    -> 6 1 2 11 -> 6 1 2 11 -> itd.
                                            7        7 8      7 8 9    7 8 9 10    7 8 9 10    7 8 9 10
</code></pre>                                            
                                            
Zróbmy to samo, ale teraz będziemy zaznaczać tylko liczby pierwsze:
<pre><code>
              3      3    5   3    5   3    5   3    5   3    5   3    5   3       5   3       5   3   
  ->   2 ->   2 ->   2 ->     2 ->     2 ->     2 ->     2 ->     2 ->     2    ->     2 11 ->     2 11 -> itd.
                                            7        7        7        7           7           7       
</code></pre>
Wygenerowaną w ten sposób spirale liczb pierwszych nazywamy Spiralą Ulama.
Została ona zaproponowana przez polskiego uczonego Stanisława Ulama w 1963 roku.

Program ma na celu wizualizację podanej spirali:
-wylicza liczby pierwsze,
-wypisuje liczby pierwsze,
-rysuje spirale 
dla zadanego zakresu liczb pierwszych.
