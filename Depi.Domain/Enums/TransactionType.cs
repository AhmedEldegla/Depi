namespace DEPI.Domain.Enums;

public enum TransactionType
{
    Deposit = 1,
    Withdrawal = 2,
    Transfer = 3,
    Escrow = 4,
    EscrowRelease = 5,
    Refund = 6,
    Commission = 7
}