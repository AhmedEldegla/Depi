using DEPI.Application.DTOs.Profiles;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.UpdateSkill;
public record UpdateSkillCommand(
    Guid Id,
    string Name,
    string NameEn,
    string? Description,
    bool IsVerified,
    bool IsActive,
    int DisplayOrder
) : IRequest<SkillResponse>;