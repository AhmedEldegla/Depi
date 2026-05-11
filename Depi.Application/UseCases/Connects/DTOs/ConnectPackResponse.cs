namespace DEPI.Application.DTOs.Connects;

public class ConnectPackResponse
{
    public Guid Id { get; set; }
    public int Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsBonus { get; set; }
}