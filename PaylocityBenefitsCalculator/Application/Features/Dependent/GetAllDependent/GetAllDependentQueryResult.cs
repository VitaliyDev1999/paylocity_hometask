using Application.Features.Employee;

namespace Application.Features.Dependent.GetAllDependent;

public class GetAllDependentQueryResult
{
    public IEnumerable<GetDependentQueryResult> DependentQueryResult { get; set; }
}
