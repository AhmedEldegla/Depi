namespace DEPI.Domain.Entities.Wallets;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Enums;

public class Escrow : AuditableEntity
{
    public Guid ContractId { get; private set; }
    public Guid ClientWalletId { get; private set; }
    public Guid FreelancerWalletId { get; private set; }
    public decimal Amount { get; private set; }
    public decimal ReleaseAmount { get; private set; }
    public decimal Fee { get; private set; }
    public EscrowStatus Status { get; private set; }
    public string? Description { get; private set; }
    public DateTime? FundedAt { get; private set; }
    public DateTime? ReleasedAt { get; private set; }
    public DateTime? RefundedAt { get; private set; }

    public virtual Contracts.Contract? Contract { get; private set; }
    public virtual Wallet? ClientWallet { get; private set; }
    public virtual Wallet? FreelancerWallet { get; private set; }

    private Escrow() { }

    public static Escrow Create(
        Guid contractId,
        Guid clientWalletId,
        Guid freelancerWalletId,
        decimal amount,
        string? description = null)
    {
        var fee = amount * 0.10m; // 10% commission

        return new Escrow
        {
            ContractId = contractId,
            ClientWalletId = clientWalletId,
            FreelancerWalletId = freelancerWalletId,
            Amount = amount,
            ReleaseAmount = amount - fee,
            Fee = fee,
            Status = EscrowStatus.Pending,
            Description = description
        };
    }

    public void Fund()
    {
        if (Status != EscrowStatus.Pending)
            throw new InvalidOperationException("الضمان ليس في حالة الانتظار");

        Status = EscrowStatus.Funded;
        FundedAt = DateTime.UtcNow;
    }

    public void Release()
    {
        if (Status != EscrowStatus.Funded)
            throw new InvalidOperationException("الضمان غير ممول");

        Status = EscrowStatus.Released;
        ReleaseAmount = Amount - Fee;
        ReleasedAt = DateTime.UtcNow;
    }

    public void Refund()
    {
        if (Status != EscrowStatus.Funded)
            throw new InvalidOperationException("الضمان غير ممول");

        Status = EscrowStatus.Refunded;
        RefundedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        if (Status != EscrowStatus.Pending)
            throw new InvalidOperationException("لا يمكن إلغاء الضمان");

        Status = EscrowStatus.Cancelled;
    }
}