using System;
using System.Reflection;
using System.Collections.Generic;

public class LoggableAttribute : Attribute {
}

interface IGetter {
    string GetValueAsString(object target);
}

class GetterArray : IGetter {
    FieldInfo p;
    public GetterArray(FieldInfo p) {
        this.p = p;
    }
    public string GetValueAsString(object target) {
        object[] arr = (object[]) p.GetValue(target);
        string str = p.Name + ": [" ;
        for(int i = 0; i < arr.Length; i++) {
            str += Logger.ObjFieldsToString(arr[i]) + ", ";
        }
        return str + "]";
    }
}

class GetterObject : IGetter {
    FieldInfo p;
    public GetterObject(FieldInfo p) {
        this.p = p;
    }
    public string GetValueAsString(object target) {
        object val = p.GetValue(target);
        return p.Name + ": " + val + ", ";
    }
}


public class Logger {
    public static void Fields (object obj) {
        Console.WriteLine("{0} => {1}", obj.GetType(), ObjFieldsToString(obj));
    }
    
    static Dictionary<Type, List<IGetter>> loggedTypes = new Dictionary<Type, List<IGetter>>();
    
    static List<IGetter> GetLoggableFields(Type klass) {
        List<IGetter> res;
        if(loggedTypes.TryGetValue(klass, out res)) return res;
        FieldInfo[] fs = klass.GetFields(
            BindingFlags.NonPublic | BindingFlags.Instance);
        res = new List<IGetter>();
        foreach(FieldInfo p in fs) {
            object[] attrs = p.GetCustomAttributes(typeof(LoggableAttribute), true);
            if(attrs.Length == 0) continue;
            if(p.FieldType.IsArray)
                res.Add(new GetterArray(p));
            else
                res.Add(new GetterObject(p));
        }
        loggedTypes.Add(klass, res);
        return res;    
    }
    
    public static string ObjFieldsToString(object obj) {
        String str = "";
        List<IGetter> fs = GetLoggableFields(obj.GetType());
        foreach(IGetter p in fs) {
            str += p.GetValueAsString(obj);
        }
        return str;
    }

}