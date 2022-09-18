.class public abstract sealed auto ansi beforefieldinit
  Hw2.Parser extends [System.Runtime]System.Object
{
    .method public static void ParseCalcArguments(
            string[] args,
            [out] float64& val1,
            [out] valuetype Hw2.CalculatorOperation& operation,
            [out] float64& val2)
    {
                    ldarg.0
                    call bool Hw2.Parser::IsArgLengthSupported(string[])
                    brfalse.s exception
                    
                    ldarg.0
                    ldc.i4.0
                    ldelem.ref
                    ldarg.1
                    call bool [System.Runtime]System.Double::TryParse(string, float64&)
                    brfalse.s exception
                    
                    ldarg.0
                    ldc.i4.2
                    ldelem.ref
                    ldarg.3
                    call bool [System.Runtime]System.Double::TryParse(string, float64&)
                    brfalse.s exception
                    
                    ldarg.2
                    ldarg.0
                    ldc.i4.1
                    ldelem.ref
                    call valuetype Hw2.CalculatorOperation Hw2.Parser::ParseOperation(string)
                    stind.i4
                    
                    ret
                    
                    exception:
                    newobj instance void [System.Runtime]System.ArgumentException::.ctor()
                    throw
    }

    .method private static bool IsArgLengthSupported (string[] args) cil managed
    {
                    ldarg.0         
                    ldlen
                    ldc.i4.3
                    ceq
                    ret
    }
    
    .method private static valuetype Hw2.CalculatorOperation ParseOperation(string arg)
    {
                    ldarg.0
                    ldstr "+"
                    call bool [System.Runtime]System.String::op_Equality(string, string)
                    brtrue.s plus
                    
                    ldarg.0
                    ldstr "-"
                    call bool [System.Runtime]System.String::op_Equality(string, string)
                    brtrue.s minus   
                    
                    ldarg.0
                    ldstr "*"
                    call bool [System.Runtime]System.String::op_Equality(string, string)
                    brtrue.s multiply     
                    
                    ldarg.0
                    ldstr "/"
                    call bool [System.Runtime]System.String::op_Equality(string, string)
                    brtrue.s divide     
                    
                    br.s unknown     
                    
                    plus:
                    ldc.i4.0
                    ret
                    
                    minus:
                    ldc.i4.1
                    ret
                    
                    multiply:
                    ldc.i4.2
                    ret
                    
                    divide:
                    ldc.i4.3
                    ret
                    
                    unknown:
                    newobj instance void [System.Runtime]System.InvalidOperationException::.ctor()
                    throw
    }
}