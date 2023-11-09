using Application.Abstraction;
using Application.Abstraction.Repositories;
using Application.Builders;
using Application.Entities;

namespace Application.Calculator;

public class PaycheckCalculator : IPaycheckCalculator
{
    private readonly PaycheckBuilder _builder;

    public PaycheckCalculator()
    {
        _builder = new PaycheckBuilder();
    }

    public async Task<decimal> CalculatePaycheck(Employee employee)
    {
        return await Task.Run(() =>
            _builder.WithEmployee(employee)
                .WithNumberOfPaychecks()
                .WithAnnualSalary(employee.Salary)
                .WithMonthlyBaseBenefitCost()
                .WithDependents()
                .WithHighEarnerStatus(employee.Salary)
                .ComputePaycheck()
        );
    }
}
