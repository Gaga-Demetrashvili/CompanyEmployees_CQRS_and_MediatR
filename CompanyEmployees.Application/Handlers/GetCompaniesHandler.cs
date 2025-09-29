using AutoMapper;
using CompanyEmployees.Application.Queries;
using CompanyEmployees.Core.Domain.Repositories;
using MediatR;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Application.Handlers;

internal sealed class GetCompaniesHandler(IRepositoryManager repository, IMapper mapper) 
    : IRequestHandler<GetCompaniesQuery, IEnumerable<CompanyDto>>
{
    public async Task<IEnumerable<CompanyDto>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
    {
        var companies = await repository.Company.GetAllCompaniesAsync(request.TrackChanges);

        var companiesDto = mapper.Map<IEnumerable<CompanyDto>>(companies);

        return companiesDto;
    }
}
