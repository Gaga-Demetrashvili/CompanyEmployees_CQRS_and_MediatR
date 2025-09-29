using MediatR;

namespace CompanyEmployees.Application.Commands;

public sealed record DeleteCompanyCommand(Guid Id, bool TrackChanges) : IRequest;
