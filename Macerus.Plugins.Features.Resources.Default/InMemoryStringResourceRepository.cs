using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using NexusLabs.Collections.Generic;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Resources.Default
{
    public sealed class InMemoryStringResourceRepository : IDiscoverableStringResourceRepository
    {
        private readonly IFrozenDictionary<IIdentifier, string> _resources;

        public InMemoryStringResourceRepository(IEnumerable<KeyValuePair<IIdentifier, string>> resources)
        {
            _resources = resources.AsFrozenDictionary(
                byCulture => byCulture.Key,
                byCulture => byCulture.Value);
        }

        public string GetString(
            IIdentifier stringResourceId,
            CultureInfo culture)
        {
            if (culture != CultureInfo.InvariantCulture)
            {
                throw new NotSupportedException(
                    $"// FIXME: this implementation is just a hack to get some " +
                    $"resource functionality, but doesn't support culture switching.");
            }

            if (!_resources.TryGetValue(
                stringResourceId,
                out var resource))
            {
                return null;
            }

            return resource;
        }
    }
}
