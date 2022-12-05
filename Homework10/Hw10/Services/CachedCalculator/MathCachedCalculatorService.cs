using Hw10.DbModels;
using Hw10.Dto;
using Hw10.Services.MathCalculator;
using Microsoft.Extensions.Caching.Memory;

namespace Hw10.Services.CachedCalculator;

public class MathCachedCalculatorService : IMathCalculatorService
{
	private readonly IMemoryCache _memoryCache;
	private readonly IMathCalculatorService _simpleCalculator;

	public MathCachedCalculatorService(IMemoryCache memoryCache, IMathCalculatorService simpleCalculator)
	{
		_memoryCache = memoryCache;
		_simpleCalculator = simpleCalculator;
	}

	public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
	{
		if (expression is not null)
		{
			var solvingExpression = _memoryCache.Get<double?>(expression);

			if (solvingExpression is not null)
				return new CalculationMathExpressionResultDto(solvingExpression.Value);
		}

		var dto = await _simpleCalculator.CalculateMathExpressionAsync(expression);

		if (dto.IsSuccess)
		{
			_memoryCache.Set(expression, dto.Result);
		}

		return dto;
	}
}