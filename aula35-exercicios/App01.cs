using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

class App{
    static void Main() {
        Validator<Student> validator = ValidatorBuilder
                    .Build<Student>()
                    // .AddValidation("Age", new Above18())
                    .AddValidation("Name", new NotNull())
                    .AddValidation<String>("Name", val => val.Length < 10);

        Student s1 = new Student(76135, "Anacleto", 20);
        validator.Validate(s1);
        s1.Print();
        Student s2 = new Student(654354, "Maria Jose Catita", 25);
        validator.Validate(s2);
        s2.Print();
    }
    
    static bool Max10Chars(String val) {
        return val.Length < 10;
    }
}

class Above18 : IValidation { 
    public bool Validate(object obj) {
        return ((int) obj) > 18;
    }
}
class NotNull : IValidation { 
    public bool Validate(object obj) {
        return obj != null;
    }
}

interface IValidation { bool Validate(object t); }

class ValidationException : Exception {}

static class ValidatorBuilder {
    public static Validator<T> Build<T>() {
        return new Validator<T>();
    }
    
    public static IValidation Combine(this IValidation self, IValidation other) {
        return new DoubleValidation(self, other);
    }
    
    class DoubleValidation : IValidation {
        IValidation self;
        IValidation other;
        public DoubleValidation(IValidation self, IValidation other) {
            this.self = self;
            this.other = other;
        }
        public bool Validate(object val) {
            return self.Validate(val) && other.Validate(val);
        }
    }
}

class Validator<T> {
    private Dictionary<PropertyInfo, IValidation> vs = new Dictionary<PropertyInfo, IValidation>();

    public Validator<T> AddValidation(string name, IValidation val) {
        PropertyInfo prop = typeof(T).GetProperty(name);
        if(prop == null) throw new ArgumentException("No property with name " + name);
        IValidation v;
        vs[prop] = vs.TryGetValue(prop, out v)
                    ? v.Combine(val) // <=> new DoubleValidation(self, other);
                    : val;
        return this;
    }
    
    public Validator<T> AddValidation<W>(string name, Func<W, bool> v){
        // Se a propriedade indicada não for do tipo W, então é 
        // lançada a exceção TypeMismatchException. 
        PropertyInfo prop = typeof(T).GetProperty(name);
        if(prop == null) throw new ArgumentException("No property with name " + name);
        if(typeof(W) != prop.PropertyType) throw new TypeMismatchException();
        return  AddValidation(name, new ValidationForFunc<W>(v));
    }
    
    public void Validate(T obj) {
        foreach(var p in vs){
            object propVal = p.Key.GetValue(obj);
            bool valid = p.Value.Validate(propVal);
            if(!valid)
                throw new ValidationException();
        }
    }
    
    class ValidationForFunc<W> : IValidation {
        Func<W, bool> v;
        public ValidationForFunc(Func<W, bool> v) {
            this.v = v;
        }
        public bool Validate(object val) {
            return v((W) val);
        }
    }
}
class TypeMismatchException : Exception {}

public class Student {

    public int Nr{ get; set; }
    public string Name{ get; set; }
    public int Age{ get; set; }
    public Student(int nr, string name, int age) {
        Nr = nr;
        Name = name;
        Age = age;
    }
    public void Print(){
        Console.WriteLine("{0}: {1} (age {2})", Nr, Name, Age);
    }
}
