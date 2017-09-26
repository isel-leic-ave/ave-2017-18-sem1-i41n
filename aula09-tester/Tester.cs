using System;
using System.Reflection;
using System.Collections.Generic;

public class TestAttribute : Attribute {
}

class TesterAssertException : Exception {
    public readonly object expected, actual;
    public TesterAssertException(object expected, object actual) {    
        this.expected = expected;
        this.actual = actual;
    }
}

public class Tester {
    public static void Assert(int expected, int actual) {
        if(expected != actual)
            throw new TesterAssertException(expected, actual);
    }
    public static void Assert(bool expected, bool actual) {
        if(expected != actual)
            throw new TesterAssertException(expected, actual);
    }

    public static void RunTests(Type suite) {
        foreach(MethodInfo m in suite.GetMethods(BindingFlags.Instance | BindingFlags.Public)) {
            if(m.IsDefined(typeof(TestAttribute), true)) {
                object target = Activator.CreateInstance(suite);
                if(m.GetParameters().Length != 0) continue;
                try{
                    m.Invoke(target, new object[0]);
                } catch(TesterAssertException e) {
                    Console.WriteLine(m.Name + " FAILED: expected " + e.expected + " but actual is " + e.actual);
                    continue;
                }
                Console.WriteLine(m.Name + "SUCCEED");
            }
        }
    }
}