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
        Employee = employee ?? throw new ArgumentNullException(nameof(employee));
        Employee = employee;
        return this;
    }

    public PaycheckBuilder WithAnnualSalary(decimal annualSalary)
    {
        if (annualSalary < 0)
        {
            throw new ArgumentException("Annual salary cannot be negative.");
        }

        AnnualSalary = annualSalary;
        return this;
    }

    public PaycheckBuilder WithMonthlyBaseBenefitCost(decimal monthlyBaseBenefitCost = 1000M)
    {
        if (monthlyBaseBenefitCost < 0)
        {
            throw new ArgumentException("Monthly base benefit cost cannot be negative.");
        }
        TotalYearBenefitCost += monthlyBaseBenefitCost * 12;
        return this;
    }

    public PaycheckBuilder WithNumberOfPaychecks(int numOfPaychecks = 26)
    {
        if (numOfPaychecks <= 0)
        {
            throw new ArgumentException("Number of paychecks must be greater than zero.");
        }
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
        if (NumOfPaychecks <= 0)
        {
            throw new InvalidOperationException("Number of paychecks must be set before computing the paycheck.");
        }

        if (AnnualSalary <= 0)
        {
            throw new InvalidOperationException("Annual salary be set before computing the paycheck.");
        }

        var paycheck = (AnnualSalary - TotalYearBenefitCost) / NumOfPaychecks;
        return Math.Round(paycheck, 2);
    } 
}
