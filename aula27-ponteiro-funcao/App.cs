
delegate void Consumer<T>(T arg);
delegate T Supplyer<T>();
delegate R BiFunction<T1, T2, R>(T1 a, T2 b);

class Funcs {
    public static string Foo() { return null; }
    public static object Bar() { return null; }
    public static void Zoo(object arg) { }
}

public class App {
    static int Add(int a, int b) { return a + b; }
    static int Sub(int a, int b) { return a - b; }

    public static void Main(string [] args) {
        Supplyer<string> sup1 = Funcs.Foo;
        Supplyer<object> sup2 = Funcs.Bar;

        // Incompatible types:
        //    * Expected: () -> object
        //    * Actual:   object -> void
        Supplyer<object> sup3 = Funcs.Zoo; // No overload for 'Zoo' matches delegate 'Supplyer<object>'
        
        Consumer<object> cons1 = Funcs.Zoo;
        Consumer<object> cons2 = System.Console.WriteLine;
        
        cons2.Invoke("Invoke println through function reference");
        
        BiFunction<int, int, int> f = App.Add;
        cons2.Invoke(f.Invoke(5, 3)); // 8
        f = App.Sub;
        cons2.Invoke(f.Invoke(5, 3)); // 2
    }
}