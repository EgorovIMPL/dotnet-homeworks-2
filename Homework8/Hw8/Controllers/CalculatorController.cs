using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Hw8.Calculator;
using Microsoft.AspNetCore.Mvc;

namespace Hw8.Controllers;

public class CalculatorController : Controller
{
    public ActionResult<double> Calculate([FromServices] ICalculator calculator,
        string val1,
        string operation,
        string val2)
    {
        double val1Parse = 0.0;
        Operation operationParse;
        double val2Parse = 0.0;

        double result;
        try
        {
            Parser.ParseCalcArguments(new[] {val1, operation, val2}, out val1Parse, out operationParse, out val2Parse);
            result = new Calculator.Calculator().Calculate(val1Parse, operationParse, val2Parse);
        }
        catch (Exception e)
        {
            if(e is ArgumentException or InvalidOperationException)
                return BadRequest(e.Message);
            
            throw;
        }

        return Ok(result);
    }
    
    [ExcludeFromCodeCoverage]
    public IActionResult Index()
    {
        return Content(
            "Заполните val1, operation(plus, minus, multiply, divide) и val2 здесь '/calculator/calculate?val1= &operation= &val2= '\n" +
            "и добавьте её в адресную строку.");
    }
}