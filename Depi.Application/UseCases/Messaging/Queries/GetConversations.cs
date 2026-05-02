using AutoMapper;
using Depi.Application.Repositories.Messaging;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Messaging;
using MediatR;

namespace DEPI.Application.UseCases.Messaging.Queries;

public record GetConversationsQuery : IRequest<Result<List<ConversationResponse>>>;

public class GetConversationsHandler : IRequestHandler<GetConversationsQuery, Result<List<ConversationResponse>>>
{
    private readonly IConversationRepository _conversationRepository;
    private readonly ICurrentUserService _currentUser;
    private readonly IMapper _mapper;

    public GetConversationsHandler(
        IConversationRepository conversationRepository,
        ICurrentUserService currentUser,
        IMapper mapper)
    {
        _conversationRepository = conversationRepository;
        _currentUser = currentUser;
        _mapper = mapper;
    }

    public async Task<Result<List<ConversationResponse>>> Handle(GetConversationsQuery request, CancellationToken ct)
    {
        var conversations = await _conversationRepository.GetUserConversationsAsync(_currentUser.UserId, ct);

        var response = _mapper.Map<List<ConversationResponse>>(conversations);

        return Result<List<ConversationResponse>>.Success(response);
    }
}