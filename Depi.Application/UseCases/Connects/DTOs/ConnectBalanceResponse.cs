namespace DEPI.Application.DTOs.Connects;

public class ConnectBalanceResponse
{
    public int TotalPurchased { get; set; }
    public int TotalUsed { get; set; }
    public int BonusConnects { get; set; }
    public int Available { get; set; }
    public decimal TotalSpent { get; set; }
}