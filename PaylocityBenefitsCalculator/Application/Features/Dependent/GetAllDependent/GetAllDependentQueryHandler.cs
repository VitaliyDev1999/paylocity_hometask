using Application.Abstraction;
using Application.Features.Employee.GetAllEmployee;
using Application.Features.Employee;
using AutoMapper;
using MediatR;

namespace Application.Features.Dependent.GetAllDependent;

internal class GetAllDependentQueryHandler : IRequestHandler<GetAllDependentQuery, GetAllDependentQueryResult>
{
    private readonly IDependentsRepository _dependentsRepository;
    private readonly IMapper _mapper;

    public GetAllDependentQueryHandler(IDependentsRepository dependentsRepository, IMapper mapper)
    {
        _dependentsRepository = dependentsRepository ?? throw new ArgumentException(nameof(dependentsRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<GetAllDependentQueryResult> Handle(GetAllDependentQuery request, CancellationToken cancellationToken)
    {
        var result = await _dependentsRepository.GetAll();

        return new GetAllDependentQueryResult()
        {
            DependentQueryResult = _mapper.Map<IEnumerable<GetDependentQueryResult>>(result),
        };
    }
}
