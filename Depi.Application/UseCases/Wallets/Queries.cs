using AutoMapper;
using DEPI.Application.DTOs.Wallets;
using DEPI.Application.Interfaces;
using MediatR;

namespace DEPI.Application.UseCases.Wallets.Queries;

public record GetWalletQuery(Guid UserId) : IRequest<WalletResponse?>;

public class GetWalletQueryHandler : IRequestHandler<GetWalletQuery, WalletResponse?>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IMapper _mapper;

    public GetWalletQueryHandler(IWalletRepository walletRepository, IMapper mapper)
    {
        _walletRepository = walletRepository;
        _mapper = mapper;
    }

    public async Task<WalletResponse?> Handle(GetWalletQuery request, CancellationToken cancellationToken)
    {
        var wallet = await _walletRepository.GetByUserIdAsync(request.UserId);
        if (wallet == null) return null;

        var response = _mapper.Map<WalletResponse>(wallet);
        response.AvailableBalance = wallet.GetAvailableBalance();
        return response;
    }
}

public record GetWalletSummaryQuery(Guid UserId) : IRequest<WalletSummaryResponse>;

public class GetWalletSummaryQueryHandler : IRequestHandler<GetWalletSummaryQuery, WalletSummaryResponse>
{
    private readonly IWalletRepository _walletRepository;

    public GetWalletSummaryQueryHandler(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }

    public async Task<WalletSummaryResponse> Handle(GetWalletSummaryQuery request, CancellationToken cancellationToken)
    {
        var wallet = await _walletRepository.GetByUserIdAsync(request.UserId);
        
        var response = new WalletSummaryResponse();
        
        if (wallet == null)
        {
            return response;
        }

        var transactions = await _walletRepository.GetTransactionsAsync(wallet.Id);

        response.Balance = wallet.Balance;
        response.PendingBalance = wallet.PendingBalance;
        response.AvailableBalance = wallet.GetAvailableBalance();
        response.TotalEarnings = wallet.TotalEarnings;
        response.TotalSpent = wallet.TotalSpent;
        response.TotalTransactions = transactions.Count();
        response.PendingTransactions = transactions.Count(t => t.Status == Domain.Enums.TransactionStatus.Pending);

        return response;
    }
}

public record GetTransactionsQuery(Guid WalletId, int Page, int PageSize) : IRequest<List<TransactionResponse>>;

public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, List<TransactionResponse>>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IMapper _mapper;

    public GetTransactionsQueryHandler(IWalletRepository walletRepository, IMapper mapper)
    {
        _walletRepository = walletRepository;
        _mapper = mapper;
    }

    public async Task<List<TransactionResponse>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
    {
        var wallet = await _walletRepository.GetByUserIdAsync(request.WalletId);
        if (wallet == null) return new List<TransactionResponse>();

        var transactions = await _walletRepository.GetTransactionsAsync(wallet.Id);

        return transactions
            .OrderByDescending(t => t.CreatedAt)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(t => _mapper.Map<TransactionResponse>(t))
            .ToList();
    }
}

public record GetTransactionByIdQuery(Guid TransactionId, Guid UserId) : IRequest<TransactionResponse?>;

public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionResponse?>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IMapper _mapper;

    public GetTransactionByIdQueryHandler(IWalletRepository walletRepository, IMapper mapper)
    {
        _walletRepository = walletRepository;
        _mapper = mapper;
    }

    public async Task<TransactionResponse?> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var wallet = await _walletRepository.GetByUserIdAsync(request.UserId);
        if (wallet == null) return null;

        var transaction = await _walletRepository.GetTransactionByIdAsync(request.TransactionId);
        if (transaction == null || transaction.WalletId != wallet.Id) return null;

        return _mapper.Map<TransactionResponse>(transaction);
    }
}