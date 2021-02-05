using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Data.Databases;
using ProjectXyz.Plugins.Features.BoundedStats.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Wip.Stats
{
    public sealed class StatDefinitionIdToBoundsMappingRepository : IStatDefinitionIdToBoundsMappingRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly Lazy<IReadOnlyCollection<IStatDefinitionIdToBoundsMapping>> _lazyCache;

        public StatDefinitionIdToBoundsMappingRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            _lazyCache = new Lazy<IReadOnlyCollection<IStatDefinitionIdToBoundsMapping>>(() =>
                ReadAll().ToArray());
        }

        public IEnumerable<IStatDefinitionIdToBoundsMapping> GetStatDefinitionIdToBoundsMappings() => _lazyCache.Value;

        private IEnumerable<IStatDefinitionIdToBoundsMapping> ReadAll()
        {
            using (var connection = _connectionFactory.OpenNew())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                SELECT
                    id,
                    stat_definition_id,
                    minimum_expression,
                    maximum_expression
                FROM
                    stat_definition_bounds
                ;";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt32(reader.GetOrdinal("id"));
                        var statDefinitionId = reader.GetInt32(reader.GetOrdinal("stat_definition_id"));
                        var minimumExpression = reader.GetString(reader.GetOrdinal("minimum_expression"));
                        var maximumExpression = reader.GetString(reader.GetOrdinal("maximum_expression"));
                        var mapping = new StatDefinitionIdToBoundsMapping(
                            new IntIdentifier(statDefinitionId),
                            new StatBounds(
                                minimumExpression,
                                maximumExpression));
                        yield return mapping;
                    }
                }
            }
        }
    }
}