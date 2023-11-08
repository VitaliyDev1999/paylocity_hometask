using Application.Entities;

namespace Application.Features.Employee.GetAllEmployee;

public class GetAllEmployeeQueryResult
{
    public IEnumerable<GetEmployeeQueryResult> EmployeeQueryResults {get; set; }
}

