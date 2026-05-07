using AutoMapper;
using DEPI.Application.UseCases.Profiles.GetUserProfileByUserId;
using DEPI.Application.DTOs.Profiles;
using DEPI.Application.Interfaces;
using DEPI.Application.UseCases.Profiles.CreateUserProfile;
using MediatR;
public class GetUserProfileByUserIdQueryHandler : IRequestHandler<GetUserProfileByUserIdQuery, UserProfileResponse?>
{
    private readonly IUserProfileRepository _repository;
    private readonly IMapper _mapper;
    public GetUserProfileByUserIdQueryHandler(IUserProfileRepository repository, IMapper mapper) { _repository = repository; _mapper = mapper; }
    public async Task<UserProfileResponse?> Handle(GetUserProfileByUserIdQuery request, CancellationToken cancellationToken)
    {
        var profile = await _repository.GetByUserIdAsync(request.UserId, cancellationToken);
        return profile != null ? _mapper.Map<UserProfileResponse>(profile) : null;
    }
}