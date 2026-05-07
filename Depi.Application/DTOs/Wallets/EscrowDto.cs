using DEPI.Domain.Enums;

namespace DEPI.Application.DTOs.Wallets;

public class EscrowDto
{
    public Guid Id { get; set; }
    public Guid ContractId { get; set; }
    public decimal Amount { get; set; }
    public decimal Fee { get; set; }
    public decimal NetAmount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public EscrowStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ReleasedAt { get; set; }
    public Guid ClientWalletId { get; set; }
    public Guid FreelancerWalletId { get; set; }
}

public record CreateEscrowRequest(
    Guid ContractId,
    decimal Amount,
    string? Description
);

public record ReleaseEscrowRequest(Guid EscrowId);