using Application.Abstraction.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Dependent.CreateDependent;

public class CreateDependentCommandHandler : IRequestHandler<CreateDependentCommand, CreateDependentCommandResult>
{
    private readonly IDependentsRepository _dependentsRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public CreateDependentCommandHandler(IDependentsRepository dependentsRepository, IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _dependentsRepository = dependentsRepository ?? throw new ArgumentException(nameof(dependentsRepository));
        _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<CreateDependentCommandResult> Handle(CreateDependentCommand request, CancellationToken cancellationToken)
    {
        var dependent = new Entities.Dependent()
        {
            EmployeeId = request.EmployeeId,
            DateOfBirth = request.DateOfBirth,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Relationship = request.Relationship
        };

        var result = await _dependentsRepository.CreateAsync(dependent);

        return _mapper.Map<CreateDependentCommandResult>(result);
    }
}
