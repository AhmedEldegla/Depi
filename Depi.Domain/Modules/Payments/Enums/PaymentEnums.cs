namespace Depi.Domain.Modules.Payments.Enums;

public enum WalletStatus
{
    Inactive = 0,
    Active = 1,
    Suspended = 2,
    Closed = 3
}

public enum TransactionType
{
    Deposit = 1,
    Withdrawal = 2,
    Payment = 3,
    Refund = 4,
    Fee = 5,
    Bonus = 6
}

public enum TransactionStatus
{
    Pending = 1,
    Processing = 2,
    Completed = 3,
    Failed = 4,
    Cancelled = 5
}
