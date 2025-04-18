using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;

namespace ADPv2.Models.Services
{
    public class CodeService : ICodeService
    {
        private readonly ICodeRepository _codeRepository;

        public CodeService(ICodeRepository codeRepository)
        {
            this._codeRepository = codeRepository;
        }

        public async Task<CodeEntity> GetAlphaCode(string code)
        {
            return await _codeRepository.GetCode(code);
        }

        public async Task<int> UpdateAlphaCode(CodeEntity entity)
        {
            return await _codeRepository.UpdateCode(entity);
        }
    }
}
