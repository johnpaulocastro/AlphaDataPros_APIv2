using ADPv2.Logger.Model;
using ADPv2.Models.Interfaces;
using ADPv2.Settings;
using Dapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ADPv2.Models.Repositories
{
    public class RequestResponseLoggerRepository : BaseRepository, IRequestResponseLoggerRepository
    {
        public RequestResponseLoggerRepository(IOptions<ConnectionSettings> settings) : base(settings.Value.Connection) { }

        public async Task CreateRequestResponseLogger(RequestResponseLogModel request)
        {
            var payloadStr = JsonConvert.SerializeObject(request);

            var query = @"INSERT INTO [dbo].[tblRequestResponseLogger]
                            ([LogId]
                            ,[Node]
                            ,[ClientIp]
                            ,[TraceId]
                            ,[RequestDateTimeUtc]
                            ,[RequestDateTimeUtcActionLevel]
                            ,[RequestPath]
                            ,[RequestQuery]
                            ,[RequestQueries]
                            ,[RequestMethod]
                            ,[RequestScheme]
                            ,[RequestHost]
                            ,[RequestHeaders]
                            ,[RequestBody]
                            ,[RequestContentType]
                            ,[ResponseDateTimeUtc]
                            ,[ResponseDateTimeUtcActionLevel]
                            ,[ResponseStatus]
                            ,[ResponseHeaders]
                            ,[ResponseBody]
                            ,[ResponseContentType]
                            ,[IsExceptionActionLevel]
                            ,[ExceptionMessage]
                            ,[ExceptionStackTrace])
                        VALUES
                            (@LogId
                            ,@Node
                            ,@ClientIp
                            ,@TraceId
                            ,@RequestDateTimeUtc
                            ,@RequestDateTimeUtcActionLevel
                            ,@RequestPath
                            ,@RequestQuery
                            ,@RequestQueries
                            ,@RequestMethod
                            ,@RequestScheme
                            ,@RequestHost
                            ,@RequestHeaders
                            ,@RequestBody
                            ,@RequestContentType
                            ,@ResponseDateTimeUtc
                            ,@ResponseDateTimeUtcActionLevel
                            ,@ResponseStatus
                            ,@ResponseHeaders
                            ,@ResponseBody
                            ,@ResponseContentType
                            ,@IsExceptionActionLevel
                            ,@ExceptionMessage
                            ,@ExceptionStackTrace)";

            await _db.ExecuteAsync(query, request);
        }
    }
}
