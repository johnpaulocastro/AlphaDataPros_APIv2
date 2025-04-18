using ADPv2.Models.Entities;

namespace ADPv2.Models.Interfaces
{
    public interface ITransactionTypeRepository
    {
        Task<TransactionTypeEntity> GetTransactionTypeById(int id);
        Task<TransactionTypeEntity> GetTransactionTypeByDescription(string desc);
    }
}
