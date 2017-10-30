using System;

interface IPrinter{
    void Print();
}

class Person {}

class Student : Person, IPrinter {
    public void Print() { Console.WriteLine("I am a Student"); }
}

struct Point{
}

static class App {
    
    static void Main() {
        Student p = new Student();
        Console.WriteLine(p.GetType());
        p.Print();
        
        Point pt = new Point(); 
        pt.GetType(); // TPC:Ver p IL gerado e dizer porquê?
        
    }
}