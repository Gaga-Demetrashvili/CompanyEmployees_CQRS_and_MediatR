using AutoMapper;
using CompanyEmployees.Application.Notifications;
using CompanyEmployees.Core.Domain.Exceptions;
using CompanyEmployees.Core.Domain.Repositories;
using MediatR;

namespace CompanyEmployees.Application.Handlers;

internal sealed class DeleteCompanyHandler(IRepositoryManager repository, IMapper mapper)
    : INotificationHandler<CompanyDeletedNotification>
{
    public async Task Handle(CompanyDeletedNotification notification, CancellationToken cancellationToken)
    {
        var company = await repository.Company.GetCompanyAsync(notification.Id, notification.TrackChanges);
        if (company is null)
            throw new CompanyNotFoundException(notification.Id);

        repository.Company.DeleteCompany(company);
        await repository.SaveAsync();
    }
}
