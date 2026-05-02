using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Profiles;
using DEPI.Application.Interfaces;
using DEPI.Domain.Entities.Profiles;
using FluentValidation;
using MediatR;

namespace DEPI.Application.UseCases.Profiles.CreateUserProfile;

public class CreateUserProfileCommandValidator : AbstractValidator<CreateUserProfileCommand>
{
    public CreateUserProfileCommandValidator()
    {
        RuleFor(x => x.DisplayName).NotEmpty().WithMessage("الاسم مطلوب").MaximumLength(100).WithMessage("الاسم يجب ألا يتجاوز 100 حرف");
        RuleFor(x => x.Title).NotEmpty().WithMessage("المسمى مطلوب").MaximumLength(200).WithMessage("المسمى يجب ألا يتجاوز 200 حرف");
        RuleFor(x => x.Bio).NotEmpty().WithMessage("النبذة مطلوبة").MaximumLength(1000).WithMessage("النبذة يجب ألا تتجاوز 1000 حرف");
        RuleFor(x => x.HourlyRate).GreaterThanOrEqualTo(0).WithMessage("السعر يجب أن يكون إيجابياً");
    }
}

public class CreateUserProfileCommandHandler : IRequestHandler<CreateUserProfileCommand, Result<UserProfileResponse>>
{
    private readonly IUserProfileRepository _profileRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateUserProfileCommandHandler(
        IUserProfileRepository profileRepository,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _profileRepository = profileRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<UserProfileResponse>> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _profileRepository.ExistsByUserIdAsync(request.UserId, cancellationToken);
            if (exists) return Result<UserProfileResponse>.Failure("ملفك الشخصي موجود بالفعل", ErrorCode.ValidationError);

            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null) return Result<UserProfileResponse>.Failure(Errors.NotFound("User"), ErrorCode.NotFound);

            var profile = UserProfile.Create(request.UserId, request.DisplayName, request.Title, request.Bio, request.HourlyRate);
            profile.SetCreator(request.UserId);

            await _profileRepository.AddAsync(profile, cancellationToken);
            return Result<UserProfileResponse>.Success(_mapper.Map<UserProfileResponse>(profile));
        }
        catch (ArgumentException ex) { return Result<UserProfileResponse>.Failure(ex.Message, ErrorCode.ValidationError); }
        catch (Exception) { return Result<UserProfileResponse>.Failure(Errors.Internal(), ErrorCode.InternalError); }
    }
}