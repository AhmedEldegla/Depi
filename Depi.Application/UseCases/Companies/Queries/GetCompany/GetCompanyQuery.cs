using AutoMapper;
using MediatR;

using DEPI.Application.Repositories.Companies;
using DEPI.Application.UseCases.Companies.Contracts;

namespace DEPI.Application.UseCases.Companies.Queries.GetCompany;

public sealed record GetCompanyQuery(Guid CompanyId)
    : IRequest<CompanyResponse?>;

public sealed class GetCompanyQueryHandler
    : IRequestHandler<GetCompanyQuery, CompanyResponse?>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public GetCompanyQueryHandler(
        ICompanyRepository companyRepository,
        IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<CompanyResponse?> Handle(
        GetCompanyQuery request,
        CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetByIdAsync(
            request.CompanyId,
            cancellationToken);

        if (company is null)
        {
            return null;
        }

        return _mapper.Map<CompanyResponse>(company);
    }
}