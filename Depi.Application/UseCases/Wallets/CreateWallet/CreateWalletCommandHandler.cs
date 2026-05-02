// Wallets/CreateWallet/CreateWalletCommandHandler.cs
using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Wallets;
using DEPI.Application.Interfaces;
using MediatR;
using DEPI.Domain.Entities.Wallets;
namespace DEPI.Application.UseCases.Wallets.CreateWallet;
public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand, WalletResponse>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IMapper _mapper;
    public CreateWalletCommandHandler(IWalletRepository walletRepository, IMapper mapper) { _walletRepository = walletRepository; _mapper = mapper; }
    public async Task<WalletResponse> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
    {
        var existingWallet = await _walletRepository.GetByUserIdAsync(request.UserId);
        if (existingWallet != null) throw new InvalidOperationException(Errors.AlreadyExists("Wallet"));
        var wallet = Wallet.Create(request.UserId);
        await _walletRepository.AddAsync(wallet, cancellationToken);
        return _mapper.Map<WalletResponse>(wallet);
    }
}