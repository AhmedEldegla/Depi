namespace DEPI.Domain.Entities.Reviews;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Projects;

public class Review : AuditableEntity
{
    public Guid ProjectId { get;set; }
    public Guid ContractId { get;set; }
    public Guid ReviewerId { get;set; }
    public Guid RevieweeId { get;set; }
    public ReviewType Type { get;set; }
    public int Rating { get;set; }
    public string? Comment { get;set; }
    public int CommunicationRating { get;set; }
    public int QualityRating { get;set; }
    public int TimelinessRating { get;set; }
    public int ValueRating { get;set; }
    public bool IsPublic { get;set; }
    public DateTime? ResponseAt { get;set; }
    public string? Response { get;set; }

    public Project? Project { get;set; }
    public Contract? Contract { get;set; }
    public User? Reviewer { get;set; }
    public User? Reviewee { get;set; }

    private Review() { }

    public static Review Create(
        Guid projectId,
        Guid contractId,
        Guid reviewerId,
        Guid revieweeId,
        ReviewType type,
        int rating,
        string? comment = null)
    {
        if (rating < 1 || rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5", nameof(rating));

        return new Review
        {
            ProjectId = projectId,
            ContractId = contractId,
            ReviewerId = reviewerId,
            RevieweeId = revieweeId,
            Type = type,
            Rating = rating,
            Comment = comment?.Trim(),
            IsPublic = true
        };
    }

    public void SetDetailedRatings(int communication, int quality, int timeliness, int value)
    {
        if (communication < 1 || communication > 5)
            throw new ArgumentException("Communication rating must be between 1 and 5");
        if (quality < 1 || quality > 5)
            throw new ArgumentException("Quality rating must be between 1 and 5");
        if (timeliness < 1 || timeliness > 5)
            throw new ArgumentException("Timeliness rating must be between 1 and 5");
        if (value < 1 || value > 5)
            throw new ArgumentException("Value rating must be between 1 and 5");

        CommunicationRating = communication;
        QualityRating = quality;
        TimelinessRating = timeliness;
        ValueRating = value;
    }

    public void AddResponse(string response)
    {
        Response = response.Trim();
        ResponseAt = DateTime.UtcNow;
    }

    public void SetPublic(bool isPublic)
    {
        IsPublic = isPublic;
    }
}

public enum ReviewType
{
    FreelancerToClient = 1,
    ClientToFreelancer = 2
}
