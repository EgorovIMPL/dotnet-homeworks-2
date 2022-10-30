using System.Globalization;

namespace Hw8.Calculator;

public static class Parser
{
    public static void ParseCalcArguments(string[] args,
        out double val1,
        out Operation operation,
        out double val2)
    {
        var tryVal1 = double.TryParse(args[0],NumberStyles.AllowDecimalPoint, 
            CultureInfo.InvariantCulture, out val1);
        var tryVal2 = double.TryParse(args[2],NumberStyles.AllowDecimalPoint, 
            CultureInfo.InvariantCulture, out val2);
        
        if (!(tryVal1 && tryVal2))
            throw new ArgumentException(Messages.InvalidNumberMessage);
        
        operation = ParseOperation(args[1]);
        
        if (operation == Operation.Invalid)
            throw new InvalidOperationException(Messages.InvalidOperationMessage);
    }

    private static Operation ParseOperation(string arg)
    {
        Operation result = arg switch
        {
            "Plus" => Operation.Plus,
            "Minus" => Operation.Minus,
            "Divide" => Operation.Divide,
            "Multiply" => Operation.Multiply,
            _ => Operation.Invalid
        };
        return result;
    }
}