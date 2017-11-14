using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


class App {

    static List<String> Lines(string path)
    {
        string line;
        List<string> res = new List<string>();
        
        using(StreamReader file = new StreamReader(path)) // <=> try-with resources do Java >= 7
        {
            while ((line = file.ReadLine()) != null)
            {
                res.Add(line);
            }
        }
        return res;
    }
     
    static List<Student> ConvertToStudents(List<String> lines) {
        List<Student> res = new List<Student>();
        foreach(String l in lines) {
            res.Add(Student.Parse(l));
        }
        return res;
    }
    
    static List<String> ConvertToName(List<Student> stds) {
        List<String> res = new List<String>();
        foreach(Student s in stds) {
            res.Add(s.name);
        }
        return res;
    }
    
    static List<Student> FilterWithNumberGreaterThan(List<Student> stds, int nr) {
        List<Student> res = new List<Student>();
        foreach(Student s in stds) {
            if(s.nr > nr)
                res.Add(s);
        }
        return res;
    }
    
    static List<Student> FilterNameStartsWith(List<Student> stds, String prefix) {
        List<Student> res = new List<Student>();
        foreach(Student s in stds) {
            if(s.name.StartsWith(prefix))
                res.Add(s);
        }
        return res;
    }
    
 
    static void Main()
    {
        List<String> names = 
            ConvertToName(
                FilterNameStartsWith(
                    FilterWithNumberGreaterThan(
                        ConvertToStudents(
                            Lines("i41n.txt")),
                        38000), 
                    "J")
                );
    
        foreach(String l in names)
            Console.WriteLine(l);
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
