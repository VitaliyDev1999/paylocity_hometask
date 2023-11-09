using Application.Entities;
using Application.Helpers;

namespace Application.Builders;

public class PaycheckBuilder
{
    private Employee? Employee { get; set; }

    private int NumOfPaychecks { get; set; }
    private decimal AnnualSalary {get; set; }
    private decimal TotalYearBenefitCost { get; set; }

    public PaycheckBuilder WithEmployee(Employee employee)
    {
        Employee = employee;
        return this;
    }

    public PaycheckBuilder WithAnnualSalary(decimal annualSalary)
    {
        AnnualSalary = annualSalary;
        return this;
    }

    public PaycheckBuilder WithMonthlyBaseBenefitCost(decimal monthlyBaseBenefitCost = 1000M)
    {
        TotalYearBenefitCost += monthlyBaseBenefitCost * 12;
        return this;
    }

    public PaycheckBuilder WithNumberOfPaychecks(int numOfPaychecks = 26)
    {
        NumOfPaychecks = numOfPaychecks;
        return this;
    }

    public PaycheckBuilder WithDependents(decimal dependentBonus = 600M, decimal oldDependentBonus = 200M, int bonusAge = 50)
    {
        if (Employee != null && Employee.Dependents.Any())
        {
            TotalYearBenefitCost += (dependentBonus * EmployeeHelper.ComputeNumberOfDependents(Employee)) * 12;
            TotalYearBenefitCost += (oldDependentBonus * EmployeeHelper.ComputeNumberOfOldDependents(Employee, bonusAge)) * 12;
        }

        return this;
    }

    public PaycheckBuilder WithHighEarnerStatus(decimal annualSalary, decimal highEarnerLine = 80000M)
    {
        var isHighEarner = annualSalary > highEarnerLine;
        if(isHighEarner)
            TotalYearBenefitCost += 0.02m * annualSalary;

        return this;
    }

    public decimal ComputePaycheck()
    {
        var paycheck = (AnnualSalary - TotalYearBenefitCost) / NumOfPaychecks;
        return Math.Round(paycheck, 2);
    } 
}
