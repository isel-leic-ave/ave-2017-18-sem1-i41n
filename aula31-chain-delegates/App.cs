using System;
using System.Collections.Generic;

public class App {

    public static void Main() {
        // TestCombineAndRemove();
        TestDelegateOperators();
    }
    static void TestDelegateOperators() {
        Action a1 = () => Console.WriteLine("I am a1");
        Action a2 = () => Console.WriteLine("I am a2");
        Action chain = null;
        chain += a1;
        chain += a2;
        chain(); // <=> chain.Invoke();
        
        Console.WriteLine("------------------------------------");
        Action a3 = () => Console.WriteLine("I am a3");
        chain += a3; // Objecto referido por chain é IMUTÁVEL
        chain();
        
        Console.WriteLine("------------------------------------");
        chain -= a2; // Objecto referido por chain é INALTERADO
        chain();
    }
    
    static void TestCombineAndRemove() {
        Action a1 = () => Console.WriteLine("I am a1");
        Action a2 = () => Console.WriteLine("I am a2");
        Action chain = null;
        chain = (Action) Delegate.Combine(chain, a1); // Retorna a1
        Console.WriteLine(Object.ReferenceEquals(chain, a1));
        chain = (Action) Delegate.Combine(chain, a2);
        Console.WriteLine(Object.ReferenceEquals(chain, a1));
        chain(); // <=> chain.Invoke();
        
        Console.WriteLine("------------------------------------");
        Action a3 = () => Console.WriteLine("I am a3");
        chain = (Action) Delegate.Combine(chain, a3); // Objecto referido por chain é IMUTÁVEL
        chain();
        
        Console.WriteLine("------------------------------------");
        chain = (Action) Delegate.Remove(chain, a2); // Objecto referido por chain é INALTERADO
        chain();
    }
}