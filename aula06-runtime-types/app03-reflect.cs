using System;
using System.Reflection;


class App {

    static void Inspect(String path) {
        Assembly asm = Assembly.LoadFrom(path);
        Type[] klasses  = asm.GetTypes();
        foreach(Type t in klasses){
            Console.Write(t + " -----> ");
            Type[] itfs = t.GetInterfaces();
            foreach(Type i in itfs)
                Console.Write(i + ", ");
            Console.WriteLine();
        }
    }
    
    static Type[] LoadTypes(String path) {
        Assembly asm = Assembly.LoadFrom(path);
        Type[] klasses  = asm.GetTypes();
        return klasses;
    }
    
    static void InspectMethods(Type t) {
        Console.WriteLine(t + ": ");
        MethodInfo[] ms = t.GetMethods();
        foreach(MethodInfo m in ms) {
            Console.Write("   " + m.Name + "(");
            ParameterInfo[] ps = m.GetParameters();
            foreach(ParameterInfo p in ps) {
                // ERRO: p.GetType() => Tipo ParameterInfo
                Console.Write(p.ParameterType + " " + p.Name + ", ");
            }
            Console.WriteLine(")");
        }
    }


    static void Main() {
        Inspect("RestSharp.dll");
        // InspectMethods(LoadTypes("RestSharp.dll")[2]);
    }
}