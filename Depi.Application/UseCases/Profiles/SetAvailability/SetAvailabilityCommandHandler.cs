using DEPI.Application.DTOs.Profiles;
using DEPI.Application.Interfaces;
using DEPI.Application.UseCases.Profiles.CreateUserProfile;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.SetAvailability;
public class SetAvailabilityCommandHandler : IRequestHandler<SetAvailabilityCommand, bool>
{
    private readonly IUserProfileRepository _profileRepository;
    public SetAvailabilityCommandHandler(IUserProfileRepository profileRepository) { _profileRepository = profileRepository; }
    public async Task<bool> Handle(SetAvailabilityCommand request, CancellationToken cancellationToken)
    {
        var profile = await _profileRepository.GetByIdAsync(request.ProfileId, cancellationToken)
            ?? throw new InvalidOperationException("الملف الشخصي غير موجود");
        profile.SetAvailability(request.IsAvailable);
        await _profileRepository.UpdateAsync(profile, cancellationToken);
        return true;
    }
}