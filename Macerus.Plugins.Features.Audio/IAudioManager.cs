using System;
using System.Threading.Tasks;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Audio
{
    public interface IAudioManager
    {
        Task PlayMusicAsync(IIdentifier musicResourceId);

        Task PlaySoundEffectAsync(IIdentifier soundEffectResourceId);
    }
}
