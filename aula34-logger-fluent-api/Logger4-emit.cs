using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;

public class LoggableAttribute : Attribute {
}

interface ILogger {
    /**
     * Returns a string with the fields values of the target object.
     */
    string Log(object target);
}

public abstract class AbstractLogger : ILogger {
    private Logger logger;
    
    public AbstractLogger(Logger l) { logger = l; }

    public string Format(string name, object val) {
        return name + ": " + logger.ObjFieldsToString(val) + ", ";
    }
    public string Format(string name, object[] arr)
    {
        string str = name + ": [";
        for (int i = 0; i < arr.Length; i++)
        {
            str += logger.ObjFieldsToString(arr[i]) + ", ";
        }
        return str + "]";
    }

    public abstract string Log(object target);
}

public class Logger {
    static readonly MethodInfo formatterForArray = typeof(AbstractLogger).GetMethod("Format", new Type[] { typeof(String), typeof(object[]) });
    static readonly MethodInfo formatterForObject = typeof(AbstractLogger).GetMethod("Format", new Type[] { typeof(String), typeof(object) });
    static readonly MethodInfo concat = typeof(String).GetMethod("Concat", new Type[] { typeof(string), typeof(string) });


    public void Fields (object obj) {
        Console.WriteLine("{0} => {1}", obj.GetType(), ObjFieldsToString(obj));
    }
    
    Dictionary<Type, ILogger> loggedTypes = new Dictionary<Type, ILogger>();

    private ILogger EmitLogger(Type klass) {
        AssemblyName aName = new AssemblyName("DynamicLogger" + klass.Name);
        AssemblyBuilder ab =
            AppDomain.CurrentDomain.DefineDynamicAssembly(
                aName,
                AssemblyBuilderAccess.RunAndSave);

        // For a single-module assembly, the module name is usually
        // the assembly name plus an extension.
        ModuleBuilder mb =
            ab.DefineDynamicModule(aName.Name, aName.Name + ".dll");

        TypeBuilder tb = mb.DefineType(
            "Logger" + klass.Name,
             TypeAttributes.Public,
             typeof(AbstractLogger));
        
        EmitCtor(klass, tb);
        EmitLogMethod(klass, tb);

        // Finish the type.
        Type t = tb.CreateType();
        ab.Save(aName.Name + ".dll");
        return (ILogger) Activator.CreateInstance(t, new object[]{this});
    }

    public static void EmitCtor(Type klass, TypeBuilder tb) {
        Type[] parameterTypes = { typeof(Logger) };
        ConstructorBuilder ctor1 = tb.DefineConstructor(
            MethodAttributes.Public, 
            CallingConventions.Standard, 
            parameterTypes);

        ILGenerator ctor1IL = ctor1.GetILGenerator();
        ctor1IL.Emit(OpCodes.Ldarg_0);
        ctor1IL.Emit(OpCodes.Ldarg_1);
        ctor1IL.Emit(OpCodes.Call, 
            typeof(AbstractLogger).GetConstructor(parameterTypes));
        ctor1IL.Emit(OpCodes.Ret);
    }
    
    public static void EmitLogMethod(Type klass, TypeBuilder tb) {
        MethodBuilder methodBuilder = tb.DefineMethod(
                "Log",
                MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.ReuseSlot,
                typeof(string), // Return Type 
                new Type[] { typeof(object)} // Types of arguments
            );
        ILGenerator il = methodBuilder.GetILGenerator();

        FieldInfo[] fs = klass.GetFields(
            BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

        LocalBuilder target = il.DeclareLocal(klass);
        il.Emit(OpCodes.Ldarg_1);          // push target
        il.Emit(OpCodes.Castclass, klass); // castclass
        il.Emit(OpCodes.Stloc, target);    // store on local variable 

        il.Emit(OpCodes.Ldstr, "");
        foreach (FieldInfo p in fs)
        {
            object[] attrs = p.GetCustomAttributes(typeof(LoggableAttribute), true);
            if (attrs.Length == 0) continue;

            il.Emit(OpCodes.Ldarg_0);          // push this
            il.Emit(OpCodes.Ldstr, p.Name);    // push on stack the field name
            il.Emit(OpCodes.Ldloc, target);    // ldloc target
            il.Emit(OpCodes.Ldfld, p);         // ldfld 
            if (p.FieldType.IsValueType)
                il.Emit(OpCodes.Box, p.FieldType); // box
            if (p.FieldType.IsArray)
                il.Emit(OpCodes.Call, formatterForArray);
            else
                il.Emit(OpCodes.Call, formatterForObject);
            il.Emit(OpCodes.Call, concat);
        }
        il.Emit(OpCodes.Ret);              // ret
    }
    
    public string ObjFieldsToString(object obj) {
        ILogger logger;
        Type klass = obj.GetType();
        if(klass.IsPrimitive || klass == typeof(string))
            return obj.ToString();
        if (!loggedTypes.TryGetValue(klass, out logger)){
            logger = EmitLogger(klass);
            loggedTypes.Add(klass, logger);
        }
        return logger.Log(obj);
    }
    
    public Logger For<T>(Func<T, string> formatter) {
        Type t = typeof(T);
        loggedTypes.Add(t, new LoggerFormatter<T>(formatter));
        return this;
    }
    class LoggerFormatter<T> : ILogger{
        Func<T, string> formatter;
        public LoggerFormatter(Func<T, string> f) {
            formatter = f;
        }
        public string Log(object o) {
            return formatter((T) o);
        }
    }
}