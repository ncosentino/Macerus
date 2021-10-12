using System;
using System.Globalization;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Resources
{
    public interface IStringResourceRepository
    {
        string GetString(
            IIdentifier stringResourceId,
            CultureInfo culture);
    }
}
