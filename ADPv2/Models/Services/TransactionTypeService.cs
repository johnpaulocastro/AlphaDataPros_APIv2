using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;

namespace ADPv2.Models.Services
{
    public class TransactionTypeService : ITransactionTypeService
    {
        private readonly ITransactionTypeRepository transactionTypeRepository;

        public TransactionTypeService(
            ITransactionTypeRepository transactionTypeRepository
            )
        {
            this.transactionTypeRepository = transactionTypeRepository;
        }

        public async Task<TransactionTypeEntity> GetTransactionTypeById(int id)
        {
            var result = await transactionTypeRepository.GetTransactionTypeById(id);
            return result;
        }

        public async Task<TransactionTypeEntity> GetTransactionTypeByDescription(string desc)
        {
            var result = await transactionTypeRepository.GetTransactionTypeByDescription(desc);
            return result;
        }

    }
}
