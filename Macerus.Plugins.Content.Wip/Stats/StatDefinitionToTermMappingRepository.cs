using System;
using System.Collections.Generic;
using System.Globalization;

using ProjectXyz.Api.Data.Databases;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Stats;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Wip.Stats
{

    public sealed class StatDefinitionToTermMappingRepository : IDiscoverableReadOnlyStatDefinitionToTermMappingRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly List<IStatDefinitionToTermMapping> _definitionCache;

        private bool _dirtyCache;

        public StatDefinitionToTermMappingRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            _definitionCache = new List<IStatDefinitionToTermMapping>();
            _dirtyCache = true;
        }        

        public IEnumerable<IStatDefinitionToTermMapping> GetStatDefinitionIdToTermMappings()
        {
            if (_dirtyCache)
            {
                _definitionCache.Clear();
                _definitionCache.AddRange(ReadAll());
                _dirtyCache = false;
            }

            return _definitionCache;
        }

        public IStatDefinitionToTermMapping GetStatDefinitionToTermMappingById(IIdentifier statDefinitionId)
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
                WHERE
                    id=@id
                ;";
                var numericId = Convert.ToInt32(
                    statDefinitionId,
                    CultureInfo.InvariantCulture);
                command.AddParameter("@id", numericId);

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    var term = reader.GetString(reader.GetOrdinal("term"));
                    var mapping = new StatDefinitionToTermMapping(
                        statDefinitionId,
                        term);
                    return mapping;
                }
            }
        }

        public IStatDefinitionToTermMapping GetStatDefinitionToTermMappingByTerm(string term)
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
                WHERE
                    term=@term
                ;";
                command.AddParameter("@term", term);

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    var id = reader.GetInt32(reader.GetOrdinal("id"));
                    var mapping = new StatDefinitionToTermMapping(
                        new IntIdentifier(id),
                        term);
                    return mapping;
                }
            }
        }

        public void WriteStatDefinitionIdToTermMappings(IEnumerable<IStatDefinitionToTermMapping> statDefinitionToTermMappings)
        {
            using (var connection = _connectionFactory.OpenNew())
            using (var transaction = connection.BeginTransaction())
            {
                foreach (var statDefinitionToTermMapping in statDefinitionToTermMappings)
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                        INSERT INTO
                            stat_definitions
                        SET
                            id=@id,
                            term=@term
                        ;";
                        var id = Convert.ToInt32(
                            statDefinitionToTermMapping.StatDefinitionId.ToString(),
                            CultureInfo.InvariantCulture);
                        command.AddParameter("@id", id);
                        command.AddParameter("@term", statDefinitionToTermMapping.Term);

                        var result = command.ExecuteNonQuery();
                        if (result != 1)
                        {
                            throw new InvalidOperationException(
                                $"The stat definition to term mapping could not be added. Result code {result}.");
                        }
                    }
                }

                transaction.Commit();
                _dirtyCache = true;
            }
        }

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
