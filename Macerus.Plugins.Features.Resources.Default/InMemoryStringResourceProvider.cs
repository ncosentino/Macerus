using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using NexusLabs.Collections.Generic;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Resources.Default
{
    public sealed class InMemoryStringResourceProvider : IStringResourceProvider
    {
        private readonly IFrozenDictionary<IIdentifier, string> _resources;

        public InMemoryStringResourceProvider(IEnumerable<KeyValuePair<IIdentifier, string>> resources)
        {
            _resources = resources.AsFrozenDictionary(
                byCulture => byCulture.Key,
                byCulture => byCulture.Value);
        }

        public CultureInfo CurrentCulture { get; } = CultureInfo.InvariantCulture;

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
                throw new KeyNotFoundException(
                    $"Could not find resource with ID '{stringResourceId}'.");
            }

            return resource;
        }
    }
}
