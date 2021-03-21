using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Api
{
    public interface IEngineSpecifications
    {
        Tuple<int, int> PitchRange { get; }

        Tuple<int, int> LengthRange { get; }

        Tuple<int, int> VolumeRange { get; }

    }
}
