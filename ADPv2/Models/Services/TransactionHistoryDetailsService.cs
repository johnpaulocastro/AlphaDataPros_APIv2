using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;
using ADPv2.Models.ViewModels;
using System.Transactions;

namespace ADPv2.Models.Services
{
    public class TransactionHistoryDetailsService : ITransactionHistoryDetailsService
    {
        private readonly ITransactionHistoryDetailsRepository _transactionHistoryDetailsRepository;
        private readonly ITransactionTypeService _transactionTypeService;

        public TransactionHistoryDetailsService(ITransactionHistoryDetailsRepository transactionHistoryDetailsRepository, ITransactionTypeService transactionTypeService)
        {
            this._transactionHistoryDetailsRepository = transactionHistoryDetailsRepository;
            this._transactionTypeService = transactionTypeService;
        }

        public async Task<List<TransactionHistoryDetailsRemarksResponseDto>> GetTransactionHistoryDetails(int transactionId)
        {
            var result = await _transactionHistoryDetailsRepository.GetTransactionHistoryDetails(transactionId);
            var response = result.Select(r => new TransactionHistoryDetailsRemarksResponseDto
            {
                TransactionId = r.TransactionIdNo,
                Remarks = r.TransactionNotes,
                RemarksDate = r.DateCreated
            }).ToList();

            return response;
        }

        public async Task<bool> CreateInitialTransactionHistoryDetails(TransactionHistoryCreateRequestDto objModel, bool isApi = false)
        {
            var transactionType = isApi ?
                await _transactionTypeService.GetTransactionTypeByDescription("API Version 2.0") :
                await _transactionTypeService.GetTransactionTypeByDescription("Virtual Terminal");

            var transactionDetails = new TransactionHistoryDetailsEntity
            {
                TransactionIdNo = objModel.TransactionId,
                TransactionApprovalStatusID = objModel.ApprovalStatusId,
                TransactionStatusID = objModel.TransactionStatusId,
                TransactionTypeID = transactionType.TransactionTypeID,
                TransactionNotes = objModel.TransactionNotes,
                CreatedBy = objModel.UserLogged,
                DateCreated = DateTime.Now,
                UpdatedBy = objModel.UserLogged,
                DateUpdated = DateTime.Now
            };
            var result = await _transactionHistoryDetailsRepository.CreateTransactionHistoryDetails(transactionDetails);

            if (result > 0)
            {
                var complianceTransactionType = await _transactionTypeService.GetTransactionTypeByDescription("OFAC Compliance");
                var complianceTransactionDetails = new TransactionHistoryDetailsEntity
                {
                    TransactionIdNo = objModel.TransactionId,
                    TransactionApprovalStatusID = objModel.ApprovalStatusId,
                    TransactionStatusID = objModel.TransactionStatusId,
                    TransactionTypeID = complianceTransactionType.TransactionTypeID,
                    CreatedBy = objModel.UserLogged,
                    DateCreated = DateTime.Now,
                    UpdatedBy = objModel.UserLogged,
                    DateUpdated = DateTime.Now
                };

                result = await _transactionHistoryDetailsRepository.CreateTransactionHistoryDetails(complianceTransactionDetails);
            }

            return (result > 0);
        }
    }
}
