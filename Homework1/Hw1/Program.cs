namespace Hw1
{
    class Program
    {
        static int Main()
        {
            try
            {
                var input = Console.ReadLine();
                var args = input.Split(" ");
                double val1;
                double val2;
                CalculatorOperation operation;
                Parser.ParseCalcArguments(args, out val1, out operation, out val2);
                var result = Calculator.Calculate(val1, operation, val2);
                Console.WriteLine(result);
                return 0;
            }
            catch (ArgumentOutOfRangeException)
            {
                return -1;
            }
            catch (InvalidOperationException)
            {
                return -2;
            }
            catch (ArgumentException)
            {
                return -3;
            }
        }
    }
}