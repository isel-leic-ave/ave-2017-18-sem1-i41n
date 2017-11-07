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

public abstract class AbstractGetterObject : IGetter {
    public abstract string FieldName();
    public abstract object FieldValue(object target);

    public string GetValueAsString(object target) {
        object val = FieldValue(target);
        return FieldName() + ": " + val + ", ";
    }
}

public abstract class AbstractGetterArray : IGetter
{
    public abstract string FieldName();
    public abstract object[] FieldValue(object target);

    public string GetValueAsString(object target)
    {
        object[] arr = FieldValue(target);
        string str = FieldName() + ": [";
        for (int i = 0; i < arr.Length; i++)
        {
            str += Logger.ObjFieldsToString(arr[i]) + ", ";
        }
        return str + "]";
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
                res.Add(EmitGetterArray(p, klass));
            else
                res.Add(EmitGetterObject(p, klass));
        }
        loggedTypes.Add(klass, res);
        return res;    
    }

    private static IGetter EmitGetterObject(FieldInfo p, Type klass)
    {
        return (IGetter)EmitGetter(p, klass, typeof(AbstractGetterObject));
    }

    private static IGetter EmitGetterArray(FieldInfo p, Type klass)
    {
        return (IGetter)EmitGetter(p, klass, typeof(AbstractGetterArray));
    }

    private static IGetter EmitGetter(FieldInfo p, Type klass, Type abstractGetterType) {
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
             abstractGetterType);

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
                abstractGetterType.GetMethod("FieldValue").ReturnType, // Return Type 
                new Type[] { typeof(object)} // Types of arguments
            );
        il = methodBuilder.GetILGenerator();
        il.Emit(OpCodes.Ldarg_1);          // push target
        il.Emit(OpCodes.Castclass, klass); // castclass
        il.Emit(OpCodes.Ldfld, p);         // ldfld 
        if(p.FieldType.IsValueType)
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