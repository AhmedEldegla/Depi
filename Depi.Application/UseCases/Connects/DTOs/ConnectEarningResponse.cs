namespace DEPI.Application.DTOs.Connects;

public class ConnectEarningResponse
{
    public Guid Id { get; set; }
    public int ConnectsEarned { get; set; }
    public string TriggerType { get; set; } = string.Empty;
    public string SourceDescription { get; set; } = string.Empty;
    public DateTime EarnedAt { get; set; }
}