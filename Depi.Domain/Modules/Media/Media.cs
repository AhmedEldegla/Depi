namespace DEPI.Domain.Entities.Media;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;

public class Media : BaseEntity
{
    public Guid UploaderId { get;set; }
    public string FileName { get;set; } = string.Empty;
    public string OriginalFileName { get;set; } = string.Empty;
    public string FileExtension { get;set; } = string.Empty;
    public string FileSize { get;set; } = string.Empty;
    public long FileSizeBytes { get;set; }
    public string ContentType { get;set; } = string.Empty;
    public string StorageUrl { get;set; } = string.Empty;
    public string? ThumbnailUrl { get;set; }
    public MediaType Type { get;set; }
    public string? AltText { get;set; }
    public string? Description { get;set; }
    public int DownloadCount { get;set; }
    public bool IsPublic { get;set; }

    public User? Uploader { get;set; }

    private Media() { }

    public static Media Create(
        Guid uploaderId,
        string fileName,
        string originalFileName,
        string fileExtension,
        long fileSizeBytes,
        string contentType,
        string storageUrl,
        MediaType type,
        bool isPublic = false)
    {
        if (uploaderId == Guid.Empty)
            throw new ArgumentException("Uploader ID is required", nameof(uploaderId));

        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("File name is required", nameof(fileName));

        if (fileSizeBytes <= 0)
            throw new ArgumentException("File size must be greater than zero", nameof(fileSizeBytes));

        return new Media
        {
            UploaderId = uploaderId,
            FileName = fileName.Trim(),
            OriginalFileName = originalFileName.Trim(),
            FileExtension = fileExtension.Trim().ToLowerInvariant(),
            FileSizeBytes = fileSizeBytes,
            FileSize = FormatFileSize(fileSizeBytes),
            ContentType = contentType.Trim().ToLowerInvariant(),
            StorageUrl = storageUrl.Trim(),
            Type = type,
            IsPublic = isPublic,
            DownloadCount = 0
        };
    }

    public void IncrementDownloadCount()
    {
        DownloadCount++;
    }

    public void UpdateAltText(string? altText)
    {
        AltText = altText?.Trim();
    }

    public void UpdateDescription(string? description)
    {
        Description = description?.Trim();
    }

    private static string FormatFileSize(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        int order = 0;
        double size = bytes;
        while (size >= 1024 && order < sizes.Length - 1)
        {
            order++;
            size = size / 1024;
        }
        return $"{size:0.##} {sizes[order]}";
    }
}

public enum MediaType
{
    Image = 1,
    Document = 2,
    Video = 3,
    Audio = 4,
    Archive = 5,
    Other = 6
}
