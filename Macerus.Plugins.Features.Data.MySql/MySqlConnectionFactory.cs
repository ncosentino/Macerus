using System.Data;

using Macerus.Plugins.Features.Data.Api;

using MySql.Data.MySqlClient;

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
