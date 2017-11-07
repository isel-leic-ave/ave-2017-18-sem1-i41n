using System;

class App{
   static readonly Student st = new Student(76521, "Ze MAria");
   
   static void Main() {
        /*
        Point p = new Point(11,9);
        Classroom cl = new Classroom( 
            new Student(76521, "Ze MAria"),
            new Student(871364, "Maria Salome")
        );
        */
        Console.WriteLine(Logger2.Logger.Fields(st));
        Console.WriteLine(Logger4.Logger.Fields(st));
        
        NBench.Bench(App.BenchLogger2, "Logger2-opt");
        NBench.Bench(App.BenchLogger4, "Logger4-emit");
    }
    
    public static void BenchLogger2() {
        Logger2.Logger.Fields(st);
    }
    
    public static void BenchLogger4() {
        Logger4.Logger.Fields(st);
    }
}



public class Point {

	[Loggable] public int x;
    [Loggable] public int y;

	public Point(int x, int y) {
        this.x = x;
        this.y = y;
    }
    
    public int X {
        get{ return x; }
        set{ x = value; }
    }
    
    public int Y {
        get{ return y; }
        set{ y = value; }
    }
    
    public double Module {
        get {
            int x2 = x*x;
            int y2 = y*y;
            return Math.Sqrt(x2 + y2);
        }
    }
    
	public void print() {
        Console.WriteLine(
            String.Format("Print V3 super (x = {0}, y = {1})\n", x, y)
        );
    }
};

public class Student {
    [Loggable] public int nr; 
    [Loggable] public string name;
    public int Nr{ 
        get{return nr;} 
        set{nr = value;}
    }
    public string Name{ 
        get{return name;} 
        set{name = value;}
    }
    public Student(int nr, string name) {
        Nr = nr;
        Name = name;
    }
    public void Print(){
        Console.WriteLine("{0}: {1}", Nr, Name);
    }
}

public class Classroom {
    [Loggable] public Student [] stds;
    public Classroom(params Student[] stds) {
        this.stds = stds;
    }
    public Student this[int i] {
        get { return stds[i]; }
    }
    public Student this[string name] {
        get { return null; /*Lookup student by name*/ }
    }
}

