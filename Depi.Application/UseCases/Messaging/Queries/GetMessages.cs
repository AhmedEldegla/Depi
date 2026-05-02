using AutoMapper;
using Depi.Application.Repositories.Messaging;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Messaging;
using MediatR;

namespace DEPI.Application.UseCases.Messaging.Queries;

public record GetMessagesQuery(Guid ConversationId) : IRequest<Result<List<MessageResponse>>>;

public class GetMessagesHandler : IRequestHandler<GetMessagesQuery, Result<List<MessageResponse>>>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IMapper _mapper;

    public GetMessagesHandler(IMessageRepository messageRepository, IMapper mapper)
    {
        _messageRepository = messageRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<MessageResponse>>> Handle(GetMessagesQuery request, CancellationToken ct)
    {
        var messages = await _messageRepository.GetByConversationIdAsync(request.ConversationId);

        var response = _mapper.Map<List<MessageResponse>>(messages);

        return Result<List<MessageResponse>>.Success(response);
    }
}