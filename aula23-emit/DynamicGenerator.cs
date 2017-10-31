using System;
using System.Reflection;
using System.Reflection.Emit;

class DynamicGenerator {
    public static void Main() {
        AssemblyName aName = new AssemblyName("DynamicAssemblyExample");
        AssemblyBuilder ab = 
            AppDomain.CurrentDomain.DefineDynamicAssembly(
                aName, 
                AssemblyBuilderAccess.RunAndSave);

        // For a single-module assembly, the module name is usually
        // the assembly name plus an extension.
        ModuleBuilder mb = 
            ab.DefineDynamicModule(aName.Name, aName.Name + ".dll");

        TypeBuilder tb = mb.DefineType(
            "MyDynamicType", 
             TypeAttributes.Public );

        // Add a private field of type int (Int32).
        FieldBuilder fbNumber = tb.DefineField(
            "m_number", 
            typeof(int), 
            FieldAttributes.Private);

        // Define a constructor that takes an integer argument and 
        // stores it in the private field. 
        Type[] parameterTypes = { typeof(int) };
        ConstructorBuilder ctor1 = tb.DefineConstructor(
            MethodAttributes.Public, 
            CallingConventions.Standard, 
            parameterTypes);

        ILGenerator ctor1IL = ctor1.GetILGenerator();
        // For a constructor, argument zero is a reference to the new
        // instance. Push it on the stack before calling the base
        // class constructor. Specify the default constructor of the 
        // base class (System.Object) by passing an empty array of 
        // types (Type.EmptyTypes) to GetConstructor.
        ctor1IL.Emit(OpCodes.Ldarg_0);
        ctor1IL.Emit(OpCodes.Call, 
            typeof(object).GetConstructor(Type.EmptyTypes));
        // Push the instance on the stack before pushing the argument
        // that is to be assigned to the private field m_number.
        ctor1IL.Emit(OpCodes.Ldarg_0);
        ctor1IL.Emit(OpCodes.Ldarg_1);
        ctor1IL.Emit(OpCodes.Stfld, fbNumber);
        ctor1IL.Emit(OpCodes.Ret);

        MethodBuilder getNumberBuilder = tb.DefineMethod(
                "GetNumber",
                MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.ReuseSlot,
                typeof(int), // Return Type 
                Type.EmptyTypes // Types of arguments
            );
        ILGenerator getNumberIl = getNumberBuilder.GetILGenerator();
        getNumberIl.Emit(OpCodes.Ldarg_0);
        getNumberIl.Emit(OpCodes.Ldfld, fbNumber);
        getNumberIl.Emit(OpCodes.Ret);

        // Finish the type.
        Type t = tb.CreateType();

        // The following line saves the single-module assembly. This
        // requires AssemblyBuilderAccess to include Save. You can now
        // type "ildasm MyDynamicAsm.dll" at the command prompt, and 
        // examine the assembly. You can also write a program that has
        // a reference to the assembly, and use the MyDynamicType type.
        // 
        ab.Save(aName.Name + ".dll");
    }
}