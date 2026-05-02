using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Profiles;
using DEPI.Application.Interfaces;
using MediatR;

namespace DEPI.Application.UseCases.Profiles.UpdateUserProfile;

public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, Result<UserProfileResponse>>
{
    private readonly IUserProfileRepository _profileRepository;
    private readonly IMapper _mapper;

    public UpdateUserProfileCommandHandler(IUserProfileRepository profileRepository, IMapper mapper)
    {
        _profileRepository = profileRepository;
        _mapper = mapper;
    }

    public async Task<Result<UserProfileResponse>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var profile = await _profileRepository.GetByIdAsync(request.ProfileId, cancellationToken);
        if (profile == null)
            return Result<UserProfileResponse>.Failure("الملف الشخصي غير موجود", ErrorCode.NotFound);

        profile.UpdateInfo(request.DisplayName, request.Title, request.Bio);
        if (request.CountryId.HasValue)
            profile.SetLocation(request.CountryId, request.Address);
        profile.SetLinks(request.LinkedInUrl, request.PortfolioUrl, request.GithubUrl, request.WebsiteUrl);

        await _profileRepository.UpdateAsync(profile, cancellationToken);
        return Result<UserProfileResponse>.Success(_mapper.Map<UserProfileResponse>(profile));
    }
}