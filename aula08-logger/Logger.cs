using System;
using System.Reflection;
using System.Collections.Generic;

public class LoggableAttribute : Attribute {
}

public class Logger {
    public static void Fields (object obj) {
        Console.WriteLine("{0} => {1}", obj.GetType(), ObjFieldsToString(obj));
    }
    
    static Dictionary<Type, List<FieldInfo>> loggedTypes = new Dictionary<Type, List<FieldInfo>>();
    
    static List<FieldInfo> GetLoggableFields(Type klass) {
        List<FieldInfo> res;
        if(loggedTypes.TryGetValue(klass, out res)) return res;
        FieldInfo[] fs = klass.GetFields(
            BindingFlags.NonPublic | BindingFlags.Instance);
        res = new List<FieldInfo>();
        foreach(FieldInfo p in fs) {
            object[] attrs = p.GetCustomAttributes(typeof(LoggableAttribute), true);
            if(attrs.Length == 0) continue;
            res.Add(p);
        }
        loggedTypes.Add(klass, res);
        return res;    
    }
    
    static string ObjFieldsToString(object obj) {
        String str = "";
        List<FieldInfo> fs = GetLoggableFields(obj.GetType());
        foreach(FieldInfo p in fs) {
            str += p.Name + ": ";
            object val = p.GetValue(obj);
            if(val.GetType().IsArray == false) {
                str += val + ", ";
            }
            else {
                object[] arr = (object[]) val;
                str += "[";
                for(int i = 0; i < arr.Length; i++) {
                    str += ObjFieldsToString(arr[i]) + ", ";
                }
                str += "]";
            }
        }
        return str;
    }

}