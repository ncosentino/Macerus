using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Stats
{
    public interface IStatResourceProvider
    {
        string GetStatName(IIdentifier statDefinitionId);
    }
}