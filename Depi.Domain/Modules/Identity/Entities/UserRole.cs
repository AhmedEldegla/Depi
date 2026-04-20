namespace DEPI.Domain.Modules.Identity.Entities;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;

public class UserRole : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public DateTime AssignedAt { get; set; }
    public User? User { get; set; }
    public Role? Role { get; set; }
}
