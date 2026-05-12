namespace DEPI.Application.DTOs.Connects;

public record PurchaseConnectsRequest(Guid ConnectPackId, string PaymentMethod);