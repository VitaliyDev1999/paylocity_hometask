using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Application.Features.Dependent.CreateDependent;
using Application.Features.Dependent.GetAllDependent;
using Application.Features.Dependent.GetDependentById;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public DependentsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        var query = new GetDependentByIdQuery() { Id = id };

        var result = await _mediator.Send(query);

        var response = new ApiResponse<GetDependentDto>
        {
            Data = _mapper.Map<GetDependentDto>(result.DependentQueryResult),
            Success = true
        };

        return response;
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        var query = new GetAllDependentQuery();

        var result = await _mediator.Send(query);

        var response = new ApiResponse<List<GetDependentDto>>
        {
            Data = _mapper.Map<List<GetDependentDto>>(result.DependentQueryResult),
            Success = true
        };

        return response;
    }

    [SwaggerOperation(Summary = "Add dependents")]
    [HttpPost]
    public async Task<ActionResult<ApiResponse<CreateDependentDto>>> CreateDependents(CreateDependentRequest request)
    {
        var command = new CreateDependentCommand()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Relationship = request.Relationship,
            EmployeeId = request.EmployeeId,
            DateOfBirth = request.DateOfBirth
        };

        var result = await _mediator.Send(command);

        var response = new ApiResponse<CreateDependentDto>
        {
            Data = _mapper.Map<CreateDependentDto>(result),
            Success = true
        };

        return response;
    }
}
