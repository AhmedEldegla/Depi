using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Messaging;
using DEPI.Application.Interfaces;
using DEPI.Domain.Entities.Messaging;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DEPI.Application.UseCases.Messaging.Commands;

public record CreateConversationCommand(Guid CurrentUserId, CreateConversationRequest Request) : IRequest<ConversationResponse>;

public class CreateConversationCommandHandler : IRequestHandler<CreateConversationCommand, ConversationResponse>
{
    private readonly IConversationRepository _conversationRepository;
    private readonly UserManager<DEPI.Domain.Entities.Identity.User> _userManager;
    private readonly IMapper _mapper;

    public CreateConversationCommandHandler(
        IConversationRepository conversationRepository,
        UserManager<DEPI.Domain.Entities.Identity.User> userManager,
        IMapper mapper)
    {
        _conversationRepository = conversationRepository;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<ConversationResponse> Handle(CreateConversationCommand request, CancellationToken cancellationToken)
    {
        Conversation conversation;

        if (request.Request.IsGroup)
        {
            if (string.IsNullOrWhiteSpace(request.Request.Title))
                throw new ArgumentException(Errors.Required("Conversation title"));

            conversation = Conversation.CreateGroup(request.Request.Title, request.CurrentUserId, request.Request.ProjectId);
        }
        else
        {
            var existingConversation = await _conversationRepository.GetByParticipantsAsync(
                request.CurrentUserId, request.Request.UserId);

            if (existingConversation != null)
                throw new InvalidOperationException(Errors.AlreadyExists("Conversation"));

            conversation = Conversation.CreateDirect(request.CurrentUserId, request.Request.UserId, request.Request.ProjectId);
        }

        await _conversationRepository.AddAsync(conversation);

        return _mapper.Map<ConversationResponse>(conversation);
    }
}

public record SendMessageCommand(Guid CurrentUserId, SendMessageRequest Request) : IRequest<MessageResponse>;

public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, MessageResponse>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IConversationRepository _conversationRepository;
    private readonly IMapper _mapper;

    public SendMessageCommandHandler(IMessageRepository messageRepository, IConversationRepository conversationRepository, IMapper mapper)
    {
        _messageRepository = messageRepository;
        _conversationRepository = conversationRepository;
        _mapper = mapper;
    }

    public async Task<MessageResponse> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var conversation = await _conversationRepository.GetWithParticipantsAsync(request.Request.ConversationId)
            ?? throw new KeyNotFoundException(Errors.NotFound("Conversation"));

        var isParticipant = conversation.Participants.Any(p => p.UserId == request.CurrentUserId);
        if (!isParticipant)
            throw new UnauthorizedAccessException(Errors.Forbidden());

        var message = Message.Create(
            conversationId: request.Request.ConversationId,
            senderId: request.CurrentUserId,
            content: request.Request.Content,
            type: request.Request.Type
        );

        await _messageRepository.AddAsync(message);

        conversation.UpdateLastMessage();
        await _conversationRepository.UpdateAsync(conversation);

        return _mapper.Map<MessageResponse>(message);
    }
}

public record MarkMessageReadCommand(Guid MessageId, Guid CurrentUserId) : IRequest<Unit>;

public class MarkMessageReadCommandHandler : IRequestHandler<MarkMessageReadCommand, Unit>
{
    private readonly IMessageRepository _messageRepository;

    public MarkMessageReadCommandHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<Unit> Handle(MarkMessageReadCommand request, CancellationToken cancellationToken)
    {
        var message = await _messageRepository.GetByIdAsync(request.MessageId)
            ?? throw new KeyNotFoundException(Errors.NotFound("Message"));

        message.MarkAsRead();
        await _messageRepository.UpdateAsync(message);

        return Unit.Value;
    }
}

public record MarkConversationReadCommand(Guid ConversationId, Guid CurrentUserId) : IRequest<Unit>;

public class MarkConversationReadCommandHandler : IRequestHandler<MarkConversationReadCommand, Unit>
{
    private readonly IConversationRepository _conversationRepository;

    public MarkConversationReadCommandHandler(IConversationRepository conversationRepository)
    {
        _conversationRepository = conversationRepository;
    }

    public async Task<Unit> Handle(MarkConversationReadCommand request, CancellationToken cancellationToken)
    {
        var conversation = await _conversationRepository.GetWithParticipantsAsync(request.ConversationId)
            ?? throw new KeyNotFoundException(Errors.NotFound("Conversation"));

        var participant = conversation.Participants.FirstOrDefault(p => p.UserId == request.CurrentUserId);
        if (participant != null)
        {
            participant.MarkAsRead();
            await _conversationRepository.UpdateAsync(conversation);
        }

        return Unit.Value;
    }
}