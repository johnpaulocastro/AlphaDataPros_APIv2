using ADPv2.Models.Entities;

namespace ADPv2.Models.Interfaces
{
    public interface ICodeService
    {
        Task<CodeEntity> GetAlphaCode(string code);
        Task<int> UpdateAlphaCode(CodeEntity entity);
    }
}
