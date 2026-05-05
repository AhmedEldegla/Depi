using DEPI.Application.Common;
using DEPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DEPI.Application.UseCases.Identity.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
{
    private readonly UserManager<User> _userManager;

    public ResetPasswordCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        if (string.IsNullOrWhiteSpace(request.Token))
            throw new InvalidOperationException("رمز إعادة التعيين مطلوب");

        if (string.IsNullOrWhiteSpace(request.NewPassword))
            throw new InvalidOperationException("كلمة المرور الجديدة مطلوبة");

        if (request.NewPassword.Length < 6)
            throw new InvalidOperationException("كلمة المرور يجب أن تكون 6 أحرف على الأقل");

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            throw new InvalidOperationException("المستخدم غير موجود");

        var decodedToken = Uri.UnescapeDataString(request.Token);
        var result = await _userManager.ResetPasswordAsync(user, decodedToken, request.NewPassword);

        if (!result.Succeeded)
        {
            var errors = string.Join("، ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException(errors);
        }
    }
}
