using ADPv2.Models.Entities;
using ADPv2.Models.ViewModels;

namespace ADPv2.Models.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> ValidateAccount(AccountCredentialsRequestDto request);
        Task<AccountEntity> GetAccountByUsername(string username);
    }
}
