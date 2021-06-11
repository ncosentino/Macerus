using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Inventory.Api.HoverCards;

namespace Macerus.Plugins.Features.Inventory.Default.HoverCards
{
    public sealed class HoverCardPartViewConverterFacade : IHoverCardPartViewConverterFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableHoverCardPartViewConverter> _converters;

        public HoverCardPartViewConverterFacade(IEnumerable<IDiscoverableHoverCardPartViewConverter> converters)
        {
            _converters = converters.ToArray();
        }

        public object Create(IHoverCardPartViewModel viewModel)
        {
            var converter = _converters.FirstOrDefault(x => x.CanHandle(viewModel));
            if (converter == null)
            {
                throw new NotSupportedException(
                    $"No supported converter for '{viewModel}'.");
            }

            var view = converter.Create(viewModel);
            return view;
        }
    }
}
