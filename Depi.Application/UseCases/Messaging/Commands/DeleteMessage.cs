using Depi.Application.Repositories.Messaging;
using DEPI.Application.Common;
using MediatR;

namespace DEPI.Application.UseCases.Messaging.Commands;

public record DeleteMessageCommand(Guid MessageId) : IRequest<Result<bool>>;

public class DeleteMessageHandler : IRequestHandler<DeleteMessageCommand, Result<bool>>
{
    private readonly IMessageRepository _messageRepository;
    private readonly ICurrentUserService _currentUser;

    public DeleteMessageHandler(IMessageRepository messageRepository, ICurrentUserService currentUser)
    {
        _messageRepository = messageRepository;
        _currentUser = currentUser;
    }

    public async Task<Result<bool>> Handle(DeleteMessageCommand request, CancellationToken ct)
    {
        var message = await _messageRepository.GetByIdAsync(request.MessageId);

        if (message == null)
            return Result<bool>.Failure(Errors.NotFound("الرسالة"), ErrorCode.NotFound);

        if (message.SenderId != _currentUser.UserId)
            return Result<bool>.Failure(Errors.Unauthorized(), ErrorCode.Unauthorized);

        // تعديل: نرسل الـ Id فقط للـ Repository
        await _messageRepository.DeleteAsync(message.Id);
        return Result<bool>.Success(true);
    }
}