using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Api;
using System;
using System.Collections.Generic;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Repository.Api
{
    public interface ISoundPattern
    {
        Channel Channel { get; }

        IReadOnlyCollection<IWaveInstruction> Transforms { get; }
    }
}
