using Application.Entities;
using Application.Features.Dependent;
using Application.Features.Dependent.CreateDependent;
using Application.Features.Employee;
using AutoMapper;

namespace Application.Mapper;

public class ApplicationMapperProfile : Profile
{
    public ApplicationMapperProfile()
    {
        CreateMap<Employee, GetEmployeeQueryResult>();
        CreateMap<Dependent, GetDependentQueryResult>();
        CreateMap<Dependent, CreateDependentCommandResult>();
    }
}
