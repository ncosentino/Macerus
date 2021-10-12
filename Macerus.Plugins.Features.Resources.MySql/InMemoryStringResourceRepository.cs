using System.Globalization;

using ProjectXyz.Api.Data.Databases;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Resources.MySql
{
    public sealed class MySqlStringResourceRepository : IDiscoverableStringResourceRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public MySqlStringResourceRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public string GetString(
            IIdentifier stringResourceId,
            CultureInfo culture)
        {
            using (var connection = _connectionFactory.OpenNew())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                SELECT
                    resources_strings.value as value
                FROM
                    resources_strings
                    INNER JOIN
                        resource_cultures
                        ON
                        resource_cultures.id = resources_strings.culture_id
                WHERE
                    resources_strings.id = @resource_id
                    AND
                    resource_cultures.name = @culture_name
                ;";
                command.AddParameter("@resource_id", stringResourceId);
                command.AddParameter("@culture_name", string.IsNullOrEmpty(culture.Name) ? "en-US" : culture.Name);

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }
                    
                    var value = reader.GetString(0);
                    return value;
                }
            }
        }
    }
}
