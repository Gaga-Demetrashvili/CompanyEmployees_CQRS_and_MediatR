using AutoMapper;
using CompanyEmployees.Application.Commands;
using CompanyEmployees.Core.Domain.Exceptions;
using CompanyEmployees.Core.Domain.Repositories;
using MediatR;

namespace CompanyEmployees.Application.Handlers;

internal sealed class DeleteCompanyHandler(IRepositoryManager repository, IMapper mapper)
    : IRequestHandler<DeleteCompanyCommand>
{
    public async Task Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await repository.Company.GetCompanyAsync(request.Id, request.TrackChanges);
        if (company is null)
            throw new CompanyNotFoundException(request.Id);

        repository.Company.DeleteCompany(company);
        await repository.SaveAsync();
    }
}
