using ADPv2.Models.Entities;

namespace ADPv2.Models.Interfaces
{
    public interface ITransactionTypeService
    {
        Task<TransactionTypeEntity> GetTransactionTypeById(int id);
        Task<TransactionTypeEntity> GetTransactionTypeByDescription(string desc);
    }
}
