using System;
using System.Collections.Generic;

public class App {

    public static void Main() {
        IEnumerator<int> iter = Foo().GetEnumerator();
        iter.MoveNext();
        Console.WriteLine(iter.Current);
        Console.ReadLine();
        iter.MoveNext();
        Console.WriteLine(iter.Current);
        Console.ReadLine();
        iter.MoveNext();
        Console.WriteLine(iter.Current);
        Console.ReadLine();
        iter.MoveNext();
        Console.WriteLine(iter.Current);
    }
    
    static IEnumerable<int> Foo() {
        Console.WriteLine("Foo init...");
        yield return 1;
        Console.WriteLine("Prepare for step 2...");
        yield return 2;
        Console.WriteLine("Prepare for step 3...");
        yield return 3;
    }
}