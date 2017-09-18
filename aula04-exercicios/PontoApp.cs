using System;

class PontoApp {

    public static double module(int x, int y) {
        int x2 = x*x;
        int y2 = y*y;
        return Math.Sqrt(x2 + y2);
    }

    public static void Main(String[] args)
    {
        Ponto p = new Ponto(5, 7);
        p.print();

    }
}