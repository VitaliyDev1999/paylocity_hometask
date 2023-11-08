using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Application.Features.Dependent;
using Application.Features.Dependent.CreateDependent;
using Application.Features.Employee;
using AutoMapper;

namespace Api.Mappers
{
    public class ApiMapperProfile : Profile
    {
        public ApiMapperProfile()
        {
            CreateMap<GetEmployeeQueryResult, GetEmployeeDto>();
            CreateMap<GetDependentQueryResult, GetDependentDto>();
            CreateMap<CreateDependentCommandResult, CreateDependentDto>();
        }
    }
}
