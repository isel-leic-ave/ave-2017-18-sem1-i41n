using System;
using System.Reflection;
using System.Collections.Generic;

public class TestAttribute : Attribute {
}

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class InlineAttribute : Attribute
{
    
    public InlineAttribute(object times)
    {
        Times = times;
    }
    public object Times
    {
        get;private set;
    }

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

    static void InvokeTest(object target, MethodInfo m, object[] args) {
        try{
            m.Invoke(target, args);
        } catch(TargetInvocationException e) {
            if(e.InnerException.GetType() != typeof(TesterAssertException))
                throw e;
            TesterAssertException ex = (TesterAssertException) e.InnerException;
            Console.WriteLine(m.Name + " FAILED: expected " + ex.expected + " but actual is " + ex.actual);
            return;
        }
        Console.WriteLine(m.Name + " SUCCEED");
    }
    
    static void InvokeTestWithParameter(object target, MethodInfo m, InlineAttribute[] attrs) {
        ParameterInfo [] ps = m.GetParameters();
        if(ps.Length != 1) {
            Console.WriteLine("BAD unit test configuration for {0}. The inline arguments does not match the method parameters.", m.Name);
            return;
        }
        int i = 0;
        foreach(ParameterInfo p in ps) {
            if(p.ParameterType != attrs[i++].Times.GetType()) {
                Console.WriteLine("BAD unit test configuration for {0}. The inline argument type is NOT COMPATIBLE with method parameter.", m.Name);
                return;
            }
        }
        foreach (InlineAttribute a in attrs) {
            InvokeTest(target, m, new object[] { a.Times });
        }
    }
    public static void RunTests(Type suite) {
        foreach(MethodInfo m in suite.GetMethods(BindingFlags.Instance | BindingFlags.Public)) {
            if(m.IsDefined(typeof(TestAttribute), true)) {
                object target = Activator.CreateInstance(suite);
                if(m.GetParameters().Length == 0) 
                    InvokeTest(target, m, new object[0]);
                else {
                    object[] attribute = m.GetCustomAttributes(typeof(InlineAttribute), false);           
                    InvokeTestWithParameter(target, m, ( InlineAttribute[]) attribute);
                }
            }
        }
    }
}