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
    public static void Foo() {
    }
    
    public static void Test(Object obj) {
        /*
         * Verificar se o tipo de obj é subclasse de Person
         */
        Console.WriteLine("obj is Person = " + (obj is Person));
        // <=>
        Console.WriteLine("obj is Person = " + (obj.GetType().IsSubclassOf(typeof(Person))));
        // <=>
        Console.WriteLine("obj is Person = " + (typeof(Person).IsAssignableFrom(obj.GetType())));
        
        /*
         * Verificar se obj é instância do tipo exacto Person
         */
        Console.WriteLine("obj.GetType() == typeof(Person) = " + (obj.GetType() == typeof(Person)));
        /*
         * Verificar se o tipo de obj se implementa IPrinter
         */
         Console.WriteLine("obj is IPrinter = " + (obj is IPrinter));
         // <=>
        Console.WriteLine("obj is IPrinter = " + (typeof(IPrinter).IsAssignableFrom(obj.GetType())));         
    }
    
    /**
     * Escreve na console os valores dos campos de instância.
     * MAS se obj implementar IPrinter usamos o Print() directamente.
     */
    static void Log(object obj) {
        if(obj is IPrinter) { // IL: isinst
            /*
             * NUNCA fazer isto qdo o Tipo do obj é CONHECIDO.
             */
            // obj.GetType().GetMethod("Print").Invoke(obj, new object[0]);
        
            IPrinter p = (IPrinter) obj; // IL: castclass 
            p.Print();
        } else {
            // TO DO o mesmo que o Logger
            Console.WriteLine("Reflecting...");
        }
    }
    
    static void Log2(object obj) {
        IPrinter p = obj as IPrinter; // IL: isinst
        if(p != null) {
            p.Print();
        } else {
            // TO DO o mesmo que o Logger
            Console.WriteLine("Reflecting...");
        }
    }
    
    static void Main() {
        // Test(new Student());
        
        Log2(new Person());
        Log2(new Student());
        
    }
}