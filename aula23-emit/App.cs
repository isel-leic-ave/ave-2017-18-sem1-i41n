using System; 

class App {
    static void Main() {
        MyDynamicType m = new MyDynamicType(17);
        Console.WriteLine(m.GetNumber());
    }
}