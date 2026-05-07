using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DEPI.Domain.Entities.Shared;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Repositories.Shared;

public interface ILanguageRepository : IRepository<Language>
{
    Task<List<Language>> GetActiveLanguagesAsync();
    Task<Language?> GetByCodeAsync(string code);
}

public interface IIndustryCategoryRepository : IRepository<IndustryCategory>
{
    Task<List<IndustryCategory>> GetActiveIndustriesAsync();
    Task<List<IndustryCategory>> GetRootIndustriesAsync();
    Task<List<IndustryCategory>> GetSubIndustriesAsync(Guid parentId);
}