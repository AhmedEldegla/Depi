using AutoMapper;
using DEPI.Application.DTOs.Profiles;
using DEPI.Application.Interfaces;
using MediatR;

namespace DEPI.Application.UseCases.Profiles.UpdatePortfolioItem;

public class UpdatePortfolioItemCommandHandler : IRequestHandler<UpdatePortfolioItemCommand, PortfolioItemResponse>
{
    private readonly IPortfolioItemRepository _repository;
    private readonly IMapper _mapper;

    public UpdatePortfolioItemCommandHandler(IPortfolioItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PortfolioItemResponse> Handle(UpdatePortfolioItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("عنصر المحفظة غير موجود");

        item.UpdateInfo(request.Title, request.Description, request.Url, request.LiveUrl);
        await _repository.UpdateAsync(item, cancellationToken);

        return _mapper.Map<PortfolioItemResponse>(item);
    }
}