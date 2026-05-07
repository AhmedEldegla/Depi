using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Profiles;
using DEPI.Application.Interfaces;
using MediatR;

namespace DEPI.Application.UseCases.Profiles.SetUserProfileAvailability;

public class SetUserProfileAvailabilityCommandHandler : IRequestHandler<SetUserProfileAvailabilityCommand, Result<UserProfileResponse>>
{
    private readonly IUserProfileRepository _profileRepository;
    private readonly IMapper _mapper;

    public SetUserProfileAvailabilityCommandHandler(IUserProfileRepository profileRepository, IMapper mapper)
    {
        _profileRepository = profileRepository;
        _mapper = mapper;
    }

    public async Task<Result<UserProfileResponse>> Handle(SetUserProfileAvailabilityCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var profile = await _profileRepository.GetByUserIdAsync(request.UserId, cancellationToken);
            if (profile == null) return Result<UserProfileResponse>.Failure("الملف الشخصي غير موجود", ErrorCode.NotFound);

            profile.SetAvailability(request.IsAvailable);
            profile.SetUpdater(request.UserId);

            await _profileRepository.UpdateAsync(profile, cancellationToken);
            return Result<UserProfileResponse>.Success(_mapper.Map<UserProfileResponse>(profile));
        }
        catch (ArgumentException ex) { return Result<UserProfileResponse>.Failure(ex.Message, ErrorCode.ValidationError); }
        catch (Exception) { return Result<UserProfileResponse>.Failure(Errors.Internal(), ErrorCode.InternalError); }
    }
}