.class public abstract sealed auto ansi beforefieldinit
  Hw2.Calculator extends [System.Runtime]System.Object
{
    .method public static float64 Calculate(float64 value1, valuetype Hw2.CalculatorOperation operation, float64 value2) cil managed
    {
        ldarg.0
        ldarg.2
        
        ldarg.1
        ldc.i4.0
        beq.s plus
        
        ldarg.1
        ldc.i4.1
        beq.s minus
        
        ldarg.1
        ldc.i4.2
        beq.s multiply
        
        ldarg.1
        ldc.i4.3
        beq.s divide
        
        br.s unknown
        
        plus:
        add
        ret
        
        minus:
        sub
        ret
               
        multiply:
        mul
       ret
                  
        divide:
        div
        ret
                
        unknown:
        newobj instance void [System.Runtime]System.ArgumentOutOfRangeException::.ctor()
        throw
    }
}



