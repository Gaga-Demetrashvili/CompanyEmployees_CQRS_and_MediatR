using AutoMapper;
using CompanyEmployees.Application.Commands;
using CompanyEmployees.Core.Domain.Exceptions;
using CompanyEmployees.Core.Domain.Repositories;
using MediatR;

namespace CompanyEmployees.Application.Handlers;

internal sealed class UpdateCompanyHandler(IRepositoryManager repository, IMapper mapper) :
    IRequestHandler<UpdateCompanyCommand>
{
    public async Task Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var companyEntity = await repository.Company.GetCompanyAsync(request.Id, request.TrackChanges);
        if (companyEntity is null)
            throw new CompanyNotFoundException(request.Id);

        mapper.Map(request.Company, companyEntity); 
        await repository.SaveAsync();
    }
}
