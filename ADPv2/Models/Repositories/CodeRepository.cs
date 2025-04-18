using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;
using ADPv2.Settings;
using Dapper;
using Microsoft.Extensions.Options;

namespace ADPv2.Models.Repositories
{
    public class CodeRepository : BaseRepository, ICodeRepository
    {
        public CodeRepository(IOptions<ConnectionSettings> settings) : base(settings.Value.Connection) { }

        public async Task<CodeEntity> GetCode(string code)
        {
            var query = "SELECT * FROM tblCodes WHERE AlphaCode = @AlphaCode";
            var param = new
            {
                AlphaCode = code
            };

            return await _db.QueryFirstOrDefaultAsync<CodeEntity>(query, param);
        }

        public async Task<int> UpdateCode(CodeEntity entity)
        {
            try
            {
                var query = "UPDATE tblCodes SET AlphaCode = @AlphaCode, Amount = @Amount, ExpirationDate = @ExpirationDate, IsUsed = @IsUsed, DateCreated = @DateCreated, CreatedBy = @CreatedBy WHERE ID = @ID";
                var parameters = new
                {
                    ID = entity.ID,
                    AlphaCode = entity.AlphaCode,
                    Amount = entity.Amount,
                    ExpirationDate = entity.ExpirationDate,
                    IsUsed = entity.IsUsed,
                    DateCreated = entity.DateCreated,
                    CreatedBy = entity.CreatedBy
                };
                return await _db.ExecuteAsync(query, parameters);
            }
            catch
            {
                return 0;
            }
        }
    }
}
