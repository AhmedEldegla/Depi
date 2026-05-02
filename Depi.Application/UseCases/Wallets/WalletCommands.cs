using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Wallets;
using DEPI.Application.Interfaces;
using DEPI.Domain.Entities.Wallets;
using DEPI.Domain.Enums;
using MediatR;

namespace DEPI.Application.UseCases.Wallets.Commands;

public static class WalletConstants
{
    public const decimal WithdrawalFeePercentage = 0.01m;
    public const decimal TransferFeePercentage = 0.01m;
}

public record CreateWalletCommand(Guid UserId) : IRequest<WalletResponse>;

public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand, WalletResponse>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IMapper _mapper;

    public CreateWalletCommandHandler(IWalletRepository walletRepository, IMapper mapper)
    {
        _walletRepository = walletRepository;
        _mapper = mapper;
    }

    public async Task<WalletResponse> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
    {
        var existingWallet = await _walletRepository.GetByUserIdAsync(request.UserId);
        if (existingWallet != null)
            throw new InvalidOperationException(Errors.AlreadyExists("Wallet"));

        var wallet = Wallet.Create(request.UserId);
        await _walletRepository.AddAsync(wallet);

        return _mapper.Map<WalletResponse>(wallet);
    }
}

public record DepositCommand(Guid UserId, DepositRequest Request) : IRequest<TransactionResponse>;

public class DepositCommandHandler : IRequestHandler<DepositCommand, TransactionResponse>
{
    private readonly IWalletRepository _walletRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;

    public DepositCommandHandler(IWalletRepository walletRepository, ITransactionRepository transactionRepository, IMapper mapper)
    {
        _walletRepository = walletRepository;
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    public async Task<TransactionResponse> Handle(DepositCommand request, CancellationToken cancellationToken)
    {
        var wallet = await _walletRepository.GetByUserIdAsync(request.UserId)
            ?? throw new KeyNotFoundException(Errors.NotFound("Wallet"));

        var transaction = Transaction.CreateDeposit(
            walletId: wallet.Id,
            amount: request.Request.Amount,
            fee: 0,
            description: request.Request.Description,
            paymentMethod: request.Request.PaymentMethod
        );

        wallet.Deposit(request.Request.Amount);
        transaction.MarkAsCompleted();

        await _transactionRepository.AddAsync(transaction);
        await _walletRepository.UpdateAsync(wallet);

        return _mapper.Map<TransactionResponse>(transaction);
    }
}

public record WithdrawCommand(Guid UserId, WithdrawRequest Request) : IRequest<TransactionResponse>;

public class WithdrawCommandHandler : IRequestHandler<WithdrawCommand, TransactionResponse>
{
    private readonly IWalletRepository _walletRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;

    public WithdrawCommandHandler(IWalletRepository walletRepository, ITransactionRepository transactionRepository, IMapper mapper)
    {
        _walletRepository = walletRepository;
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    public async Task<TransactionResponse> Handle(WithdrawCommand request, CancellationToken cancellationToken)
    {
        var wallet = await _walletRepository.GetByUserIdAsync(request.UserId)
            ?? throw new KeyNotFoundException(Errors.NotFound("Wallet"));

        if (wallet.GetAvailableBalance() < request.Request.Amount)
            throw new InvalidOperationException("Insufficient balance");

        var transaction = Transaction.CreateWithdrawal(
            walletId: wallet.Id,
            amount: request.Request.Amount,
            fee: request.Request.Amount * WalletConstants.WithdrawalFeePercentage,
            description: request.Request.Description
        );

        wallet.Withdraw(request.Request.Amount);
        transaction.MarkAsCompleted();

        await _transactionRepository.AddAsync(transaction);
        await _walletRepository.UpdateAsync(wallet);

        return _mapper.Map<TransactionResponse>(transaction);
    }
}

public record TransferCommand(Guid FromUserId, TransferRequest Request) : IRequest<TransactionResponse>;

public class TransferCommandHandler : IRequestHandler<TransferCommand, TransactionResponse>
{
    private readonly IWalletRepository _walletRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;

    public TransferCommandHandler(IWalletRepository walletRepository, ITransactionRepository transactionRepository, IMapper mapper)
    {
        _walletRepository = walletRepository;
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    public async Task<TransactionResponse> Handle(TransferCommand request, CancellationToken cancellationToken)
    {
        var fromWallet = await _walletRepository.GetByUserIdAsync(request.FromUserId)
            ?? throw new KeyNotFoundException(Errors.NotFound("Wallet"));

        var toWallet = await _walletRepository.GetByUserIdAsync(request.Request.ToUserId)
            ?? throw new KeyNotFoundException(Errors.NotFound("Recipient wallet"));

        if (fromWallet.GetAvailableBalance() < request.Request.Amount)
            throw new InvalidOperationException("Insufficient balance");

        var transaction = Transaction.CreateTransfer(
            fromWalletId: fromWallet.Id,
            toWalletId: toWallet.Id,
            amount: request.Request.Amount,
            fee: request.Request.Amount * WalletConstants.TransferFeePercentage,
            description: request.Request.Description
        );

        fromWallet.TransferTo(toWallet, request.Request.Amount);
        transaction.MarkAsCompleted();

        await _transactionRepository.AddAsync(transaction);
        await _walletRepository.UpdateAsync(fromWallet);
        await _walletRepository.UpdateAsync(toWallet);

        return _mapper.Map<TransactionResponse>(transaction);
    }
}