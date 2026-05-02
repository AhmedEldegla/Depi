using AutoMapper;
using DEPI.Application.UseCases.Profiles.GetFeaturedPortfolio;
using DEPI.Application.DTOs.Profiles;
using DEPI.Application.Interfaces;
using MediatR;
public class GetFeaturedPortfolioQueryHandler : IRequestHandler<GetFeaturedPortfolioQuery, IEnumerable<PortfolioItemResponse>>
{
    private readonly IPortfolioItemRepository _repository;
    private readonly IMapper _mapper;
    public GetFeaturedPortfolioQueryHandler(IPortfolioItemRepository repository, IMapper mapper) { _repository = repository; _mapper = mapper; }
    public async Task<IEnumerable<PortfolioItemResponse>> Handle(GetFeaturedPortfolioQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetFeaturedAsync(cancellationToken);
        return _mapper.Map<IEnumerable<PortfolioItemResponse>>(items);
    }
}