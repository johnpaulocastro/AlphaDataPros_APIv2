using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;
using ADPv2.Models.ViewModels;
using AutoMapper;

namespace ADPv2.Models.Services
{
    public class MerchantService : IMerchantService
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly IMapper _mapper;

        public MerchantService(
            IMerchantRepository _merchantRepository,
            IMapper _imapper)
        {
            this._merchantRepository = _merchantRepository;
            this._mapper = _imapper;
        }

        public async Task<MerchantCompanyInfoEntity> GetMerchantCompanyInfo(string merchantId)
        {
            return await _merchantRepository.GetMerchantCompanyInfo(merchantId);
        }

        public async Task<bool> CreateMerchantAccount(AccountMerchantRegistrationRequestDto request)
        {
            var merchantInfo = _mapper.Map<MerchantInfoEntity>(request.MerchantInfo);
            merchantInfo.ResellerID = request.ResellerID;
            await _merchantRepository.CreateMerchantInfo(merchantInfo);

            var merchantCompany = _mapper.Map<MerchantCompanyInfoEntity>(request.MerchantCompanyInfo);
            await _merchantRepository.CreateMerchantCompanyInfo(merchantCompany);

            var merchantTransactionInfo = _mapper.Map<MerchantTransactionInfoEntity>(request.MerchantTransactionInfo);
            await _merchantRepository.CreateMerchantTransactionInfo(merchantTransactionInfo);

            var merchantProcessingHistoryInfo = _mapper.Map<MerchantProcessingHistoryEntity>(request.MerchantProcessingInfo);
            await _merchantRepository.CreateMerchantProcessingHistoryInfo(merchantProcessingHistoryInfo);

            var merchantOtherInfo = _mapper.Map<MerchantOtherInfoEntity>(request.MerchantOtherInfo);
            await _merchantRepository.CreateMerchantOtherInfo(merchantOtherInfo);

            return true;
        }
    }
}
