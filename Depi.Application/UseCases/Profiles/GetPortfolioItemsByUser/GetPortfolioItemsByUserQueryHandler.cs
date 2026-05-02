using AutoMapper;
using DEPI.Application.UseCases.Profiles.GetPortfolioItemsByUser;
using DEPI.Application.DTOs.Profiles;
using DEPI.Application.Interfaces;
using MediatR;
public class GetPortfolioItemsByUserQueryHandler : IRequestHandler<GetPortfolioItemsByUserQuery, IEnumerable<PortfolioItemResponse>>
{
    private readonly IPortfolioItemRepository _repository;
    private readonly IMapper _mapper;
    public GetPortfolioItemsByUserQueryHandler(IPortfolioItemRepository repository, IMapper mapper) { _repository = repository; _mapper = mapper; }
    public async Task<IEnumerable<PortfolioItemResponse>> Handle(GetPortfolioItemsByUserQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetByUserIdAsync(request.UserId, cancellationToken);
        return _mapper.Map<IEnumerable<PortfolioItemResponse>>(items);
    }
}