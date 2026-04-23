using DEPI.Application.DTOs.Identity;
using DEPI.Application.Interfaces;
using DEPI.Domain.Entities.Identity;
using MediatR;

namespace DEPI.Application.UseCases.Identity.Queries;

public record GetUserByRefreshTokenQuery(string RefreshToken) : IRequest<User?>;

public class GetUserByRefreshTokenQueryHandler : IRequestHandler<GetUserByRefreshTokenQuery, User?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByRefreshTokenQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> Handle(GetUserByRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetByRefreshTokenAsync(request.RefreshToken);
    }
}