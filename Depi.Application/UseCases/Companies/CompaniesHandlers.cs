using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.Repositories.Companies;
using DEPI.Domain.Entities.Companies;
using MediatR;

namespace DEPI.Application.UseCases.Companies;

public class CompanyResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public bool IsVerified { get; set; }
    public decimal Rating { get; set; }
    public int TotalProjects { get; set; }
    public int TeamSize { get; set; }
    public DateTime CreatedAt { get; set; }
}

public record CreateCompanyRequest(string Name, string Description, string? Website, string? Size, string? Location);
public record UpdateCompanyRequest(string Name, string Description, string? Website, string? Size, string? Location);

public record CreateCompanyCommand(Guid OwnerId, CreateCompanyRequest Request) : IRequest<CompanyResponse>;
public record UpdateCompanyCommand(Guid CompanyId, Guid UserId, UpdateCompanyRequest Request) : IRequest<CompanyResponse>;
public record DeleteCompanyCommand(Guid CompanyId, Guid UserId) : IRequest;
public record GetCompanyQuery(Guid CompanyId) : IRequest<CompanyResponse?>;
public record ListCompaniesQuery(string? SearchTerm, bool? VerifiedOnly, int Page, int PageSize) : IRequest<List<CompanyResponse>>;

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CompanyResponse>
{
    private readonly ICompanyRepository _repo;
    private readonly IMapper _mapper;
    public CreateCompanyCommandHandler(ICompanyRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }

    public async Task<CompanyResponse> Handle(CreateCompanyCommand r, CancellationToken ct)
    {
        var company = new Company { OwnerId = r.OwnerId, Name = r.Request.Name, Description = r.Request.Description, Website = r.Request.Website ?? "", Size = r.Request.Size ?? "1-10", Location = r.Request.Location ?? "" };
        await _repo.AddAsync(company, ct);
        return _mapper.Map<CompanyResponse>(company);
    }
}

public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, CompanyResponse>
{
    private readonly ICompanyRepository _repo;
    private readonly IMapper _mapper;
    public UpdateCompanyCommandHandler(ICompanyRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }

    public async Task<CompanyResponse> Handle(UpdateCompanyCommand r, CancellationToken ct)
    {
        var company = await _repo.GetByIdAsync(r.CompanyId, ct) ?? throw new KeyNotFoundException(Errors.NotFound("Company"));
        if (company.OwnerId != r.UserId) throw new UnauthorizedAccessException(Errors.Forbidden());
        company.UpdateInfo(r.Request.Name, r.Request.Description, r.Request.Website ?? "", r.Request.Size ?? "1-10", r.Request.Location ?? "");
        await _repo.UpdateAsync(company, ct);
        return _mapper.Map<CompanyResponse>(company);
    }
}

public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
{
    private readonly ICompanyRepository _repo;
    public DeleteCompanyCommandHandler(ICompanyRepository repo) => _repo = repo;

    public async Task Handle(DeleteCompanyCommand r, CancellationToken ct)
    {
        var company = await _repo.GetByIdAsync(r.CompanyId, ct) ?? throw new KeyNotFoundException(Errors.NotFound("Company"));
        if (company.OwnerId != r.UserId) throw new UnauthorizedAccessException(Errors.Forbidden());
        await _repo.DeleteAsync(r.CompanyId, ct);
    }
}

public class GetCompanyQueryHandler : IRequestHandler<GetCompanyQuery, CompanyResponse?>
{
    private readonly ICompanyRepository _repo;
    private readonly IMapper _mapper;
    public GetCompanyQueryHandler(ICompanyRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }

    public async Task<CompanyResponse?> Handle(GetCompanyQuery r, CancellationToken ct)
    {
        var c = await _repo.GetByIdAsync(r.CompanyId, ct);
        return c == null ? null : _mapper.Map<CompanyResponse>(c);
    }
}

public class ListCompaniesQueryHandler : IRequestHandler<ListCompaniesQuery, List<CompanyResponse>>
{
    private readonly ICompanyRepository _repo;
    private readonly IMapper _mapper;
    public ListCompaniesQueryHandler(ICompanyRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }

    public async Task<List<CompanyResponse>> Handle(ListCompaniesQuery r, CancellationToken ct)
    {
        List<Company> companies;
        if (!string.IsNullOrWhiteSpace(r.SearchTerm)) companies = await _repo.SearchCompaniesAsync(r.SearchTerm);
        else if (r.VerifiedOnly == true) companies = await _repo.GetVerifiedCompaniesAsync();
        else companies = (await _repo.GetAllAsync(ct)).ToList();
        return _mapper.Map<List<CompanyResponse>>(companies);
    }
}
