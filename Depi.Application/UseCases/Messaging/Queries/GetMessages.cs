using Depi.Application.Repositories.Messaging;
using DEPI.Application.Common;
using MediatR;

namespace DEPI.Application.UseCases.Messaging.Queries;

public record GetMessagesQuery(Guid ConversationId) : IRequest<Result<List<MessageResponse>>>;

public class GetMessagesHandler : IRequestHandler<GetMessagesQuery, Result<List<MessageResponse>>>
{
    private readonly IMessageRepository _messageRepository;

    public GetMessagesHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<Result<List<MessageResponse>>> Handle(GetMessagesQuery request, CancellationToken ct)
    {
        var messages = await _messageRepository.GetByConversationIdAsync(request.ConversationId);

        var response = messages.Select(m => new MessageResponse(
            m.Id,
            m.Content,
            m.SenderId,
            m.SentAt,
            m.Type.ToString())).ToList();

        return Result<List<MessageResponse>>.Success(response);
    }
}

public record MessageResponse(Guid Id, string Content, Guid SenderId, DateTime SentAt, string Type);