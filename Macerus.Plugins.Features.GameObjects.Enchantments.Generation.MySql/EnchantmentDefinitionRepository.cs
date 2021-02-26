using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Data.Databases;
using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Api.Enchantments.Generation;

namespace Macerus.Plugins.Features.GameObjects.Enchantments.Generation.MySql
{
    public sealed class EnchantmentDefinitionRepository : IDiscoverableReadOnlyEnchantmentDefinitionRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IDeserializer _deserializer;
        private readonly ISerializer _serializer;
        private readonly IAttributeFilterer _attributeFilterer;
        private readonly List<IEnchantmentDefinition> _definitionCache;

        private bool _dirtyCache;

        public EnchantmentDefinitionRepository(
            IConnectionFactory connectionFactory,
            IDeserializer deserializer,
            ISerializer serializer,
            IAttributeFilterer attributeFilterer)
        {
            _connectionFactory = connectionFactory;
            _deserializer = deserializer;
            _serializer = serializer;
            _attributeFilterer = attributeFilterer;
            _definitionCache = new List<IEnchantmentDefinition>();
            _dirtyCache = true;
        }

        public IEnumerable<IEnchantmentDefinition> ReadEnchantmentDefinitions(IFilterContext filterContext)
        {
            if (_dirtyCache)
            {
                _definitionCache.Clear();
                _definitionCache.AddRange(ReadAllEnchantmentDefinitions());
                _dirtyCache = false;
            }

            var enchantmentDefinitions = _definitionCache;
            var filteredEnchantmentDefinitions = _attributeFilterer.Filter(
                enchantmentDefinitions,
                filterContext);
            foreach (var filteredEnchantmentDefinition in filteredEnchantmentDefinitions)
            {
                // TODO: ensure we have all of the Enchantment generation components
                // NOTE: this includes:
                // - Fixed ones attached to the Enchantment definition
                // - Filter-applies ones that aren't attached to the Enchantment definition but can be applied by filter requirement being met   

                yield return filteredEnchantmentDefinition;
            }
        }

        public void WriteEnchantmentDefinitions(IEnumerable<IEnchantmentDefinition> enchantmentDefinitions)
        {
            using (var connection = _connectionFactory.OpenNew())
            using (var transaction = connection.BeginTransaction())
            {
                foreach (var enchantmentDefinition in enchantmentDefinitions)
                {
                    var serialized = _serializer.SerializeToString(
                        enchantmentDefinition,
                        Encoding.UTF8);
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                        INSERT INTO
                            enchantment_definitions
                        SET
                            serialized=@serialized
                        ;";
                        command.AddParameter("@serialized", serialized);

                        var result = command.ExecuteNonQuery();
                        if (result != 1)
                        {
                            throw new InvalidOperationException(
                                $"The enchantment definition could not be added. Result code {result}.");
                        }
                    }
                }

                transaction.Commit();
                _dirtyCache = true;
            }
        }

        private IEnumerable<IEnchantmentDefinition> ReadAllEnchantmentDefinitions()
        {
            using (var connection = _connectionFactory.OpenNew())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                SELECT
                    id,
                    serialized
                FROM
                    enchantment_definitions
                ;";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var raw = reader.GetString(reader.GetOrdinal("serialized"));
                        using (var dataStream = raw.ToStream(Encoding.UTF8))
                        {
                            var value = _deserializer.Deserialize<IEnchantmentDefinition>(dataStream);
                            yield return value;
                        }
                    }
                }
            }
        }
    }

    public static class StringExtensions
    {
        public static Stream ToStream(this string str, Encoding encoding)
        {
            var stream = new MemoryStream(encoding.GetBytes(str));
            stream.Position = 0;
            return stream;
        }
    }
}
