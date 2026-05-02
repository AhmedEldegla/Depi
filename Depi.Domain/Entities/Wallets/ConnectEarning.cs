using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;

namespace DEPI.Domain.Entities.Wallets;

public class ConnectEarningRule : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ConnectsAwarded { get; set; }
    public EarningTrigger Trigger { get; set; }
    public decimal? MinRating { get; set; }
    public int? CooldownHours { get; set; }
    public int MaxPerDay { get; set; } = 5;
    public bool IsActive { get; set; } = true;
    public int DisplayOrder { get; set; }
}

public class ConnectEarning : AuditableEntity
{
    public string UserId { get; set; } = string.Empty;
    public Guid RuleId { get; set; }
    public int ConnectsEarned { get; set; }
    public string TriggerType { get; set; } = string.Empty;
    public string SourceDescription { get; set; } = string.Empty;
    public Guid? RelatedProjectId { get; set; }
    public Guid? RelatedReviewId { get; set; }
    public Guid? RelatedMessageId { get; set; }
    public DateTime EarnedAt { get; set; } = DateTime.UtcNow;

    public virtual User User { get; set; } = null!;
    public virtual ConnectEarningRule Rule { get; set; } = null!;
}

public enum EarningTrigger
{
    ProjectCompleted = 1,
    FiveStarReview = 2,
    FastResponse = 3,
    ProfileCompleted = 4,
    PortfolioPublished = 5,
    DailyLogin = 6,
    FirstProjectWon = 7,
    GuildJoined = 8,
    CourseCompleted = 9,
    CertificationEarned = 10,
    Referral = 11,
    Streak7Days = 12
}
