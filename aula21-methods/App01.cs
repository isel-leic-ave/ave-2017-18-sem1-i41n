using System;

class Student {
    int nr;
    string name; 
    public Student(int nr,  string name) {
        this.nr = nr;
        this.name = name;
    }
    /*
     * Método Instância NÃO virtual
     */
    public void Print() {
        Console.WriteLine("I am a Student");
    }
}
static class App {
    public static void Foo() {
    }
    static void Main() {
        Student s = new Student(1725, "Ze Manel");
        App.Foo(); // call App::Foo => Despacho Estático
        s.Print(); // ldloc.0 + callvirt Student::Print  => Despacho Estático
        s.ToString(); // ldloc.0 + callvirt Object::ToString  => Despacho Dinâmico
        s = null;
        s.Print(); // NullReferenceException
    }
}