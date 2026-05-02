using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Profiles;
using DEPI.Application.Interfaces;
using MediatR;

namespace DEPI.Application.UseCases.Profiles.SetUserProfileHourlyRate;

public class SetUserProfileHourlyRateCommandHandler : IRequestHandler<SetUserProfileHourlyRateCommand, Result<UserProfileResponse>>
{
    private readonly IUserProfileRepository _profileRepository;
    private readonly IMapper _mapper;

    public SetUserProfileHourlyRateCommandHandler(IUserProfileRepository profileRepository, IMapper mapper)
    {
        _profileRepository = profileRepository;
        _mapper = mapper;
    }

    public async Task<Result<UserProfileResponse>> Handle(SetUserProfileHourlyRateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var profile = await _profileRepository.GetByUserIdAsync(request.UserId, cancellationToken);
            if (profile == null) return Result<UserProfileResponse>.Failure("الملف الشخصي غير موجود", ErrorCode.NotFound);

            profile.SetHourlyRate(request.Rate, request.CurrencyId);
            profile.SetUpdater(request.UserId);

            await _profileRepository.UpdateAsync(profile, cancellationToken);
            return Result<UserProfileResponse>.Success(_mapper.Map<UserProfileResponse>(profile));
        }
        catch (ArgumentException ex) { return Result<UserProfileResponse>.Failure(ex.Message, ErrorCode.ValidationError); }
        catch (Exception) { return Result<UserProfileResponse>.Failure(Errors.Internal(), ErrorCode.InternalError); }
    }
}