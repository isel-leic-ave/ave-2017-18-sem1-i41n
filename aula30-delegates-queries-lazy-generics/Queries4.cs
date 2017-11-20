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

    static IList<string> Lines(string path)
    {
        string line;
        IList<String> res = new List<String>();
        
        using(StreamReader file = new StreamReader(path)) // <=> try-with resources do Java >= 7
        {
            while ((line = file.ReadLine()) != null)
            {
                res.Add(line);
            }
        }
        return res;
    }
      
    static IEnumerable<R> Convert<T, R>(this IEnumerable<T> src, Func<T, R> func) {
        return new MapperEnumerator<T, R>(src, func);
    }
       
    static IEnumerable<T> Filter<T>(this IEnumerable<T> src, Predicate<T> test) {
        return new FilterEnumerator<T>(src, test);
    }
    
    class MapperEnumerator<T, R>: IEnumerable<R>, IEnumerator<R> {
        IEnumerable<T> src; 
        IEnumerator<T> iter;
        Func<T, R> func; 
        public MapperEnumerator(IEnumerable<T> src, Func<T, R> f) {
            this.src = src; this.func = f;
        }
        public MapperEnumerator(IEnumerator<T> iter, Func<T, R> f) {
            this.iter = iter; this.func = f;
        }
        public IEnumerator<R> GetEnumerator() {
            return new MapperEnumerator<T, R>(src.GetEnumerator(), func);
        }
        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }     
        public bool MoveNext() { return iter.MoveNext(); }
        
        public R Current {
            get { return func.Invoke(iter.Current); }
        }
        object IEnumerator.Current {
            get { return this.Current; }
        }
        public void Reset() { iter.Reset(); }
        public void Dispose() { iter.Dispose(); }
    }
    
    class FilterEnumerator<T>: IEnumerable<T>, IEnumerator<T> {
        IEnumerable<T> src; 
        IEnumerator<T> iter;
        Predicate<T> test; 
        
        public FilterEnumerator(IEnumerable<T> src, Predicate<T> test) {
            this.src = src; this.test = test;
        }
        public FilterEnumerator(IEnumerator<T> iter, Predicate<T> test) {
            this.iter = iter; this.test = test;
        }
        public IEnumerator<T> GetEnumerator() {
            return new FilterEnumerator<T>(src.GetEnumerator(), test);
        }
        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
        public bool MoveNext() { 
            while(iter.MoveNext())
                if(test.Invoke(iter.Current))
                    return true;
            return false; 
        }
        public T Current {
            get { return iter.Current; }
        }
        Object IEnumerator.Current {
            get { return this.Current; }
        }
        public void Reset() { iter.Reset(); }
        public void Dispose() { iter.Dispose(); }
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
