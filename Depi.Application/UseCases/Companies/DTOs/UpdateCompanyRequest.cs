namespace DEPI.Application.DTOs.Companies;

public record UpdateCompanyRequest(
    string Name,
    string Description,
    string? Website,
    string? Size,
    string? Location
);