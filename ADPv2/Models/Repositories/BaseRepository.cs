using System.Data;
using System.Data.SqlClient;

namespace ADPv2.Models.Repositories
{
    public class BaseRepository
    {
        protected string _connectionString;
        protected IDbConnection _db;

        public BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
            _db = new SqlConnection(_connectionString);
            Dapper.SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);
        }
    }
}
