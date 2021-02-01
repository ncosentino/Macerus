using System.Data;

namespace Macerus.Plugins.Features.Data.Api
{
    public static class IConnectionFactoryExtensions
    {
        public static IDbConnection OpenNew(this IConnectionFactory connectionFactory)
        {
            var connection = connectionFactory.Create();
            connection.Open();
            return connection;
        }
    }
}
