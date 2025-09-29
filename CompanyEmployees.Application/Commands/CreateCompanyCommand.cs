using MediatR;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Application.Commands;

public sealed record CreateCompanyCommand(CompanyForCreationDto company) : IRequest<CompanyDto>;