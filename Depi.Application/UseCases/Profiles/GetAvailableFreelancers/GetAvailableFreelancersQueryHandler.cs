using AutoMapper;
using DEPI.Application.DTOs.Profiles;
using DEPI.Application.Interfaces;
using MediatR;

namespace DEPI.Application.UseCases.Profiles.GetAvailableFreelancers;

public class GetAvailableFreelancersQueryHandler : IRequestHandler<GetAvailableFreelancersQuery, (IEnumerable<UserProfileResponse> Items, int TotalCount)>
{
    private readonly IUserProfileRepository _repository;
    private readonly IMapper _mapper;

    public GetAvailableFreelancersQueryHandler(IUserProfileRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<(IEnumerable<UserProfileResponse> Items, int TotalCount)> Handle(GetAvailableFreelancersQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAvailableFreelancersAsync(request.Search, request.Page, request.PageSize, cancellationToken);
        var items = result.Items.Select(p => _mapper.Map<UserProfileResponse>(p));
        return (items, result.TotalCount);
    }
}