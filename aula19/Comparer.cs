using System;
using System.Reflection;
using System.Collections.Generic;

public interface IEqualityComparer {  
   bool Equals(object x, object y);  
} 

interface IGetter {
    object GetValue(object target);
}

class GetFromProperty : IGetter {
    PropertyInfo p;
    public GetFromProperty(PropertyInfo p) {
        this.p = p;
    }
    public object GetValue(object target) {
        return p.GetValue(target);
    }
}

class GetFromField : IGetter {
    FieldInfo f;
    public GetFromField(FieldInfo f) {
        this.f = f;
    }
    public object GetValue(object target) {
        return f.GetValue(target);
    }
}

class GetFromMethod : IGetter {
    MethodInfo m;
    public GetFromMethod(MethodInfo m) {
        this.m = m;
    }
    public object GetValue(object target) {
        return m.Invoke(target, new object[0]);
    }
}

public class Comparators {
    public static IEqualityComparer Comparer(Type klass, params String[] props) {
        return new EqualityComparer(klass, props);
    }
}

class EqualityComparer: IEqualityComparer {
    Type klass;
    List<IGetter> getters; 
    public EqualityComparer(Type klass, params String[] props) {
        this.klass = klass;
        getters = new List<IGetter>();
        foreach(string name in props) {
            PropertyInfo p = klass.GetProperty(name);
            if(p != null) {
                getters.Add(new GetFromProperty(p));
                continue;
            }
            FieldInfo f = klass.GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);
            if(f != null) {
                getters.Add(new GetFromField(f));
                continue;
            }
            MethodInfo m = klass.GetMethod(name);
            if(m != null && m.ReturnType != typeof(void) && m.GetParameters().Length == 0) {
                getters.Add(new GetFromMethod(m));
            }
        }
    }
    public bool Equals(object x, object y) {
        if(x.GetType() != klass || y.GetType() != klass) return false;
        foreach(IGetter g in getters) {
            if(g.GetValue(x).Equals(g.GetValue(y)) == false)
                return false;
        }
        return true;
    }
}