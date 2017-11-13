// interfaces ja definidas java.util.function

interface Consumer<T> { void apply(T item); }
interface Supplyer<T> { T get(); }
interface BiFunction<T1, T2, R> { R apply(T1 a, T2 b); }

class Funcs {
    public static String foo() { return null; }
    public static Object bar() { return null; }
    public static void zoo(Object arg) { }
}

public class App {
    static int add(int a, int b) { return a + b; }
    static int sub(int a, int b) { return a - b; }

    public static void main(String [] args) {
        Supplyer<String> sup1 = Funcs::foo;
        Supplyer<Object> sup2 = Funcs::bar;
        // Supplyer<Object> sup3 = Funcs::zoo; // zoo in class Funcs cannot be applied to given types
        
        Consumer<Object> cons1 = Funcs::zoo;
        Consumer<Object> cons2 = System.out::println;
        
        cons2.apply("Invoke println through function reference");
        
        BiFunction<Integer, Integer, Integer> f = App::add;
        cons2.apply(f.apply(5, 3)); // 8
        f = App::sub;
        cons2.apply(f.apply(5, 3)); // 2
    }
}