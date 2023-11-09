using Application.Entities;

namespace Application.Abstraction;

public interface IPaycheckCalculator
{
    public Task<decimal> CalculatePaycheck(Employee employee);
}
