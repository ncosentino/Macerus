using System;

using Macerus.Plugins.Features.Data.Api;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.GameObjects.Items.Generation.Magic.MySql
{
    public sealed class MagicAffixRepository : IMagicAffixRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public MagicAffixRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public string GetAffix(IIdentifier affixId)
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
