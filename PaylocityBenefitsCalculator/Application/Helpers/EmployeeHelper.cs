using Application.Entities;

namespace Application.Helpers;

public static class EmployeeHelper
{
    public static int ComputeAge(DateTime birthDate)
    {
        DateTime currentDate = DateTime.Today;
        int age = currentDate.Year - birthDate.Year;

        // Check if the birthday for this year has already occurred
        if (birthDate > currentDate.AddYears(-age))
        {
            age--;
        }

        return age;
    }

    public static int ComputeNumberOfDependents(Employee employee)
    {
        return employee.Dependents.Count();
    }

    public static int ComputeNumberOfOldDependents(Employee employee, int bonusAge)
    {
        var numberOfDepends = employee.Dependents
            .Count(x => EmployeeHelper.ComputeAge(x.DateOfBirth) >= bonusAge);

        return numberOfDepends;
    }
}
