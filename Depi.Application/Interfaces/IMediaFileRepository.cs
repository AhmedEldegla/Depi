using System.Threading;
using DEPI.Domain.Entities.Media;
using DEPI.Domain.Enums;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Interfaces;

public interface IMediaFileRepository : IRepository<MediaFile>
{
    Task<MediaFile?> GetByOwnerAsync(Guid ownerId, CancellationToken cancellationToken = default);
    Task<MediaFile?> GetByTypeAsync(Guid ownerId, MediaType type, CancellationToken cancellationToken = default);
    Task<MediaFile?> GetAvatarAsync(Guid ownerId, CancellationToken cancellationToken = default);
    Task<IEnumerable<MediaFile>> GetFilesAsync(Guid? ownerId, MediaType? type = null, bool? isActive = null, CancellationToken cancellationToken = default);
}