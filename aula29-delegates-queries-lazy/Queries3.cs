using System;
using System.Collections;
using System.Text;
using System.IO;

/**
 * static <=> abstract + sealed
          <=> não pode ser instanciada + não pode ser extendida
 */ 
static class App {

    static IList Lines(string path)
    {
        string line;
        IList res = new ArrayList();
        
        using(StreamReader file = new StreamReader(path)) // <=> try-with resources do Java >= 7
        {
            while ((line = file.ReadLine()) != null)
            {
                res.Add(line);
            }
        }
        return res;
    }
   
    delegate Object Mapper(Object s);
    delegate bool Predicate(Object s);
   
    static IEnumerable Convert(this IEnumerable src, Mapper func) {
        return new MapperEnumerator(src, func);
    }
       
    static IEnumerable Filter(this IEnumerable src, Predicate test) {
        return new FilterEnumerator(src, test);
    }
    
    class MapperEnumerator: IEnumerable, IEnumerator {
        IEnumerable src; 
        IEnumerator iter;
        Mapper func; 
        
        public MapperEnumerator(IEnumerable src, Mapper f) {
            this.src = src; this.func = f;
        }
        public MapperEnumerator(IEnumerator iter, Mapper f) {
            this.iter = iter; this.func = f;
        }
        public IEnumerator GetEnumerator() {
            return new MapperEnumerator(src.GetEnumerator(), func);
        }
        public bool MoveNext() { return iter.MoveNext(); }
        public Object Current {
            get { return func.Invoke(iter.Current); }
        }
        public void Reset() { iter.Reset(); }
    }
    
    class FilterEnumerator: IEnumerable, IEnumerator {
        IEnumerable src; 
        IEnumerator iter;
        Predicate test; 
        
        public FilterEnumerator(IEnumerable src, Predicate test) {
            this.src = src; this.test = test;
        }
        public FilterEnumerator(IEnumerator iter, Predicate test) {
            this.iter = iter; this.test = test;
        }
        public IEnumerator GetEnumerator() {
            return new FilterEnumerator(src.GetEnumerator(), test);
        }
        public bool MoveNext() { 
            while(iter.MoveNext())
                if(test.Invoke(iter.Current))
                    return true;
            return false; 
        }
        public Object Current {
            get { return iter.Current; }
        }
        public void Reset() { iter.Reset(); }
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
                .Convert(l => { Print("Convert"); return Student.Parse((String) l); })
                .Filter(s => { Print("Filtering..."); return ((Student) s).nr > 38000; } )
                .Filter(s => { Print("Filtering..."); return ((Student) s).name.StartsWith("J"); } )
                .Convert(s => { Print("Convert"); return ((Student) s).name; } );
    
        // foreach(object l in names) Console.WriteLine(l);

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
