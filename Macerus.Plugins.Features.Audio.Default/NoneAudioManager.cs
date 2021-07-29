using System;
using System.Threading.Tasks;

using ProjectXyz.Api.Framework;

namespace Macerus.Plugins.Features.Audio.Default
{
    public sealed class NoneAudioManager : IAudioManager
    {
        public async Task PlayMusicAsync(IIdentifier musicResourceId)
        {
        }

        public async Task PlaySoundEffectAsync(IIdentifier soundEffectResourceId)
        {
        }
    }
}
