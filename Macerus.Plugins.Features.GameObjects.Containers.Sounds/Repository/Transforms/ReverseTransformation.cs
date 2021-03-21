using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Api;
using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Repository.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Repository.Transforms
{
    public sealed class ReverseTransformation : ISoundPatternTransformation
    {
        public IEnumerable<IWaveInstruction> Transform(IEnumerable<IWaveInstruction> instructions)
        {
            return instructions.Reverse();
        }
    }
}
