using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using DEPI.Application.Repositories.Companies;

namespace DEPI.Application.UseCases.Companies.Commands.DeleteCompany;

public record DeleteCompanyCommand(Guid CompanyId, Guid UserId) : IRequest;

public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
{
    private readonly ICompanyRepository _companyRepository;

    public DeleteCompanyCommandHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task Handle(DeleteCompanyCommand request, CancellationToken ct)
    {
        var company = await _companyRepository.GetByIdAsync(request.CompanyId, ct)
            ?? throw new KeyNotFoundException("Company not found");

        if (company.OwnerId != request.UserId)
            throw new UnauthorizedAccessException("Only the company owner can delete this company.");

        await _companyRepository.DeleteAsync(request.CompanyId, ct);
    }
}