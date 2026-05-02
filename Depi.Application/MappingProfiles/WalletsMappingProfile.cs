using AutoMapper;
using DEPI.Application.DTOs.Wallets;
using DEPI.Domain.Entities.Wallets;

namespace DEPI.Application.MappingProfiles;

public class WalletsMappingProfile : Profile
{
    public WalletsMappingProfile()
    {
        CreateMap<Wallet, WalletResponse>();

        CreateMap<Transaction, TransactionResponse>();

        CreateMap<Escrow, EscrowDto>();
    }
}