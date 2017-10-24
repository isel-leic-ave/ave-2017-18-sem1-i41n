using System;

class Tr{}
struct Tv{}

class Triangle {
    Point p1, p2, p3;
    public Triangle(int x1, int y1, int x2, int y2, int x3, int y3) {
        // ??????  In-Place ????
        // 
        // ldarg.0
        // ldflda Triangle::p1  // endereço do campo p1
        // ldarg.1
        // ldarg.2
        // call Point::.ctor ???? newobj
        p1 = new Point(x1, y1); 
        p2 = new Point(x2, y2); 
        p3 = new Point(x3, y3); 
    }
}
struct Point {
    public int X {get; set; }
    public int Y {get; set; }
    public double Module {
        get { return Math.Sqrt(X*X + Y*Y); }
    }
    public Point(int x, int y) {
        X = x; Y = y;
    }
    /* ERRO de compilação => todos os campos têm que ser iniciados.
    public Point(int x) {
        X = x;
    }
    */
}

class Student {
    int nr;
    string name; 
    public Student(int nr,  string name) {
        this.nr = nr;
        this.name = name;
    }
}
static class App {
    static void Main() {
        Tr o = new Tr(); // => newobj + stloc
        Tv v = new Tv(); // => ldloca + initobj
        Point p = new Point(7, 11); // => ldloca + ldc.i4.7 + ldc.i4 11 + call Point::.ctor
        Point p2 = new Point(); // => ldloca + initobj
        Student s = new Student(1725, "Ze Manel");
        //Student s2 = new Student(); => ERRO de compilação pq Studente nao tem Ctor sem parametros
    }
}