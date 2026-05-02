using Depi.Application.Repositories.Messaging;
using DEPI.Application.Common;
using MediatR;

namespace DEPI.Application.UseCases.Messaging.Commands;

public record CreateConversationCommand(string? Title, bool IsGroup) : IRequest<Result<Guid>>;

public class CreateConversationHandler : IRequestHandler<CreateConversationCommand, Result<Guid>>
{
    private readonly IConversationRepository _conversationRepository;

    public CreateConversationHandler(IConversationRepository conversationRepository)
    {
        _conversationRepository = conversationRepository;
    }

    public async Task<Result<Guid>> Handle(CreateConversationCommand request, CancellationToken ct)
    {
        var conversation = DEPI.Domain.Entities.Messaging.Conversation.Create(request.Title, request.IsGroup);

        await _conversationRepository.AddAsync(conversation);
        return Result<Guid>.Success(conversation.Id);
    }
}