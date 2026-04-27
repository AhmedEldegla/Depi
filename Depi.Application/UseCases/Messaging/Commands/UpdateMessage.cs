using Depi.Application.Repositories.Messaging;
using DEPI.Application.Common;
using MediatR;

namespace DEPI.Application.UseCases.Messaging.Commands;

public record UpdateMessageCommand(Guid MessageId, string NewContent) : IRequest<Result<bool>>;

public class UpdateMessageHandler : IRequestHandler<UpdateMessageCommand, Result<bool>>
{
    private readonly IMessageRepository _messageRepository;
    private readonly ICurrentUserService _currentUser;

    public UpdateMessageHandler(IMessageRepository messageRepository, ICurrentUserService currentUser)
    {
        _messageRepository = messageRepository;
        _currentUser = currentUser;
    }

    public async Task<Result<bool>> Handle(UpdateMessageCommand request, CancellationToken ct)
    {
        var message = await _messageRepository.GetByIdAsync(request.MessageId);

        if (message == null)
            return Result<bool>.Failure(Errors.NotFound("الرسالة"), ErrorCode.NotFound);

        // التأكد إن اللي بيعدل هو صاحب الرسالة
        if (message.SenderId != _currentUser.UserId)
            return Result<bool>.Failure(Errors.Unauthorized("لا تملك صلاحية التعديل"), ErrorCode.Unauthorized);

        message.UpdateContent(request.NewContent); // ميثود في الدومين
        await _messageRepository.UpdateAsync(message);

        return Result<bool>.Success(true);
    }
}