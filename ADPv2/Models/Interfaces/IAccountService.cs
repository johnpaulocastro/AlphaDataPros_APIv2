using ADPv2.Models.Entities;
using ADPv2.Models.ViewModels;

namespace ADPv2.Models.Interfaces
{
    public interface IAccountService
    {
        Task<bool> Authenticate(AccountCredentialsRequestDto credentials);
        Task<AccountEntity> GetAccount(string username);
    }
}
