using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Identity;
using DEPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DEPI.Application.UseCases.Identity.GetCurrentUser;

public record GetCurrentUserQuery(Guid UserId) : IRequest<UserResponse>;

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public GetCurrentUserQueryHandler(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<UserResponse> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString())
            ?? throw new InvalidOperationException(Errors.NotFound("User"));

        var roles = await _userManager.GetRolesAsync(user);

        var response = _mapper.Map<UserResponse>(user);
        response.Roles = roles.ToList();

        return response;
    }
}
