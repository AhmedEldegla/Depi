using AutoMapper;
using DEPI.Application.UseCases.Profiles.CreatePortfolioItem;
using DEPI.Application.DTOs.Profiles;
using DEPI.Application.Interfaces;
using DEPI.Domain.Entities.Profiles;
using MediatR;
public class CreatePortfolioItemCommandHandler : IRequestHandler<CreatePortfolioItemCommand, PortfolioItemResponse>
{
    private readonly IPortfolioItemRepository _repository;
    private readonly IMapper _mapper;
    public CreatePortfolioItemCommandHandler(IPortfolioItemRepository repository, IMapper mapper) { _repository = repository; _mapper = mapper; }
    public async Task<PortfolioItemResponse> Handle(CreatePortfolioItemCommand request, CancellationToken cancellationToken)
    {
        var item = PortfolioItem.Create(request.UserId, request.Title, request.Description, request.Url, request.LiveUrl);
        await _repository.AddAsync(item, cancellationToken);
        return _mapper.Map<PortfolioItemResponse>(item);
    }
}