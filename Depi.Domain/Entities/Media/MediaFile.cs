namespace DEPI.Domain.Entities.Media;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Enums;

public class MediaFile : AuditableEntity
{
    public string FileName { get; private set; } = string.Empty;
    public string OriginalName { get; private set; } = string.Empty;
    public string FilePath { get; private set; } = string.Empty;
    public string FileExtension { get; private set; } = string.Empty;
    public long FileSize { get; private set; }
    public string MimeType { get; private set; } = string.Empty;
    public MediaType Type { get; private set; }
    public Guid? OwnerId { get; private set; }
    public string? Description { get; private set; }
    public bool IsActive { get; private set; } = true;
    public virtual User? Owner { get; private set; }

    private MediaFile() { }

    public static MediaFile Create(
        string fileName,
        string originalName,
        string filePath,
        string fileExtension,
        long fileSize,
        string mimeType,
        MediaType type,
        Guid? ownerId = null,
        string? description = null)
    {
        return new MediaFile
        {
            FileName = fileName,
            OriginalName = originalName,
            FilePath = filePath,
            FileExtension = fileExtension,
            FileSize = fileSize,
            MimeType = mimeType,
            Type = type,
            OwnerId = ownerId,
            Description = description,
            IsActive = true
        };
    }

    public void UpdateDescription(string description)
    {
        Description = description;
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new InvalidOperationException("الملف غير نشط بالفعل");

        IsActive = false;
    }

    public void Activate()
    {
        if (IsActive)
            throw new InvalidOperationException("الملف نشط بالفعل");

        IsActive = true;
    }
}