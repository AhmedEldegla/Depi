using Depi.Application.Repositories.Messaging;
using DEPI.Application.Common;
using MediatR;

namespace DEPI.Application.UseCases.Messaging.Queries;

public record GetConversationsQuery : IRequest<Result<List<ConversationResponse>>>;

public class GetConversationsHandler : IRequestHandler<GetConversationsQuery, Result<List<ConversationResponse>>>
{
    private readonly IConversationRepository _conversationRepository;
    private readonly ICurrentUserService _currentUser;

    public GetConversationsHandler(IConversationRepository conversationRepository, ICurrentUserService currentUser)
    {
        _conversationRepository = conversationRepository;
        _currentUser = currentUser;
    }

    public async Task<Result<List<ConversationResponse>>> Handle(GetConversationsQuery request, CancellationToken ct)
    {
        var conversations = await _conversationRepository.GetUserConversationsAsync(_currentUser.UserId, ct);

        var response = conversations.Select(c => new ConversationResponse(
            c.Id,
            c.Title ?? "محادثة",
            (DateTime)c.LastMessageAt, // تأكد أن DTO يقبل DateTime? أو استخدم .Value
            c.IsGroup
        )).ToList();

        return Result<List<ConversationResponse>>.Success(response);
    }
}

public record ConversationResponse(Guid Id, string Title, DateTime LastMessageAt, bool IsGroup);