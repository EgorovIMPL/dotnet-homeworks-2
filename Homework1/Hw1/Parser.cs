using System.Security.Cryptography.X509Certificates;

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
            
            bool tryVal1 = double.TryParse(args[0], out val1);
            bool tryVal2 = double.TryParse(args[2], out val2);
            if (tryVal1 && tryVal2)
                operation = ParseOperation(args[1]);
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
        if (arg == "+")
            return CalculatorOperation.Plus;
        else if (arg == "-")
            return CalculatorOperation.Minus;
        else if (arg == "*")
            return CalculatorOperation.Multiply;
        else if (arg == "/")
            return CalculatorOperation.Divide;
        else
            throw new InvalidOperationException();
    }
}