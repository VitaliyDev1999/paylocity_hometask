using Application.Abstraction;
using AutoMapper;
using MediatR;

namespace Application.Features.Dependent.GetDependentById;

public class GetDependentByIdQueryHandler : IRequestHandler<GetDependentByIdQuery, GetDependentByIdQueryResult>
{
    private readonly IDependentsRepository _dependentsRepository;
    private readonly IMapper _mapper;

    public GetDependentByIdQueryHandler(IDependentsRepository dependentsRepository, IMapper mapper)
    {
        _dependentsRepository = dependentsRepository ?? throw new ArgumentException(nameof(dependentsRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<GetDependentByIdQueryResult> Handle(GetDependentByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await _dependentsRepository.GetByIdAsync(request.Id) ?? throw new Exception("Dependent was not found.");

        return new GetDependentByIdQueryResult()
        {
            DependentQueryResult = _mapper.Map<GetDependentQueryResult>(response)
        };
    }
}
