namespace DEPI.Application.DTOs.Connects;

public class ConnectUsageResponse
{
    public Guid Id { get; set; }
    public int ConnectsUsed { get; set; }
    public string UsageType { get; set; } = string.Empty;
    public Guid? ProjectId { get; set; }
    public Guid? JobId { get; set; }
    public bool IsRefunded { get; set; }
    public DateTime CreatedAt { get; set; }
}