namespace DEPI.Application.DTOs.Companies;

public class CompanyResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public bool IsVerified { get; set; }
    public decimal Rating { get; set; }
    public int TotalProjects { get; set; }
    public int TeamSize { get; set; }
    public DateTime CreatedAt { get; set; }
}