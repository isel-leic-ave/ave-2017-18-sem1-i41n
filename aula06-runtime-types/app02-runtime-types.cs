using System;

class Student {
    int nr;
    string name;

    public Student(int nr, string name) {
        this.nr = nr;
        this.name = name;
    }
}

class App {

    static void Inspect(object obj) {
        Type t = obj.GetType();
        Console.WriteLine("obj Type = " + t);
    }

    static void SameType(object o1, object o2) {
        // bool res = o1.GetType().Equals(o2.GetType()); // Igualdade de Estados
        // bool res = o1.GetType() == o2.GetType(); // Comparação de Identidade
        bool res = object.ReferenceEquals(o1.GetType(), o2.GetType()); // Comparação de Identidade
        if(res)
            Console.WriteLine("o1 and o2 SAME Type");
        else
            Console.WriteLine("o1 and o2 DIFERENT Type");
    }

    static void IsInstanceOf(object obj, Type t) {
        if(obj.GetType() == t)
            Console.WriteLine("obj is instance of " + t);
        else 
            Console.WriteLine("obj is NOT instance of " + t);

    }

    static void Main() {
        Student s1 = new Student(8233, "Maria");
        Student s2 = new Student(8233, "Jose");
        String str = "super isel";
        Inspect(s1);
        Inspect(s2);
        Inspect(str);
        SameType(s1, s2);
        SameType(s1, str);
        IsInstanceOf(s1, typeof(Student));
        IsInstanceOf(str, typeof(Student));
    }
}