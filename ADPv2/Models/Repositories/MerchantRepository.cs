using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;
using ADPv2.Settings;
using Dapper;
using Microsoft.Extensions.Options;

namespace ADPv2.Models.Repositories
{
    public class MerchantRepository : BaseRepository, IMerchantRepository
    {
        public MerchantRepository(IOptions<ConnectionSettings> settings) : base(settings.Value.Connection) { }

        public string GenerateMerchantId()
        {
            string merchantId = string.Format("M{0}", DateTime.Now.ToString("yyyyMMddhhmmss"));
            return merchantId;
        }
        public async Task<MerchantCompanyInfoEntity> GetMerchantCompanyInfo(string merchantId)
        {
            var query = "SELECT * FROM tblMerchantCompanyInfo WHERE MerchantId = @MerchantId";
            return await _db.QueryFirstOrDefaultAsync<MerchantCompanyInfoEntity>(query, new { MerchantId = merchantId });
        }
        public async Task<int> CreateMerchantInfo(MerchantInfoEntity entity)
        {
            try
            {
                string query = $@"INSERT INTO tblMerchantInfo
                                               (ResellerID
                                               ,MerchantID
                                               ,MerchantName
                                               ,MerchantStreetNo
                                               ,MerchantStreetName
                                               ,MerchantUnitNo
                                               ,MerchantCity
                                               ,MerchantState
                                               ,MerchantZipCode
                                               ,MerchantCountry
                                               ,MerchantPhoneNo
                                               ,MerchantMobileNo
                                               ,MerchantEmailAddress
                                               ,MerchantSSN
                                               ,MerchantPassportNo
                                               ,CreatedBy
                                               ,DateCreated
                                               ,UpdatedBy
                                               ,DateUpdated)
                                         VALUES
                                            ('{entity.ResellerID}', 
                                            '{entity.MerchantID}', 
                                            '{entity.MerchantName}', 
                                            '{entity.MerchantStreetNo}', 
                                            '{entity.MerchantStreetName}', 
                                            '{entity.MerchantUnitNo}', 
                                            '{entity.MerchantCity}', 
                                            '{entity.MerchantState}', 
                                            '{entity.MerchantZipCode}', 
                                            '{entity.MerchantCountry}', 
                                            '{entity.MerchantPhoneNo}', 
                                            '{entity.MerchantMobileNo}', 
                                            '{entity.MerchantEmailAddress}', 
                                            '{entity.MerchantSSN}', 
                                            '{entity.MerchantPassportNo}', 
                                            '{entity.CreatedBy}', 
                                            '{entity.DateCreated}', 
                                            '{entity.UpdatedBy}', 
                                            '{entity.DateUpdated}')";

                return await _db.ExecuteAsync(query);
            }
            catch
            {
                return 0;
            }
        }
        public async Task<int> CreateMerchantCompanyInfo(MerchantCompanyInfoEntity entity)
        {
            try
            {
                string query = $@"INSERT INTO tblMerchantCompanyInfo
                                               (MerchantID
                                               ,CompanyName
                                               ,CompanyStreetNo
                                               ,CompanyStreetName
                                               ,CompanyUnitNo
                                               ,CompanyCity
                                               ,CompanyState
                                               ,CompanyZipCode
                                               ,CompanyCountry
                                               ,CompanyWebsiteURL
                                               ,CompanyProduct
                                               ,CompanyDescriptor
                                               ,CSRContactName
                                               ,CSRContactPhoneNo
                                               ,CSREmailAddress
                                               ,CreatedBy
                                               ,DateCreated
                                               ,UpdatedBy
                                               ,DateUpdated)
                                         VALUES 
                                            ('{entity.MerchantID}',
                                            '{entity.CompanyName}',
                                            '{entity.CompanyStreetNo}',
                                            '{entity.CompanyStreetName}',
                                            '{entity.CompanyUnitNo}',
                                            '{entity.CompanyCity}',
                                            '{entity.CompanyState}',
                                            '{entity.CompanyZipCode}',
                                            '{entity.CompanyCountry}',
                                            '{entity.CompanyWebsiteURL}',
                                            '{entity.CompanyProduct}',
                                            '{entity.CompanyDescriptor}',
                                            '{entity.CSRContactName}',
                                            '{entity.CSRContactPhoneNo}',
                                            '{entity.CSREmailAddress}',
                                            '{entity.CreatedBy}',
                                            '{entity.DateCreated}',
                                            '{entity.UpdatedBy}',
                                            '{entity.DateUpdated}')";

                return await _db.ExecuteAsync(query);
            }
            catch
            {
                return 0;
            }
        }
        public async Task<int> CreateMerchantTransactionInfo(MerchantTransactionInfoEntity entity)
        {
            try
            {
                string query = $@"INSERT INTO tblMerchantTransactionInfo
                                               (MerchantID
                                               ,TotalSalesPerMonth
                                               ,NumberTransactionPerMonth
                                               ,MinimumTicketAmount
                                               ,MaximumTicketAmount
                                               ,CreatedBy
                                               ,DateCreated
                                               ,UpdatedBy
                                               ,DateUpdated)
                                         VALUES
                                            ('{entity.MerchantID}',
                                            '{entity.TotalSalesPerMonth}',
                                            '{entity.NumberTransactionPerMonth}',
                                            '{entity.MinimumTicketAmount}',
                                            '{entity.MaximumTicketAmount}',
                                            '{entity.CreatedBy}',
                                            '{entity.DateCreated}',
                                            '{entity.UpdatedBy}',
                                            '{entity.DateUpdated}')";

                return await _db.ExecuteAsync(query);
            }
            catch
            {
                return 0;
            }
        }
        public async Task<int> CreateMerchantProcessingHistoryInfo(MerchantProcessingHistoryEntity entity)
        {
            try
            {
                string query = $@"INSERT INTO tblMerchantProcessingHistory
                                               (MerchantID
                                               ,IsAcceptingCreditCard
                                               ,HasBeenProcessedBefore
                                               ,IsAccountTerminated
                                               ,Reason
                                               ,FormerProcessor
                                               ,TimeProcessor
                                               ,CreatedBy
                                               ,DateCreated
                                               ,UpdatedBy
                                               ,DateUpdated)
                                         VALUES
                                            ('{entity.MerchantID}',
                                            '{entity.IsAcceptingCreditCard}',
                                            '{entity.HasBeenProcessedBefore}',
                                            '{entity.IsAccountTerminated}',
                                            '{entity.Reason}',
                                            '{entity.FormerProcessor}',
                                            '{entity.TimeProcessor}',
                                            '{entity.CreatedBy}',
                                            '{entity.DateCreated}',
                                            '{entity.UpdatedBy}',
                                            '{entity.DateUpdated}')";

                return await _db.ExecuteAsync(query);
            }
            catch
            {
                return 0;
            }
        }
        public async Task<int> CreateMerchantOtherInfo(MerchantOtherInfoEntity entity)
        {
            try
            {
                string query = $@"INSERT INTO tblMerchantOtherInfo
                                               (MerchantID
                                               ,OtherInformationCode
                                               ,AgentName
                                               ,CreatedBy
                                               ,DateCreated
                                               ,UpdatedBy
                                               ,DateUpdated)
                                         VALUES
                                               ('{entity.MerchantID}',
                                               '{entity.OtherInformationCode}',
                                               '{entity.AgentName}',
                                               '{entity.CreatedBy}',
                                               '{entity.DateCreated}',
                                               '{entity.UpdatedBy}',
                                               '{entity.DateUpdated}')";

                return await _db.ExecuteAsync(query);
            }
            catch
            {
                return 0;
            }
        }
        public async Task<int> CreateMerchantBankInfo(MerchantBankInfoEntity entity)
        {
            try
            {
                string query = $@"INSERT INTO tblMerchantBankInfo
                                               (MerchantID
                                               ,AccountHolderName
                                               ,AccountHolderAddress
                                               ,AccountHolderCountry
                                               ,BankName
                                               ,BankAddress
                                               ,BankCountry
                                               ,BankSwiftCode
                                               ,BankRoutingNumber
                                               ,BankAccountNumber
                                               ,AdditionalInformation
                                               ,CreatedBy
                                               ,DateCreated
                                               ,UpdatedBy
                                               ,DateUpdated)
                                         VALUES
                                               ('{entity.MerchantID}',
                                               '{entity.AccountHolderName}',
                                               '{entity.AccountHolderAddress}',
                                               '{entity.AccountHolderCountry}',
                                               '{entity.BankName}',
                                               '{entity.BankAddress}',
                                               '{entity.BankCountry}',
                                               '{entity.BankSwiftCode}',
                                               '{entity.BankRoutingNumber}',
                                               '{entity.BankAccountNumber}',
                                               '{entity.AdditionalInformation}',
                                               '{entity.CreatedBy}',
                                               '{entity.DateCreated}',
                                               '{entity.UpdatedBy}',
                                               '{entity.DateUpdated}')";

                return await _db.ExecuteAsync(query);
            }
            catch
            {
                return 0;
            }
        }
    }
}
