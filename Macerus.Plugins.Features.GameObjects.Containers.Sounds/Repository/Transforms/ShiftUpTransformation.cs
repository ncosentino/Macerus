using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine;
using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Api;
using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Repository.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Repository.Transforms
{
    public sealed class ShiftUpTransformation : ISoundPatternTransformation
    {
        public IEnumerable<IWaveInstruction> Transform(IEnumerable<IWaveInstruction> instructions)
        {
            var random = new Random();
            var shiftAmount = random.Next(0x01, 0x05);

            return instructions.Select(x => new WaveInstructionTransform(
                x.Ops[0],
                x.Ops[1],
                x.Ops[2],
                x.Ops[3] + shiftAmount));
        }
    }
}
