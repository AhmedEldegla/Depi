using DEPI.Domain.Entities.Wallets;
using DEPI.Domain.Enums;

namespace DEPI.Application.DTOs.Connects;

public record TriggerEarningRequest(
    EarningTrigger Trigger,
    string Description,
    Guid? ProjectId,
    Guid? ReviewId,
    Guid? MessageId
);