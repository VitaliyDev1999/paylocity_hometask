using Application.Builders;
using Application.Entities;
using System.Collections.Generic;
using System;
using Xunit;
using Bogus;
using Application.Helpers;

namespace ApiTests.UnitTests;

public class PaycheckBuilderTests
{
    private Faker _faker;

    public PaycheckBuilderTests()
    {
        _faker = new Faker();
    }

    [Fact]
    public void WithEmployee_WhenValidEmployee_IsSet()
    {
        // Arrange
        var builder = new PaycheckBuilder();
        var employee = new Employee();

        // Act
        var result = builder.WithEmployee(employee);

        // Assert
        Assert.Same(employee, builder.Employee);
        Assert.Same(builder, result);
    }

    [Fact]
    public void WithAnnualSalary_WhenValidSalary_IsSet()
    {
        // Arrange
        var builder = new PaycheckBuilder();
        var salary = _faker.Finance.Amount(10000M,150000M);

        // Act
        var result = builder.WithAnnualSalary(salary);

        // Assert
        Assert.Equal(salary, builder.AnnualSalary);
        Assert.Same(builder, result);
    }

    [Fact]
    public void WithAnnualSalary_WhenNegativeSalary_ThrowsArgumentException()
    {
        // Arrange
        var builder = new PaycheckBuilder();
        var salary = _faker.Finance.Amount(Decimal.Zero, Decimal.MinValue);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => builder.WithAnnualSalary(salary));
    }

    [Fact]
    public void WithMonthlyBaseBenefitCost_WhenValidCost_IsUpdated()
    {
        // Arrange
        var builder = new PaycheckBuilder();
        var cost = _faker.Finance.Amount(100M,1000M);
        var expectedResult = cost * 12;

        // Act
        var result = builder.WithMonthlyBaseBenefitCost(cost);

        // Assert
        Assert.Equal(expectedResult, builder.TotalYearBenefitCost);
        Assert.Same(builder, result);
    }

    [Fact]
    public void WithMonthlyBaseBenefitCost_WhenNegativeCost_ThrowsArgumentException()
    {
        // Arrange
        var builder = new PaycheckBuilder();
        var cost = _faker.Finance.Amount(Decimal.Zero, Decimal.MinValue);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => builder.WithMonthlyBaseBenefitCost(cost));
    }

    [Fact]
    public void WithNumberOfPaychecks_WhenValidCount_IsSet()
    {
        // Arrange
        var builder = new PaycheckBuilder();
        var count = _faker.Random.Int(1,30);

        // Act
        var result = builder.WithNumberOfPaychecks(count);

        // Assert
        Assert.Equal(count, builder.NumOfPaychecks);
        Assert.Same(builder, result);
    }

    [Fact]
    public void WithNumberOfPaychecks_WhenNonPositiveCount_ThrowsArgumentException()
    {
        // Arrange
        var builder = new PaycheckBuilder();
        var count = 0;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => builder.WithNumberOfPaychecks(count));
    }

    [Fact]
    public void WithDependents_WhenValidEmployeeAndDependents_UpdateTotalYearBenefitCost()
    {
        // Arrange
        decimal expectedTotalYearBenefitCost = default(decimal);
        var builder = new PaycheckBuilder();
        var employee = new Employee
        {
            Dependents = new List<Dependent>
            {
                new Dependent() {DateOfBirth = _faker.Date.Past()},
                new Dependent() {DateOfBirth = _faker.Date.Past()}
            }
        };

        var dependentBonus = _faker.Finance.Amount(Decimal.One);
        var oldDependentBonus = _faker.Finance.Amount(Decimal.One);
        var bonusAge = _faker.Random.Int(1, 70);

        expectedTotalYearBenefitCost += (dependentBonus * EmployeeHelper.ComputeNumberOfDependents(employee)) * 12;
        expectedTotalYearBenefitCost += (oldDependentBonus * EmployeeHelper.ComputeNumberOfOldDependents(employee, bonusAge)) * 12;

        // Act
        var result = builder.WithEmployee(employee).WithDependents(dependentBonus, oldDependentBonus, bonusAge);

        // Assert
        Assert.Equal(expectedTotalYearBenefitCost, builder.TotalYearBenefitCost);
        Assert.Same(builder, result);
    }

    [Fact]
    public void WithDependents_WhenEmployeeIsNull_NoChangeToTotalYearBenefitCost()
    {
        // Arrange
        var builder = new PaycheckBuilder();
        var dependentBonus = _faker.Finance.Amount(Decimal.One);
        var oldDependentBonus = _faker.Finance.Amount(Decimal.One);
        var bonusAge = _faker.Random.Int(1, 70);

        // Act
        var result = builder.WithDependents(dependentBonus, oldDependentBonus, bonusAge);

        // Assert
        Assert.Equal(0, builder.TotalYearBenefitCost);
        Assert.Same(builder, result);
    }

    [Fact]
    public void WithHighEarnerStatus_WhenHighEarner_AddsToTotalYearBenefitCost()
    {
        // Arrange
        var builder = new PaycheckBuilder();
        var annualSalary = _faker.Finance.Amount(10000M, 100000M);
        var highEarnerLine = _faker.Finance.Amount(10000M, 100000M);
        decimal expectedResult = default(decimal);

        if (annualSalary > highEarnerLine)
            expectedResult += 0.02m * annualSalary;

        // Act
        var result = builder.WithHighEarnerStatus(annualSalary, highEarnerLine);

        // Assert
        Assert.Equal(expectedResult, builder.TotalYearBenefitCost);
        Assert.Same(builder, result);
    }

    [Fact]
    public void ComputePaycheck_WhenValidData_ComputesPaycheck()
    {
        // Arrange
        var builder = new PaycheckBuilder();
        var annualSalary = _faker.Finance.Amount(10000M, 100000M);
        var monthlyBenefitCost = _faker.Finance.Amount(100M, 1000M);
        var numberOfPaychecks = _faker.Random.Int(1, 30);
        builder.WithAnnualSalary(annualSalary)
            .WithMonthlyBaseBenefitCost(monthlyBenefitCost)
            .WithNumberOfPaychecks(numberOfPaychecks);

        var expectedResult = (annualSalary - (monthlyBenefitCost * 12)) / numberOfPaychecks;

        // Act
        var paycheck = builder.ComputePaycheck();

        // Assert
        Assert.Equal(Math.Round(expectedResult,2), paycheck);
    }

    [Fact]
    public void ComputePaycheck_WhenNumOfPaychecksNotSet_ThrowsInvalidOperationException()
    {
        // Arrange
        var builder = new PaycheckBuilder();
        var annualSalary = _faker.Finance.Amount(10000M, 100000M);
        builder.WithAnnualSalary(annualSalary);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => builder.ComputePaycheck());
    }

    [Fact]
    public void ComputePaycheck_WhenAnnualSalaryNotSet_ThrowsInvalidOperationException()
    {
        // Arrange
        var builder = new PaycheckBuilder();
        var numberOfPaychecks = _faker.Random.Int(1, 30);
        builder.WithNumberOfPaychecks(numberOfPaychecks);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => builder.ComputePaycheck());
    }
}
