using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;
using ADPv2.Models.ViewModels;
using ADPv2.Settings;
using Dapper;
using Microsoft.Extensions.Options;
using static Dapper.SqlMapper;

namespace ADPv2.Models.Repositories
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        public AccountRepository(IOptions<ConnectionSettings> settings) : base(settings.Value.Connection) { }

        public async Task<bool> ValidateAccount(AccountCredentialsRequestDto request)
        {
            var result = await _db.QueryFirstOrDefaultAsync<AccountEntity>($"SELECT * FROM tblAccount WHERE Username = '{request.Username}'");
            return (result != null);
        }

        public async Task<AccountEntity> GetAccountByUsername(string username)
        {
            var parameters = new { Username = username };
            var query = @"SELECT ID
                            ,PersonalInfoId
                            ,CustomerNo
                            ,Username
                            ,Password
                            ,Role
                            ,IsTemporary
                            ,UserStatus
                            ,AccountStatus
                            ,DateCreated
                            ,CreatedBy
                            ,DateUpdated
                            ,SecureKey
                            ,SecureIV
                            ,TwoFactor
                            ,UpdatedBy
                            ,LastLoggedIn
                            ,ExcludeSignature
                            ,PasswordExpiry FROM tblAccount 
                        WHERE Username = @Username";

            var result = await _db.QueryFirstOrDefaultAsync<AccountEntity>(query, parameters);
            return result;
        }   
    }
}
