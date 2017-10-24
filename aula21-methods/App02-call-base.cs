using System;

class Person {
    public virtual void Print() {
        Console.WriteLine("I am a Person");
    }
}

class Student : Person{
    int nr;
    string name; 
    public Student(int nr,  string name) {
        this.nr = nr;
        this.name = name;
    }
    public override void Print() {
        Console.WriteLine("I am a Student");
        base.Print(); // Print é Virtual e de Instância => callvirt ??? 
    }
}
static class App {
    static void Main() {
        Student s = new Student(1725, "Ze Manel");
        s.Print(); // callvirt => despacho dinamico
    }
}