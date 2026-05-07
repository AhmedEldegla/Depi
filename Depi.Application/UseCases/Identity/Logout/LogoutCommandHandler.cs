using DEPI.Application.Common;
using DEPI.Application.DTOs.Identity;
using DEPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DEPI.Application.UseCases.Identity.Logout;

public record LogoutCommand(Guid UserId) : IRequest;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
{
    private readonly UserManager<User> _userManager;

    public LogoutCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString())
            ?? throw new InvalidOperationException(Errors.NotFound("User"));

        user.InvalidateRefreshToken();
        await _userManager.UpdateAsync(user);
    }
}
