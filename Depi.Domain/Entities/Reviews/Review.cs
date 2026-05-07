namespace DEPI.Domain.Entities.Reviews;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Contracts;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Projects;

public class Review : AuditableEntity
{
    public Guid ReviewerId { get; private set; }
    public Guid RevieweeId { get; private set; }
    public Guid? ProjectId { get; private set; }
    public Guid? ContractId { get; private set; }
    public ReviewType Type { get; private set; }
    public int Rating { get; private set; }
    public string Comment { get; private set; } = string.Empty;
    public string? Response { get; private set; }
    public DateTime? ResponseAt { get; private set; }
    public virtual User? Reviewer { get; private set; }
    public virtual User? Reviewee { get; private set; }
    public virtual Project? Project { get; private set; }
    public virtual Contract? Contract { get; private set; }
    private Review() { }

    public static Review Create(
        Guid reviewerId,
        Guid revieweeId,
        int rating,
        string comment,
        ReviewType type,
        Guid? projectId = null,
        Guid? contractId = null)
    {
        if (rating < 1 || rating > 5)
            throw new ArgumentException("التقييم يجب أن يكون بين 1 و 5", nameof(rating));

        if (string.IsNullOrWhiteSpace(comment))
            throw new ArgumentException("التعليق مطلوب", nameof(comment));

        return new Review
        {
            ReviewerId = reviewerId,
            RevieweeId = revieweeId,
            ProjectId = projectId,
            ContractId = contractId,
            Type = type,
            Rating = rating,
            Comment = comment.Trim()
        };
    }
    public void AddResponse(string response)
    {
        if (string.IsNullOrWhiteSpace(response))
            throw new ArgumentException("الرد مطلوب", nameof(response));

        Response = response.Trim();
        ResponseAt = DateTime.UtcNow;
    }
    public void UpdateRating(int newRating)
    {
        if (newRating < 1 || newRating > 5)
            throw new ArgumentException("التقييم يجب أن يكون بين 1 و 5", nameof(newRating));

        if (Response != null)
            throw new InvalidOperationException("لا يمكن تعديل تقييم به رد");

        Rating = newRating;
    }
    public void UpdateComment(string newComment)
    {
        if (string.IsNullOrWhiteSpace(newComment))
            throw new ArgumentException("التعليق مطلوب", nameof(newComment));

        if (Response != null)
            throw new InvalidOperationException("لا يمكن تعديل تعليق به رد");

        Comment = newComment.Trim();
    }
}

public enum ReviewType
{
    FreelancerToClient = 1,
    ClientToFreelancer = 2,
    FreelancerToFreelancer = 3,
    ClientToClient = 4
}