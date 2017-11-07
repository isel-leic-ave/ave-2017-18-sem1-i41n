using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;

public class LoggableAttribute : Attribute {
}

interface IGetter {
    /**
     *  Gets a value from a property of target and 
     *  returns its value as a string.
     */
    string GetValueAsString(object target);
}

/**
 *  For properties of array type.
 */
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

public abstract class AbstractGetterObject : IGetter {
    public abstract string FieldName();
    public abstract object FieldValue(object target);

    public string GetValueAsString(object target) {
        object val = FieldValue(target);
        return FieldName() + ": " + val + ", ";
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
            BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        res = new List<IGetter>();
        foreach(FieldInfo p in fs) {
            object[] attrs = p.GetCustomAttributes(typeof(LoggableAttribute), true);
            if(attrs.Length == 0) continue;
            if(p.FieldType.IsArray)
                res.Add(new GetterArray(p));
            else
                res.Add(EmitGetterObject(p, klass));
        }
        loggedTypes.Add(klass, res);
        return res;    
    }

    private static IGetter EmitGetterObject(FieldInfo p, Type klass) {
        AssemblyName aName = new AssemblyName("DynamicGetter" + p.Name + "From" + klass.Name);
        AssemblyBuilder ab =
            AppDomain.CurrentDomain.DefineDynamicAssembly(
                aName,
                AssemblyBuilderAccess.RunAndSave);

        // For a single-module assembly, the module name is usually
        // the assembly name plus an extension.
        ModuleBuilder mb =
            ab.DefineDynamicModule(aName.Name, aName.Name + ".dll");

        TypeBuilder tb = mb.DefineType(
            "Getter" + p.Name + "From" + klass.Name,
             TypeAttributes.Public,
             typeof(AbstractGetterObject));

        MethodBuilder methodBuilder = tb.DefineMethod(
                "FieldName",
                MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.ReuseSlot,
                typeof(string), // Return Type 
                Type.EmptyTypes // Types of arguments
            );
        ILGenerator il = methodBuilder.GetILGenerator();
        il.Emit(OpCodes.Ldstr, p.Name);
        il.Emit(OpCodes.Ret);

        methodBuilder = tb.DefineMethod(
                "FieldValue",
                MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.ReuseSlot,
                typeof(object), // Return Type 
                new Type[] { typeof(object)} // Types of arguments
            );
        il = methodBuilder.GetILGenerator();
        il.Emit(OpCodes.Ldarg_1);          // push target
        il.Emit(OpCodes.Castclass, klass); // castclass
        il.Emit(OpCodes.Ldfld, p);         // ldfld 
        il.Emit(OpCodes.Box, p.FieldType); // box
        il.Emit(OpCodes.Ret);              // ret


        // Finish the type.
        Type t = tb.CreateType();
        ab.Save(aName.Name + ".dll");
        return (IGetter) Activator.CreateInstance(t);
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