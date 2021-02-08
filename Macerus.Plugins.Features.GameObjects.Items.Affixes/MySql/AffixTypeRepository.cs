using System;
using System.Collections.Generic;
using System.Globalization;

using Macerus.Plugins.Features.GameObjects.Items.Affixes.Api;

using ProjectXyz.Api.Data.Databases;
using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Affixes.MySql
{
    public sealed partial class AffixTypeRepository : IAffixTypeRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly List<IAffixType> _definitionCache;

        private bool _dirtyCache;

        public AffixTypeRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            _definitionCache = new List<IAffixType>();
            _dirtyCache = true;
        }

        public IEnumerable<IAffixType> GetAllAffixTypes()
        {
            if (_dirtyCache)
            {
                _definitionCache.Clear();
                _definitionCache.AddRange(ReadAll());
                _dirtyCache = false;
            }

            return _definitionCache;
        }

        public IAffixType GetAffixTypeById(IIdentifier affixId)
        {
            using (var connection = _connectionFactory.OpenNew())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                SELECT
                    id,
                    name
                FROM
                    affix_types
                WHERE
                    id=@id
                ;";
                var numericId = Convert.ToInt32(
                    affixId,
                    CultureInfo.InvariantCulture);
                command.AddParameter("@id", numericId);

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    var term = reader.GetString(reader.GetOrdinal("term"));
                    var affixType = new AffixType(
                        affixId,
                        term);
                    return affixType;
                }
            }
        }

        public IAffixType GetAffixTypeByName(string name)
        {
            using (var connection = _connectionFactory.OpenNew())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                SELECT
                    id,
                    name
                FROM
                    affix_types
                WHERE
                    name=@name
                ;";
                command.AddParameter("@name", name);

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    var id = reader.GetInt32(reader.GetOrdinal("id"));
                    var affixType = new AffixType(
                        new IntIdentifier(id),
                        name);
                    return affixType;
                }
            }
        }

        public void WriteAffixTypes(IEnumerable<IAffixType> affixTypes)
        {
            using (var connection = _connectionFactory.OpenNew())
            using (var transaction = connection.BeginTransaction())
            {
                foreach (var affixType in affixTypes)
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                        INSERT INTO
                            affix_types
                        SET
                            id=@id,
                            name=@name
                        ;";
                        var id = Convert.ToInt32(
                            affixType.Id.ToString(),
                            CultureInfo.InvariantCulture);
                        command.AddParameter("@id", id);
                        command.AddParameter("@name", affixType.Name);

                        var result = command.ExecuteNonQuery();
                        if (result != 1)
                        {
                            throw new InvalidOperationException(
                                $"The affix type could not be added. Result code {result}.");
                        }
                    }
                }

                transaction.Commit();
                _dirtyCache = true;
            }
        }

        private IEnumerable<IAffixType> ReadAll()
        {
            using (var connection = _connectionFactory.OpenNew())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                SELECT
                    id,
                    name
                FROM
                    affix_types
                ;";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt32(reader.GetOrdinal("id"));
                        var term = reader.GetString(reader.GetOrdinal("name"));
                        var mapping = new AffixType(
                            new IntIdentifier(id),
                            term);
                        yield return mapping;
                    }
                }
            }
        }
    }
}
