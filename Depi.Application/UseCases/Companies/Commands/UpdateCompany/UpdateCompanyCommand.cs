using AutoMapper;
using DEPI.Application.Repositories.Companies;
using DEPI.Application.UseCases.Companies.Contracts;
using MediatR;

namespace DEPI.Application.UseCases.Companies.Commands.UpdateCompany;

public sealed record UpdateCompanyCommand(
    Guid CompanyId,
    Guid UserId,
    UpdateCompanyRequest Request)
    : IRequest<CompanyResponse>;

public sealed class UpdateCompanyCommandHandler
    : IRequestHandler<UpdateCompanyCommand, CompanyResponse>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public UpdateCompanyCommandHandler(
        ICompanyRepository companyRepository,
        IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<CompanyResponse> Handle(
        UpdateCompanyCommand request,
        CancellationToken ct)
    {
        var company = await _companyRepository.GetByIdAsync(request.CompanyId, ct)
            ?? throw new KeyNotFoundException("Company not found");

        if (company.OwnerId != request.UserId)
            throw new UnauthorizedAccessException("Only the company owner can update this company");

        company.UpdateInfo(
            Normalize(request.Request.Name),
            Normalize(request.Request.Description),
            Normalize(request.Request.Website),
            Normalize(request.Request.Size, "1-10"),
            Normalize(request.Request.Location)
        );

        await _companyRepository.UpdateAsync(company, ct);

        return _mapper.Map<CompanyResponse>(company);
    }

    private static string Normalize(string? value, string defaultValue = "")
        => string.IsNullOrWhiteSpace(value) ? defaultValue : value.Trim();
}