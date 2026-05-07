namespace DEPI.Domain.Entities.Profiles;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;

public class PortfolioItem : AuditableEntity
{
    public Guid UserId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string? Url { get; private set; }
    public string? LiveUrl { get; private set; }
    public bool IsFeatured { get; private set; }
    public bool IsPublished { get; private set; }
    public int ViewCount { get; private set; }
    public int DisplayOrder { get; private set; }

    public virtual User? User { get; private set; }

    private PortfolioItem() { }

    public static PortfolioItem Create(
        Guid userId,
        string title,
        string description,
        string? url = null,
        string? liveUrl = null)
    {
        return new PortfolioItem
        {
            UserId = userId,
            Title = title,
            Description = description,
            Url = url,
            LiveUrl = liveUrl,
            IsFeatured = false,
            IsPublished = false,
            ViewCount = 0,
            DisplayOrder = 0
        };
    }

    public void UpdateInfo(string title, string description, string? url, string? liveUrl)
    {
        Title = title;
        Description = description;
        Url = url;
        LiveUrl = liveUrl;
    }

    public void Publish()
    {
        if (IsPublished)
            throw new InvalidOperationException("العنصر منشور بالفعل");

        IsPublished = true;
    }

    public void Unpublish()
    {
        if (!IsPublished)
            throw new InvalidOperationException("العنصر غير منشور");

        IsPublished = false;
    }

    public void ToggleFeatured()
    {
        IsFeatured = !IsFeatured;
    }

    public void IncrementViewCount()
    {
        ViewCount++;
    }

    public void SetDisplayOrder(int order)
    {
        DisplayOrder = Math.Max(0, order);
    }
}