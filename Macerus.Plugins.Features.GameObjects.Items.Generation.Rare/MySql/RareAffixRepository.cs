using System.Collections.Generic;

using ProjectXyz.Api.Data.Databases;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Rare.MySql
{
    public sealed class RareAffixRepository : IRareAffixRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public RareAffixRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IEnumerable<string> GetAffixes(bool prefix)
        {
            using (var connection = _connectionFactory.OpenNew())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                SELECT
                    name
                FROM
                    rare_name_affixes
                WHERE
                    prefix=@prefix
                ;";
                command.AddParameter("@prefix", prefix);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var value = reader.GetString(0);
                        yield return value;
                    }
                }
            }
        }
    }
}
