using System;
using System.Globalization;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Resources
{
    public interface IStringResourceProvider
    {
        CultureInfo CurrentCulture { get; }

        string GetString(
            IIdentifier stringResourceId,
            CultureInfo culture);
    }
}
