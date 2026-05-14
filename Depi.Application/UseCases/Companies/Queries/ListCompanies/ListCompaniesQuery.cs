using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using DEPI.Application.Repositories.Companies;
using DEPI.Application.UseCases.Companies.Contracts;
using DEPI.Application.UseCases.Companies;

namespace Depi.Application.UseCases.Companies.Queries.ListCompanies;

public record ListCompaniesQuery(string? SearchTerm, bool? VerifiedOnly) : IRequest<List<CompanyResponse>>;

public class ListCompaniesQueryHandler : IRequestHandler<ListCompaniesQuery, List<CompanyResponse>>
{
    private readonly ICompanyRepository _repo;
    private readonly IMapper _mapper;

    public ListCompaniesQueryHandler(ICompanyRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<List<CompanyResponse>> Handle(ListCompaniesQuery r, CancellationToken ct)
    {
        List<DEPI.Domain.Entities.Companies.Company> companies;

        if (!string.IsNullOrWhiteSpace(r.SearchTerm))
            companies = await _repo.SearchCompaniesAsync(r.SearchTerm);
        else if (r.VerifiedOnly == true)
            companies = await _repo.GetVerifiedCompaniesAsync();
        else
        {
            var result = await _repo.GetAllAsync();
            companies = result.ToList();
        }

        return _mapper.Map<List<CompanyResponse>>(companies);
    }
}