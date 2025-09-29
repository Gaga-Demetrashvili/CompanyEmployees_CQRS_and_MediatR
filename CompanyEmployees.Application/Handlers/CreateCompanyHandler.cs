using AutoMapper;
using CompanyEmployees.Application.Commands;
using CompanyEmployees.Core.Domain.Entities;
using CompanyEmployees.Core.Domain.Repositories;
using MediatR;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Application.Handlers;

internal sealed class CreateCompanyHandler(IRepositoryManager repository, IMapper mapper)
    : IRequestHandler<CreateCompanyCommand, CompanyDto>
{
    public async Task<CompanyDto> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var companyEntity = mapper.Map<Company>(request.Company);

        repository.Company.CreateCompany(companyEntity);
        await repository.SaveAsync();

        var companyToReturn = mapper.Map<CompanyDto>(companyEntity);

        return companyToReturn;
    }
}
