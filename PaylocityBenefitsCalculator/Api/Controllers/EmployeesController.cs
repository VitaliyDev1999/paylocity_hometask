using Api.Dtos.Employee;
using Api.Models;
using Application.Entities;
using Application.Features.Employee.GetAllEmployee;
using Application.Features.Employee.GetEmployeeById;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public EmployeesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        var query = new GetEmployeeByIdQuery() { Id = id };

        var result = await _mediator.Send(query);

        var response = new ApiResponse<GetEmployeeDto>
        {
            Data = _mapper.Map<GetEmployeeDto>(result.EmployeeQueryResult),
            Success = true
        };

        return response;
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        var query = new GetAllEmployeeQuery();

        var result = await _mediator.Send(query);

        var response  = new ApiResponse<List<GetEmployeeDto>>
        {
            Data = _mapper.Map<List<GetEmployeeDto>>(result.EmployeeQueryResults),
            Success = true
        };

        return response;
    }
}
