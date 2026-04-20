namespace DEPI.Domain.Entities.Projects;

using DEPI.Domain.Common.Base;

public class ProjectAttachment : AuditableEntity
{
    public Guid ProjectId { get;set; }
    public Guid MediaId { get;set; }
    public string Title { get;set; } = string.Empty;
    public string? Description { get;set; }
    public AttachmentType Type { get;set; }
    public bool IsMandatory { get;set; }
    public int DownloadCount { get;set; }

    public Project? Project { get;set; }
    public virtual Media.Media Media { get;set; }

    private ProjectAttachment() { }

    public static ProjectAttachment Create(
        Guid projectId,
        Guid mediaId,
        string title,
        AttachmentType type,
        string? description = null,
        bool isMandatory = false)
    {
        if (projectId == Guid.Empty)
            throw new ArgumentException("Project ID is required", nameof(projectId));

        if (mediaId == Guid.Empty)
            throw new ArgumentException("Media ID is required", nameof(mediaId));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required", nameof(title));

        return new ProjectAttachment
        {
            ProjectId = projectId,
            MediaId = mediaId,
            Title = title.Trim(),
            Description = description?.Trim(),
            Type = type,
            IsMandatory = isMandatory,
            DownloadCount = 0
        };
    }

    public void IncrementDownloadCount()
    {
        DownloadCount++;
    }

    public void UpdateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required", nameof(title));

        Title = title.Trim();
    }

    public void UpdateDescription(string? description)
    {
        Description = description?.Trim();
    }
}

public enum AttachmentType
{
    File = 1,
    Image = 2,
    Document = 3,
    Reference = 4,
    Sample = 5,
    Other = 6
}
