using ADPv2.Models.ViewModels;
using AutoMapper;

namespace ADPv2.Profiles
{
    public class TransactionHistoryProfiles : Profile
    {
        public TransactionHistoryProfiles()
        {
            CreateMap<TransactionHistoryCCardRequestDto, TransactionHistoryRequestDto>();
        }
    }
}
