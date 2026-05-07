using DEPI.Application.Common;
using DEPI.Application.Interfaces;
using DEPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DEPI.Application.UseCases.Identity.ForgotPassword;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;
    private readonly ILogger<ForgotPasswordCommandHandler> _logger;

    public ForgotPasswordCommandHandler(
        UserManager<User> userManager,
        IEmailService emailService,
        ILogger<ForgotPasswordCommandHandler> logger)
    {
        _userManager = userManager;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task Handle(ForgotPasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(command.Request.Email);

        if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
        {
            _logger.LogWarning("Password reset requested for non-existent or unconfirmed email: {Email}", command.Request.Email);
            return;
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var encodedToken = Uri.EscapeDataString(token);
        var resetLink = $"https://localhost:3000/reset-password?email={command.Request.Email}&token={encodedToken}";

        var subject = "إعادة تعيين كلمة المرور - DEPI";
        var body = $@"
            <h2>طلب إعادة تعيين كلمة المرور</h2>
            <p>مرحباً {user.FullName}،</p>
            <p>تم استلام طلب لإعادة تعيين كلمة المرور الخاصة بحسابك.</p>
            <p>لإعادة تعيين كلمة المرور، اضغط على الرابط التالي:</p>
            <p><a href='{resetLink}'>إعادة تعيين كلمة المرور</a></p>
            <p>إذا لم تطلب إعادة تعيين كلمة المرور، يرجى تجاهل هذا البريد الإلكتروني.</p>
            <hr/>
            <p><strong>رمز إعادة التعيين (للاستخدام اليدوي):</strong> {token}</p>
        ";

        await _emailService.SendAsync(command.Request.Email, subject, body);
    }
}
