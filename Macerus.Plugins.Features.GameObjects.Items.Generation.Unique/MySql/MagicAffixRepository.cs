using System;

using ProjectXyz.Api.Data.Databases;
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic.MySql
{
    public sealed class UniqueItemDefinition : IUniqueItemDefinition
    {
    }

    public interface IUniqueItemDefinition
    {
    }

    public sealed class UniqueItemDefinitionRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public UniqueItemDefinitionRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public string GetUniqueItemDefinitions(IIdentifier affixId)
        {
            using (var connection = _connectionFactory.OpenNew())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                SELECT
                    value
                FROM
                    magic_affixes
                WHERE
                    id=@id
                ;";
                command.AddParameter("@id", affixId.ToString());

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException(
                            $"Could not find magic affix with id '{affixId}'.");
                    }

                    var value = reader.GetString(reader.GetOrdinal("value"));
                    return value;
                }
            }
        }
    }
}
