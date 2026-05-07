namespace DEPI.Domain.Entities.Profiles;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Shared;
using DEPI.Domain.Enums;

public class UserProfile : AuditableEntity
{
    public Guid UserId { get; private set; }
    public string DisplayName { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
    public string Bio { get; private set; } = string.Empty;
    public decimal HourlyRate { get; private set; }
    public string Timezone { get; private set; } = "Africa/Cairo";
    public bool IsAvailable { get; private set; } = true;
    public int ResponseTime { get; private set; } = 24;
    public int CompletedProjects { get; private set; }
    public Guid? CountryId { get; private set; }
    public Guid? CurrencyId { get; private set; }
    public string? LinkedInUrl { get; private set; }
    public string? PortfolioUrl { get; private set; }
    public string? GithubUrl { get; private set; }
    public string? WebsiteUrl { get; private set; }
    public Gender Gender { get; private set; } = Gender.NotSpecified;
    public DateTime? BirthDate { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? Address { get; private set; }
    public string ExperienceLevel { get; private set; } = "Intermediate";
    public int SkillsCount { get; private set; }
    public decimal TotalEarnings { get; private set; }
    public decimal ResponseRate { get; private set; } = 0.8m;
    public decimal OnTimeDeliveryRate { get; private set; } = 0.9m;
    public decimal CompletionRate { get; private set; } = 0.85m;
    public int ProfileCompletion { get; private set; } = 50;
    public string? CountryName { get; private set; }

    public virtual User? User { get; private set; }
    public virtual Country? Country { get; private set; }
    public virtual Currency? Currency { get; private set; }

    private UserProfile() { }

    public static UserProfile Create(
        Guid userId,
        string displayName,
        string title,
        string bio,
        decimal hourlyRate)
    {
        return new UserProfile
        {
            UserId = userId,
            DisplayName = displayName,
            Title = title,
            Bio = bio,
            HourlyRate = hourlyRate,
            Timezone = "Africa/Cairo",
            IsAvailable = true,
            ResponseTime = 24
        };
    }

    public void UpdateInfo(string displayName, string title, string bio)
    {
        DisplayName = displayName;
        Title = title;
        Bio = bio;
    }

    public void SetHourlyRate(decimal rate, Guid currencyId)
    {
        if (rate < 0)
            throw new ArgumentException("السعر يجب أن يكون إيجابياً", nameof(rate));

        HourlyRate = rate;
        CurrencyId = currencyId;
    }

    public void SetLocation(Guid? countryId, string? address)
    {
        CountryId = countryId;
        Address = address;
    }

    public void SetAvailability(bool isAvailable)
    {
        IsAvailable = isAvailable;
    }

    public void SetLinks(string? linkedIn, string? portfolio, string? github, string? website)
    {
        LinkedInUrl = linkedIn;
        PortfolioUrl = portfolio;
        GithubUrl = github;
        WebsiteUrl = website;
    }

    public void IncrementCompletedProjects()
    {
        CompletedProjects++;
    }
}