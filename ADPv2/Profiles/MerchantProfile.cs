using ADPv2.Models.Entities;
using ADPv2.Models.ViewModels;
using AutoMapper;

namespace ADPv2.Profiles
{
    public class MerchantProfile : Profile
    {
        public MerchantProfile()
        {
            CreateMap<AccountMerchantInfoRequestDto, MerchantInfoEntity>().ReverseMap();
            CreateMap<AccountMerchantCompanyInfoRequestDto, MerchantCompanyInfoEntity>().ReverseMap();
            CreateMap<AccountMerchantTransactionInfoRequestDto, MerchantTransactionInfoEntity>().ReverseMap();
            CreateMap<AccountMerchantProcessingHistoryInfoRequestDto, MerchantProcessingHistoryEntity>().ReverseMap();
            CreateMap<AccountMerchantOtherInfoRequestDto, MerchantOtherInfoEntity>().ReverseMap();
        }
    }
}
