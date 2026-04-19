namespace DEPI.Domain.Entities.HeadHunters;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;

public class HeadHunterProfile : AuditableEntity
{
    public Guid UserId { get;set; }
    public string CompanyName { get;set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public string? Website { get; set; }
    public string? Description { get; set; }
    public int SuccessfulPlacements { get; set; }
    public decimal TotalEarnings { get; set; }
    public decimal Rating { get;set; }
    public int TotalReviews { get;set; }
    public bool IsVerified { get;set; }
    public HeadHunterStatus Status { get; set; }
    public string? PaymentDetails { get; set; }
    public decimal Balance { get;set; }
    public int ConnectsBalance { get;set; }

    public User? User { get;set; }
    public ICollection<SearchRequest> SearchRequests { get; set; } = new HashSet<SearchRequest>();
    public ICollection<TalentRecommendation> Recommendations { get; set; } = new HashSet<TalentRecommendation>();

    private HeadHunterProfile() { }

    public static HeadHunterProfile Create(
        Guid userId,
        string companyName,
        string specialization)
    {
        return new HeadHunterProfile
        {
            UserId = userId,
            CompanyName = companyName.Trim(),
            Specialization = specialization.Trim(),
            Status = HeadHunterStatus.Active,
            SuccessfulPlacements = 0,
            TotalEarnings = 0,
            Rating = 0,
            TotalReviews = 0,
            Balance = 0,
            ConnectsBalance = 10
        };
    }

    public void UpdateProfile(string companyName, string specialization, string? description)
    {
        CompanyName = companyName.Trim();
        Specialization = specialization.Trim();
        Description = description?.Trim();
    }

    public void AddPlacement(decimal earnings)
    {
        SuccessfulPlacements++;
        TotalEarnings += earnings;
        Balance += earnings * 0.8m;
    }

    public void AddRating(decimal newRating)
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

    public void Verify()
    {
        IsVerified = true;
    }

    public void Suspend()
    {
        Status = HeadHunterStatus.Suspended;
    }

    public void Activate()
    {
        Status = HeadHunterStatus.Active;
    }

    public void UseConnect()
    {
        if (ConnectsBalance <= 0)
            throw new InvalidOperationException("No connects remaining");
        ConnectsBalance--;
    }

    public void AddConnects(int count)
    {
        ConnectsBalance += count;
    }

    public void Withdraw(decimal amount)
    {
        if (amount > Balance)
            throw new InvalidOperationException("Insufficient balance");
        Balance -= amount;
    }
}

public class SearchRequest : AuditableEntity
{
    public Guid HeadHunterId { get;set; }
    public string Title { get;set; } = string.Empty;
    public string Description { get;set; } = string.Empty;
    public string? RequiredSkills { get;set; }
    public int? MinExperienceYears { get;set; }
    public decimal? MinHourlyRate { get;set; }
    public decimal? MaxHourlyRate { get;set; }
    public string? Location { get;set; }
    public int ResultsLimit { get;set; }
    public int ConnectsUsed { get;set; }
    public SearchStatus Status { get;set; }
    public DateTime? CompletedAt { get;set; }
    public int MatchesFound { get;set; }

    public HeadHunterProfile? HeadHunter { get;set; }
    public ICollection<TalentRecommendation> Recommendations { get;set; } = new HashSet<TalentRecommendation>();

    private SearchRequest() { }

    public static SearchRequest Create(
        Guid headHunterId,
        string title,
        string description,
        int resultsLimit = 10)
    {
        return new SearchRequest
        {
            HeadHunterId = headHunterId,
            Title = title.Trim(),
            Description = description.Trim(),
            ResultsLimit = resultsLimit,
            Status = SearchStatus.Pending,
            ConnectsUsed = resultsLimit
        };
    }

    public void SetFilters(
        string? skills,
        int? minExperience,
        decimal? minRate,
        decimal? maxRate,
        string? location)
    {
        RequiredSkills = skills;
        MinExperienceYears = minExperience;
        MinHourlyRate = minRate;
        MaxHourlyRate = maxRate;
        Location = location;
    }

    public void Complete(int matchesFound)
    {
        Status = SearchStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        MatchesFound = matchesFound;
    }

    public void Cancel()
    {
        Status = SearchStatus.Cancelled;
    }
}

public class TalentRecommendation : AuditableEntity
{
    public Guid SearchRequestId { get;set; }
    public Guid HeadHunterId { get;set; }
    public Guid FreelancerId { get;set; }
    public decimal MatchScore { get;set; }
    public string MatchReason { get;set; } = string.Empty;
    public RecommendationStatus Status { get;set; }
    public Guid? ClientId { get;set; }
    public DateTime? PresentedAt { get;set; }
    public DateTime? HiredAt { get;set; }
    public decimal? CommissionEarned { get;set; }
    public string? Feedback { get;set; }

    public SearchRequest? SearchRequest { get;set; }
    public HeadHunterProfile? HeadHunter { get;set; }
    public User? Freelancer { get;set; }

    private TalentRecommendation() { }

    public static TalentRecommendation Create(
        Guid searchRequestId,
        Guid headHunterId,
        Guid freelancerId,
        decimal matchScore,
        string matchReason)
    {
        return new TalentRecommendation
        {
            SearchRequestId = searchRequestId,
            HeadHunterId = headHunterId,
            FreelancerId = freelancerId,
            MatchScore = matchScore,
            MatchReason = matchReason,
            Status = RecommendationStatus.Pending
        };
    }

    public void PresentToClient(Guid clientId)
    {
        Status = RecommendationStatus.Presented;
        ClientId = clientId;
        PresentedAt = DateTime.UtcNow;
    }

    public void MarkAsHired(decimal commission)
    {
        Status = RecommendationStatus.Hired;
        HiredAt = DateTime.UtcNow;
        CommissionEarned = commission;
    }

    public void Reject(string reason)
    {
        Status = RecommendationStatus.Rejected;
        Feedback = reason;
    }
}

public enum HeadHunterStatus
{
    Active = 1,
    Suspended = 2,
    Inactive = 3
}

public enum SearchStatus
{
    Pending = 1,
    InProgress = 2,
    Completed = 3,
    Cancelled = 4
}

public enum RecommendationStatus
{
    Pending = 1,
    Presented = 2,
    Hired = 3,
    Rejected = 4,
    Withdrawn = 5
}
