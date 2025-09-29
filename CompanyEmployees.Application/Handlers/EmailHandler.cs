using CompanyEmployees.Application.Notifications;
using LoggingService;
using MediatR;

namespace CompanyEmployees.Application.Handlers;

internal sealed class EmailHandler(ILoggerManager logger)
    : INotificationHandler<CompanyDeletedNotification>
{
    public async Task Handle(CompanyDeletedNotification notification, CancellationToken cancellationToken)
    {
        logger.LogWarning($"Delete action for the company with id: {notification.Id} has occurred.");

        await Task.CompletedTask;
    }
}
