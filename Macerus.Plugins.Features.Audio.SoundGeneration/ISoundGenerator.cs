using ProjectXyz.Plugins.Features.Filtering.Api;

namespace Macerus.Plugins.Features.Audio.SoundGeneration
{
    public interface ISoundGenerator
    {
        float[] GenerateSound(IFilterContext filterContext);
    }
}
