// using System;

struct Point{ int x, y; }  // Tipo Valor => NÃO existe em Java

class Student{ int nr; string name; } // Tipo Referência

class App {
    static void Main() {
        int a = 5;          // Primitivo Tipo Valor
        System.Int32 b = 7; // NÃO primitivo Tipo Valor

        System.String s1 = "Ola"; // NÃO Primitivo de Tipo Referência
        string s2 = "ISEL";       // Primitivo de Tipo Referência

        Student s = new Student(); // instanciado um objecto => IL newobj
        Point p = new Point();     // inicializado um valor => IL initobj
    }
}