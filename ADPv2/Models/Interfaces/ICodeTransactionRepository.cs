using ADPv2.Models.Entities;

namespace ADPv2.Models.Interfaces
{
    public interface ICodeTransactionRepository
    {
        Task<int> CreateCodeTransaction(CodeTransactionEntity entity);
    }
}
