namespace Hw1;

public static class Parser
{
    public static void ParseCalcArguments(string[] args, 
        out double val1, 
        out CalculatorOperation operation, 
        out double val2)
    {
        if (IsArgLengthSupported(args))
        {
            var tryVal1 = double.TryParse(args[0], out val1);
            var tryVal2 = double.TryParse(args[2], out val2);
            if (tryVal1 && tryVal2)
            {
                operation = ParseOperation(args[1]);
                if (operation == CalculatorOperation.Undefined)
                    throw new InvalidOperationException();
            }
            else
                throw new ArgumentException();
        }
        else
        {
            throw new ArgumentException();
        }
    }

    private static bool IsArgLengthSupported(string[] args) => args.Length == 3;

    private static CalculatorOperation ParseOperation(string arg)
    {
        CalculatorOperation result = arg switch
        {
            "+" => CalculatorOperation.Plus,
            "-" => CalculatorOperation.Minus,
            "/" => CalculatorOperation.Divide,
            "*" => CalculatorOperation.Multiply,
            _ => CalculatorOperation.Undefined
        };
        return result;
    }
}