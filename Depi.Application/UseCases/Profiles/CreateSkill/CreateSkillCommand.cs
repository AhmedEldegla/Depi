using DEPI.Application.DTOs.Profiles;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.CreateSkill;
public record CreateSkillCommand(string Name, string? NameEn, string? Description, bool IsVerified, int? DisplayOrder) : IRequest<SkillResponse>;