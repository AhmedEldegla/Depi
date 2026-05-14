namespace DEPI.Application.UseCases.Companies.Contracts;

public record CreateCompanyRequest(string Name, string Description, string? Website, string? Size, string? Location);
public record UpdateCompanyRequest(string Name, string Description, string? Website, string? Size, string? Location);