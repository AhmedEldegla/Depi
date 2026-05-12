namespace DEPI.Application.DTOs.Connects;

public class EarningSummaryResponse
{
    public int TotalEarned { get; set; }
    public int TotalPurchased { get; set; }
    public int TotalSpent { get; set; }
    public int AvailableBalance { get; set; }
    public int EarnedToday { get; set; }
    public List<EarningRuleResponse> ActiveRules { get; set; } = new();
    public List<ConnectEarningResponse> RecentEarnings { get; set; } = new();
}