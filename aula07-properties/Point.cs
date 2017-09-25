using System;

public class Point {

	private int x, y;

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
    public int Nr{ get; set; }
    public string Name{ get; set; }
    public Student(int nr, string name) {
        Nr = nr;
        Name = name;
    }
    public void Print(){
        Console.WriteLine("{0}: {1}", Nr, Name);
    }
}

public class Classroom {
    Student [] stds;
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


class App {
    static void Main() {
        Point p = new Point(11,9);
        p.X = 7;
        int n = p.Y;
        Console.WriteLine("Module = " + p.Module);
        
        Student st = new Student(76521, "Ze MAria");
        st.Nr = 76987;
        
        Classroom cl = new Classroom( // O compilador vai gerar IL para instanciar 1 array com 2 posições
            new Student(76521, "Ze MAria"),
            new Student(871364, "Maria Salome")
        );
        
        cl[0].Print();
        cl[1].Print();
    }
}