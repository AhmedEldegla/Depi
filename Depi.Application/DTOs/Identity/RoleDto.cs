namespace DEPI.Application.DTOs.Identity;

public record RoleDto(Guid Id, string Name, string NormalizedName);

public record PermissionDto(Guid Id, string Name, string NormalizedName);

public record UserRoleDto(
    Guid UserId,
    Guid RoleId,
    string RoleName,
    List<PermissionDto> Permissions
);
