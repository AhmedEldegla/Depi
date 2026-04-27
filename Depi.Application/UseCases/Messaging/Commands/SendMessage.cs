using Depi.Application.Repositories.Messaging;
using DEPI.Application.Common; // ده اللي هيشاور على الـ Interface اللي لسه عاملينه
using DEPI.Domain.Entities.Messaging;
using MediatR;

namespace DEPI.Application.UseCases.Messaging.Commands;

public record SendMessageCommand(Guid ConversationId, string Content, MessageType Type) : IRequest<Result<Guid>>;

public class SendMessageHandler : IRequestHandler<SendMessageCommand, Result<Guid>>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IConversationRepository _conversationRepository;
    private readonly ICurrentUserService _currentUser;

    public SendMessageHandler(
        IMessageRepository messageRepository,
        IConversationRepository conversationRepository,
        ICurrentUserService currentUser)
    {
        _messageRepository = messageRepository;
        _conversationRepository = conversationRepository;
        _currentUser = currentUser;
    }

    public async Task<Result<Guid>> Handle(SendMessageCommand request, CancellationToken ct)
    {
        var conversation = await _conversationRepository.GetByIdAsync(request.ConversationId);

        if (conversation == null)
        {
            return Result<Guid>.Failure(Errors.NotFound("المحادثة"), ErrorCode.NotFound);
        }

        // دلوقتي السطر ده مش هيدي Error لأن UserId بقى متعرف في الـ Interface
        var message = Message.Create(
            request.ConversationId,
            _currentUser.UserId,
            request.Content,
            request.Type);

        await _messageRepository.AddAsync(message);

        conversation.UpdateLastMessage();
        await _conversationRepository.UpdateAsync(conversation);

        return Result<Guid>.Success(message.Id);
    }
}