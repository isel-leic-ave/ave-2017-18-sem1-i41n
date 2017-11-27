using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

/**
 * static <=> abstract + sealed
          <=> não pode ser instanciada + não pode ser extendida
 */ 
static class App {

    static IEnumerable<string> Lines(string path)
    {
        string line;
        using(StreamReader file = new StreamReader(path)) // <=> try-with resources do Java >= 7
        {
            while ((line = file.ReadLine()) != null)
            {
                yield return line;
            }
        }
    }
      
    static IEnumerable<R> Convert<T, R>(this IEnumerable<T> src, Func<T, R> func) {
        foreach(T item in src) {
            yield return func.Invoke(item);
        }    
    }
       
    static IEnumerable<T> Filter<T>(this IEnumerable<T> src, Predicate<T> test) {
        foreach(T item in src) {
            if(test.Invoke(item))
                yield return item;
        }
    }
    
    /***********************************************
     * Segundo programador -- Utilizador das Queries
     ************************************************/
     /*
    static bool StudentGreaterThan38000(Student s) {
        return s.nr > 38000;
    }
    
    static bool StudentNameStartWithJ(Student s) {
        return s.name.StartsWith("J");
    }
     */
    static void Print(String s) {
        Console.WriteLine(s);
    }
     
    static void Main()
    {
        IEnumerable names = Lines("i41n.txt")
                .Convert(l => { Print("Convert"); return Student.Parse(l); })
                .Filter(s => { Print("Filtering..."); return s.nr > 38000; } )
                .Filter(s => { Print("Filtering..."); return s.name.StartsWith("J"); } )
                .Convert(s => { Print("Convert"); return s.name; } );
    
        foreach(object l in names) Console.WriteLine(l);

    }
}

public class Student
{
    public readonly int nr;
    public readonly string name;
    public readonly int group;
    public readonly string githubId;

    public Student(int nr, String name, int group, string githubId)
    {
        this.nr = nr;
        this.name = name;
        this.group = group;
        this.githubId = githubId;
    }

    public override String ToString()
    {
        return String.Format("{0} {1} ({2}, {3})", nr, name, group, githubId);
    }
    public void Print()
    {
        Console.WriteLine(this.ToString());
    }
    
    public static Student Parse(string src){
        string [] words = src.Split('|');
        return new Student(
            int.Parse(words[0]),
            words[1],
            int.Parse(words[2]),
            words[3]);
    }
}
