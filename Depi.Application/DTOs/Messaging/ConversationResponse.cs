namespace DEPI.Application.DTOs.Messaging;

public sealed record ConversationResponse(
    Guid Id,
    string Title,
    bool IsGroup,
    Guid? ProjectId,
    Guid? ContractId,
    DateTime? LastMessageAt,
    DateTime CreatedAt);