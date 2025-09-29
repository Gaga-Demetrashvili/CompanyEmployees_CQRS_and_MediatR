using AutoMapper;
using CompanyEmployees.Application.Queries;
using CompanyEmployees.Core.Domain.Exceptions;
using CompanyEmployees.Core.Domain.Repositories;
using MediatR;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Application.Handlers;

internal sealed class GetCompanyHandler(IRepositoryManager repository, IMapper mapper)
    : IRequestHandler<GetCompanyQuery, CompanyDto>
{
    public async Task<CompanyDto> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
    {
        var company = await repository.Company.GetCompanyAsync(request.Id, request.TrackChanges);
        if (company is null)
            throw new CompanyNotFoundException(request.Id);

        var companyDto = mapper.Map<CompanyDto>(company);

        return companyDto;
    }
}
