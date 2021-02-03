using System.Data;

using MySql.Data.MySqlClient;

using ProjectXyz.Api.Data.Databases;

namespace Macerus.Plugins.Features.Data.MySql
{
    public sealed class MySqlConnectionFactory : IConnectionFactory
    {
        public IDbConnection Create()
        {
            var connection = new MySqlConnection(
                $"Server=localhost;" +
                $"Database=macerus;" +
                $"Uid=macerus;" +
                $"Pwd=macerus;");
            return connection;
        }
    }
}
