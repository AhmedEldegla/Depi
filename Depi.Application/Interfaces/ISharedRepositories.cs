using System.Threading;
using DEPI.Domain.Entities.Shared;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Interfaces;

public interface ICountryRepository : IRepository<Country>
{
    Task<Country?> GetByIso2Async(string iso2, CancellationToken cancellationToken = default);
    Task<IEnumerable<Country>> GetActiveAsync(CancellationToken cancellationToken = default);
}

public interface ICurrencyRepository : IRepository<Currency>
{
    Task<Currency?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<Currency?> GetDefaultAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Currency>> GetActiveAsync(CancellationToken cancellationToken = default);
}