
//  Microsoft (R) .NET Framework IL Disassembler.  Version 4.6.1055.0
//  Copyright (c) Microsoft Corporation.  All rights reserved.



// Metadata version: v4.0.30319
.assembly extern mscorlib
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 4:0:0:0
}
.assembly App
{
  .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilationRelaxationsAttribute::.ctor(int32) = ( 01 00 08 00 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Runtime.CompilerServices.RuntimeCompatibilityAttribute::.ctor() = ( 01 00 01 00 54 02 16 57 72 61 70 4E 6F 6E 45 78   // ....T..WrapNonEx
                                                                                                             63 65 70 74 69 6F 6E 54 68 72 6F 77 73 01 )       // ceptionThrows.

  // --- The following custom attribute is added automatically, do not uncomment -------
  //  .custom instance void [mscorlib]System.Diagnostics.DebuggableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggableAttribute/DebuggingModes) = ( 01 00 07 01 00 00 00 00 ) 

  .hash algorithm 0x00008004
  .ver 0:0:0:0
}
.module App.exe
// MVID: {EB3C825B-BBED-44AF-9BC0-5702903C883F}
.imagebase 0x00400000
.file alignment 0x00000200
.stackreserve 0x00100000
.subsystem 0x0003       // WINDOWS_CUI
.corflags 0x00000001    //  ILONLY
// Image base: 0x00630000


// =============== CLASS MEMBERS DECLARATION ===================

.class private auto ansi beforefieldinit Student
       extends [mscorlib]System.Object
{
  .field private int32 nr
  .field private string name
  .method public hidebysig specialname rtspecialname 
          instance void  .ctor(int32 nr,
                               string name) cil managed
  {
    // Code size       23 (0x17)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
    IL_0006:  nop
    IL_0007:  nop
    IL_0008:  ldarg.0
    IL_0009:  ldarg.1
    IL_000a:  stfld      int32 Student::nr
    IL_000f:  ldarg.0
    IL_0010:  ldarg.2
    IL_0011:  stfld      string Student::name
    IL_0016:  ret
  } // end of method Student::.ctor

  .method public hidebysig instance void 
          Print() cil managed
  {
    // Code size       13 (0xd)
    .maxstack  8
    IL_0000:  nop
    IL_0001:  ldstr      "I am a Student"
    IL_0006:  call       void [mscorlib]System.Console::WriteLine(string)
    IL_000b:  nop
    IL_000c:  ret
  } // end of method Student::Print

} // end of class Student

.class private abstract auto ansi sealed beforefieldinit App
       extends [mscorlib]System.Object
{
  .method public hidebysig static void  Foo() cil managed
  {
    // Code size       2 (0x2)
    .maxstack  8
    IL_0000:  nop
    IL_0001:  ret
  } // end of method App::Foo

  .method private hidebysig static void  Main() cil managed
  {
    .entrypoint
    // Code size       47 (0x2f)
    .maxstack  2
    .locals init ([0] class Student s)
    IL_0000:  nop
    IL_0001:  ldc.i4     0x6bd
    IL_0006:  ldstr      "Ze Manel"
    IL_000b:  newobj     instance void Student::.ctor(int32,
                                                      string)
    IL_0010:  stloc.0
    IL_0011:  call       void App::Foo()
    IL_0016:  nop
    IL_0017:  ldloc.0
    IL_0018:  callvirt   instance void Student::Print()
    IL_001d:  nop
    IL_001e:  ldloc.0
    IL_001f:  callvirt   instance string [mscorlib]System.Object::ToString()
    IL_0024:  pop
    IL_0025:  ldnull
    IL_0026:  stloc.0
    IL_0027:  ldloc.0
    IL_0028:  call   instance void Student::Print()
    IL_002d:  nop
    IL_002e:  ret
  } // end of method App::Main

} // end of class App


// =============================================================

// *********** DISASSEMBLY COMPLETE ***********************
// WARNING: Created Win32 resource file C:\MyFolder\ISEL\pg5 ave - 2017-2018 - 1º sem\aulas\aula21-methods\App.res
