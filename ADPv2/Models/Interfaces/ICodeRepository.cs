using ADPv2.Models.Entities;

namespace ADPv2.Models.Interfaces
{
    public interface ICodeRepository
    {
        Task<CodeEntity> GetCode(string code);
        Task<int> UpdateCode(CodeEntity entity);
    }
}
