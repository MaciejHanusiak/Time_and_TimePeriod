# C#-Lab: Implementacja `Time`, `TimePeriod`

* Autor: Krzysztof Molenda
* Wersja: 0.2 (2020-11-08)
* Zagadnienia: projektowanie typu danych, typ strukturalny (`struct`), implementacja interfejsów, przeciążanie operatorów, praca na wielu plikach, testowanie funkcjonalności, properties i auto-properties, zapewnianie niezmienniczości (_immutability_) zmiennych.

_UWAGA_: Zadanie ma charakter ćwiczebny. W bibliotekach C# dostępne są gotowe struktury/klasy realizujące (prawdopodobnie lepiej) przedstawione założenia (`DateTime`, `DateTimeOfset`, `TimeSpan`). Jednak realizacja tego zadania pozwala lepiej zrozumieć ich działanie.

## Problem, cel

Zaprogramuj, korzystając z notatek do wykładów, **wzajemnie powiązane ze sobą** struktury `Time` oraz `TimePeriod`, spełniające następujące wymagania:

### Struktura `Time`

* zmienna typu `Time` opisuje punkt w czasie, w przedziale `00:00:00 … 23:59:59` (weź pod uwagę arytmetykę modulo w godzinach `%24` oraz minutach i sekundach `%60` -- wtedy, kiedy to będzie sensowne i wymagane)
  
    ![Time & TimePeriod](TimePeriod.png)

* wewnętrzną reprezentacją czasu są pola typu `byte`: `Hours`, `Minutes`, `Seconds` – zrealizuj je jako properties,

* zapewnij niezmienniczość (ang. _immutable_) tworzonych zmiennych typu `Time`,
  
* dostarcz różne warianty konstrukcji (między innymi dla trzech parametrów: godzina, minuta, sekunda; dla dwóch parametrów: godzina, minuta i sekundy domyślnie; dla jednego parametru godzina; dla parametru typu `string` o postaci `hh:mm:ss` i ewentualnie innych, wg uznania), zadbaj o poprawną konstrukcję zmiennych, zgłaszając odpowiedni wyjątek w sytuacji błędnych danych,
  
* zaimplementuj standardową reprezentację tekstową czasu (w postaci: `hh:mm:ss`) – przeciąż metodę `ToString()`,
  
* zaimplementuj interfejsy `IEquatable<Time>` oraz `IComparable<Time>` dzięki którym zdefiniujesz naturalny porządek w zbiorze „punktów czasowych”, przeciąż operatory relacyjne (`==`, `!=`, `<`, `<=`, `>`, `>=`),
  
* zapewnij działania arytmetyczne na czasie (modulo 24 godziny) – plus, minus, np. metody `Time Plus(TimePeriod)`, `static Time Plus(Time, TimePeriod)`, przeciążenie operatora `+`)

### Struktura `TimePeriod`

* zmienna typu `TimePeriod` reprezentuje długość odcinka w czasie (odległość między dwoma punktami czasowymi, czas trwania),
  
* przyjmij wewnętrzną realizację czasu trwania jako liczbę sekund (typ `long`),

* zapewnij „zewnętrzną reprezentację” w postaci `h:m:s` – uwaga: wartość `12:25:23` typu `TimePeriod` oznacza upływ czasu równy 12 godzin, 25 minut i 23 sekund, zaś ten sam zapis w rozumieniu Time oznacza punkt na osi czasu: godzinę dwunastą dwadzieścia pięć i 23 sekundy. _Uwaga_: zmienna typu `TimePeriod` o wartości `29:58:12` ma sens, tzn. oznacza ona odcinek czasowy o długości 29 godzin, 58 minut i 23 sekund, zaś w typie `Time` nie ma sensu,

* zapewnij niezmienniczość (ang. _immutable_) tworzonych zmiennych typu `TimePeriod`,

* dostarcz różne warianty konstrukcji (m. in. dla trzech parametrów: liczba godzin, liczba minut, liczba sekund; dla dwóch parametrów: liczba godzin, liczba minut; dla jednego parametru liczba sekund; dla dwóch parametrów typu `Time` obliczając różnicę punktów czasowych, dla parametru typu `string` o postaci `h:mm:ss` i ewentualnie innych, wg uznania), zadbaj o poprawną konstrukcję zmiennych, zgłaszając odpowiedni wyjątek w sytuacji błędnych danych,

* zaimplementuj standardową reprezentację tekstową upływu czasu (w postaci: `h:mm:ss`) – przeciąż metodę `ToString()`, dopuszczalny jest zapis 129:58:12,

* zaimplementuj interfejsy `IEquatable<TimePeriod>` oraz `IComparable<TimePeriod>`, przeciąż operatory relacyjne (`==`, `!=`, `<`, `<=`, `>`, `>=`),
  
* zapewnij działania arytmetyczne na odcinkach czasowych  – plus, minus, np. metody `TimePeriod Plus(TimePeriod)`, `static TimePeriod Plus(TimePeriod, TimePeriod)`, przeciążenie operatora `+`), oraz inne - wg uznania.

Przetestuj poprawność zaprojektowanych struktur tworząc odpowiednie testy jednostkowe.

## Zadania dodatkowe

1. Wykorzystaj zaprogramowane struktury do realizacji programu typu „zegar” i „stoper”. Mile widziana aplikacja desktopowa (WinForm, WPF, UWP).

2. Zmodyfikuj zaprogramowane struktury tak, aby możliwe było operowanie na milisekundach, czyli tysięcznych częściach sekundy. Taka realizacja może przydać się przy tworzeniu programu typu stoper, gdzie pożądana jest większa dokładność niż na poziomie 1 sekundy.
