namespace DEPI.Domain.Entities.Freelancers;

using Depi.Domain.Modules.Projects.Enums;
using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;

public class FreelancerProfile : AuditableEntity
{
    public Guid UserId { get;set; }
    public string Headline { get;set; } = string.Empty;
    public string Overview { get;set; } = string.Empty;
    public string? AvatarUrl { get;set; }
    public string? HourlyRate { get;set; }
    public ExperienceLevel Level { get;set; }
    public int CompletedProjectsCount { get;set; }
    public decimal TotalEarnings { get;set; }
    public decimal Rating { get;set; }
    public int TotalReviews { get;set; }
    public string? Website { get;set; }
    public string? LinkedInUrl { get;set; }
    public string? GitHubUrl { get;set; }
    public bool IsAvailable { get;set; }
    public int ResponseTimeHours { get;set; }
    public string? Skills { get;set; }
    public string? Languages { get;set; }
    public string? Certifications { get;set; }
    public string? Education { get;set; }

    public User? User { get;set; }
    public ICollection<FreelancerSkill> SkillsList { get;set; } = new HashSet<FreelancerSkill>();
    public ICollection<FreelancerLanguage> LanguagesList { get;set; } = new HashSet<FreelancerLanguage>();
    public ICollection<FreelancerPortfolio> Portfolio { get;set; } = new HashSet<FreelancerPortfolio>();

    private FreelancerProfile() { }

    public static FreelancerProfile Create(
        Guid userId,
        string headline,
        string overview,
        ExperienceLevel level)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User ID is required", nameof(userId));

        return new FreelancerProfile
        {
            UserId = userId,
            Headline = headline.Trim(),
            Overview = overview.Trim(),
            Level = level,
            CompletedProjectsCount = 0,
            TotalEarnings = 0,
            Rating = 0,
            TotalReviews = 0,
            IsAvailable = true,
            ResponseTimeHours = 24
        };
    }

    public void UpdateProfile(string headline, string overview)
    {
        Headline = headline.Trim();
        Overview = overview.Trim();
    }

    public void SetHourlyRate(string? rate)
    {
        HourlyRate = rate?.Trim();
    }

    public void SetAvailability(bool isAvailable)
    {
        IsAvailable = isAvailable;
    }

    public void AddSkill(string skill)
    {
        if (string.IsNullOrEmpty(Skills))
            Skills = skill;
        else
            Skills += "," + skill;
    }

    public void IncrementCompletedProjects()
    {
        CompletedProjectsCount++;
    }

    public void AddEarnings(decimal amount)
    {
        TotalEarnings += amount;
    }

    public void UpdateRating(decimal newRating)
    {
        if (TotalReviews == 0)
        {
            Rating = newRating;
            TotalReviews = 1;
        }
        else
        {
            Rating = ((Rating * TotalReviews) + newRating) / (TotalReviews + 1);
            TotalReviews++;
        }
    }
}

public class FreelancerSkill : BaseEntity
{
    public Guid FreelancerProfileId { get;set; }
    public string Name { get;set; } = string.Empty;
    public int ProficiencyLevel { get;set; }

    public FreelancerProfile? FreelancerProfile { get;set; }

    private FreelancerSkill() { }
}

public class FreelancerLanguage : BaseEntity
{
    public Guid FreelancerProfileId { get;set; }
    public string Name { get;set; } = string.Empty;
    public string Proficiency { get;set; } = string.Empty;

    public FreelancerProfile? FreelancerProfile { get;set; }

    private FreelancerLanguage() { }
}

public class FreelancerPortfolio : BaseEntity
{
    public Guid FreelancerProfileId { get;set; }
    public string Title { get;set; } = string.Empty;
    public string? Description { get;set; }
    public string? Url { get;set; }
    public string? ImageUrl { get;set; }

    public FreelancerProfile? FreelancerProfile { get;set; }

    private FreelancerPortfolio() { }
}
