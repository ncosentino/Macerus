
using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Resources
{
    public static class IStringResourceProviderExtensions
    {
        public static string GetString(
            this IStringResourceProvider @this,
            IIdentifier stringResourceId)
        {
            var resource = @this.GetString(
                stringResourceId,
                @this.CurrentCulture);
            return resource;
        }
    }
}
