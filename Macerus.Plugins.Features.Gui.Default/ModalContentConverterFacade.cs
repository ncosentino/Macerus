using System;
using System.Collections.Generic;
using System.Linq;

using NexusLabs.Contracts;

namespace Macerus.Plugins.Features.Gui.Default
{
    public sealed class ModalContentConverterFacade : IModalContentConverterFacade
    {
        private readonly Lazy<IReadOnlyCollection<IDiscoverableModalContentConverter>> _lazyConverters;

        public ModalContentConverterFacade(Lazy<IEnumerable<IDiscoverableModalContentConverter>> lazyConverters)
        {
            _lazyConverters = new Lazy<IReadOnlyCollection<IDiscoverableModalContentConverter>>(() =>
                lazyConverters.Value.ToArray());
        }

        public object ConvertContentToWeldableView(object content)
        {
            var converter = _lazyConverters
                .Value
                .FirstOrDefault(x => x.CanConvert(content));
            Contract.RequiresNotNull(
                converter,
                $"No converter found for content type '{content?.GetType()}'.");
            var converted = converter.ConvertContentToWeldableView(content);
            return converted;
        }
    }
}
