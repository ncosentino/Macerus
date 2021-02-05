using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Data.Databases;
using ProjectXyz.Api.Stats;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Wip.Stats
{
    public sealed class StatDefinitionToTermMappingRepository : IStatDefinitionToTermMappingRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly Lazy<IReadOnlyCollection<IStatDefinitionToTermMapping>> _lazyCache;

        public StatDefinitionToTermMappingRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            _lazyCache = new Lazy<IReadOnlyCollection<IStatDefinitionToTermMapping>>(() =>
                ReadAll().ToArray());
        }        

        public IEnumerable<IStatDefinitionToTermMapping> GetStatDefinitionIdToTermMappings() => _lazyCache.Value;

        private IEnumerable<IStatDefinitionToTermMapping> ReadAll()
        {
            using (var connection = _connectionFactory.OpenNew())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                SELECT
                    id,
                    term
                FROM
                    stat_definitions
                ;";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt32(reader.GetOrdinal("id"));
                        var term = reader.GetString(reader.GetOrdinal("term"));
                        var mapping = new StatDefinitionToTermMapping(
                            new IntIdentifier(id),
                            term);
                        yield return mapping;
                    }
                }
            }
        }
    }
}
