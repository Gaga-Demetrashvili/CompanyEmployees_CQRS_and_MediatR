using CompanyEmployees.Application.Commands;
using CompanyEmployees.Application.Notifications;
using CompanyEmployees.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Infrastructure.Presentation.Controllers;

[Route("api/companies")]
[ApiController]
public class CompaniesController(ISender sender, IPublisher publisher) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCompanies()
    {
        var companies = await sender.Send(new GetCompaniesQuery(TrackChanges: false));

        return Ok(companies);
    }

    [HttpGet("{id:guid}", Name = "CompanyById")]
    public async Task<IActionResult> GetCompany(Guid id)
    {
        var company = await sender.Send(new GetCompanyQuery(id, TrackChanges: false));

        return Ok(company);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDto companyForCreationDto)
    {
        if (companyForCreationDto is null)
            return BadRequest("CompanyForCreationDto object is null");

        var company = await sender.Send(new CreateCompanyCommand(companyForCreationDto));

        return CreatedAtRoute("CompanyById", new { id = company.Id }, company);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCompany(Guid id, CompanyForUpdateDto companyForUpdateDto)
    {
        if (companyForUpdateDto is null)
            return BadRequest("CompanyForUpdateDto object is null");

        await sender.Send(new UpdateCompanyCommand(id, companyForUpdateDto, TrackChanges: true));

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCompany(Guid id)
    {
        await publisher.Publish(new CompanyDeletedNotification(id, TrackChanges: false));

        return NoContent();
    }
}