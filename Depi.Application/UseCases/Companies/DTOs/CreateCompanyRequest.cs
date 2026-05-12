namespace DEPI.Application.DTOs.Companies;

public record CreateCompanyRequest(
    string Name,
    string Description,
    string? Website,
    string? Size,
    string? Location
);