using ADPv2.Models.Entities;
using ADPv2.Models.Interfaces;
using ADPv2.Settings;
using Dapper;
using Microsoft.Extensions.Options;

namespace ADPv2.Models.Repositories
{
    public class PersonalInfoRepository : BaseRepository, IPersonalInfoRepository
    {
        public PersonalInfoRepository(IOptions<ConnectionSettings> settings) : base(settings.Value.Connection) { }

        public async Task<PersonalInfoEntity> GetPersonalInfo(string customerId)
        {
            var query = @"SELECT ID,
                              PersonalInfoId,
                              CustomerNo,
                              SaltData,
                              Title,
                              FirstName,
                              MiddleName,
                              LastName,
                              DateOfBirth,
                              SocialSecurity,
                              Profession,
                              EmailAddress,
                              MobileNumber,
                              LandlineNumber,
                              Status,
                              DefaultArkcode,
                              DateCreated,
                              CreatedBy,
                              DateUpdated,
                              UpdatedBy
                          FROM tblPersonalInfo WHERE PersonalInfoId = @CustomerId";

            var param = new { CustomerId = customerId };

            return await _db.QueryFirstOrDefaultAsync<PersonalInfoEntity>(query, param);
        }
    }
}
