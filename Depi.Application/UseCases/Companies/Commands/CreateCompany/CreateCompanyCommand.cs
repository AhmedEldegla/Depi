using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using DEPI.Application.Repositories.Companies;
using DEPI.Application.UseCases.Companies.Contracts;

namespace DEPI.Application.UseCases.Companies.Commands.CreateCompany;

public record CreateCompanyCommand(Guid OwnerId, CreateCompanyRequest Request)
    : IRequest<CompanyResponse>;

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CompanyResponse>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly ICompanyMemberRepository _memberRepository;
    private readonly IMapper _mapper;

    public CreateCompanyCommandHandler(
        ICompanyRepository companyRepository,
        ICompanyMemberRepository memberRepository,
        IMapper mapper)
    {
        _companyRepository = companyRepository;
        _memberRepository = memberRepository;
        _mapper = mapper;
    }

    public async Task<CompanyResponse> Handle(CreateCompanyCommand request, CancellationToken ct)
    {
        var company = new DEPI.Domain.Entities.Companies.Company
        {
            OwnerId = request.OwnerId,
            Name = Normalize(request.Request.Name),
            Description = Normalize(request.Request.Description),
            Website = Normalize(request.Request.Website, allowEmpty: true) ?? string.Empty,
            Location = Normalize(request.Request.Location, allowEmpty: true) ?? string.Empty,
            IsActive = true
        };

        await _companyRepository.AddAsync(company, ct);

        var member = new DEPI.Domain.Entities.Companies.CompanyMember
        {
            CompanyId = company.Id,
            UserId = request.OwnerId,
            Role = "Owner",
            JoinedAt = DateTime.UtcNow,
            IsActive = true
        };

        await _memberRepository.AddAsync(member, ct);

        return _mapper.Map<CompanyResponse>(company);
    }

    private static string Normalize(string? value, bool allowEmpty = false)
    {
        if (string.IsNullOrWhiteSpace(value))
            return allowEmpty ? null! : string.Empty;

        return value.Trim();
    }
}