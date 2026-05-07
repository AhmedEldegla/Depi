using DEPI.Application.Common;
using DEPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DEPI.Application.UseCases.Identity.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
{
    private readonly UserManager<User> _userManager;

    public ChangePasswordCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        if (string.IsNullOrWhiteSpace(request.CurrentPassword))
            throw new InvalidOperationException("كلمة المرور الحالية مطلوبة");

        if (string.IsNullOrWhiteSpace(request.NewPassword))
            throw new InvalidOperationException("كلمة المرور الجديدة مطلوبة");

        if (request.NewPassword.Length < 6)
            throw new InvalidOperationException("كلمة المرور يجب أن تكون 6 أحرف على الأقل");

        if (request.CurrentPassword == request.NewPassword)
            throw new InvalidOperationException("كلمة المرور الجديدة يجب أن تكون مختلفة عن الحالية");

        var user = await _userManager.FindByIdAsync(command.UserId.ToString());
        if (user == null)
            throw new InvalidOperationException("المستخدم غير موجود");

        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

        if (!result.Succeeded)
        {
            var errors = string.Join("، ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException(errors);
        }
    }
}
