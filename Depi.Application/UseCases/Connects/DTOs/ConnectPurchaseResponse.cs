using DEPI.Domain.Entities.Wallets;
using DEPI.Domain.Enums;
namespace DEPI.Application.DTOs.Connects;

public class ConnectPurchaseResponse
{
    public Guid Id { get; set; }
    public string PackDescription { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int TotalConnects { get; set; }
    public decimal AmountPaid { get; set; }
    public string Currency { get; set; } = string.Empty;
    public PurchaseStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}