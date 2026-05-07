using DEPI.Domain.Enums;

namespace DEPI.Application.DTOs.Profiles;

public class UserProfileResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public decimal HourlyRate { get; set; }
    public string? CurrencyCode { get; set; }
    public string Timezone { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
    public int ResponseTime { get; set; }
    public int CompletedProjects { get; set; }
    public Guid? CountryId { get; set; }
    public string? CountryName { get; set; }
    public string? LinkedInUrl { get; set; }
    public string? PortfolioUrl { get; set; }
    public string? GithubUrl { get; set; }
    public string? WebsiteUrl { get; set; }
    public Gender Gender { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}