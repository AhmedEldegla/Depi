namespace DEPI.Application.DTOs.Connects;

public class EarningRuleResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ConnectsAwarded { get; set; }
    public string Trigger { get; set; } = string.Empty;
    public int MaxPerDay { get; set; }
}