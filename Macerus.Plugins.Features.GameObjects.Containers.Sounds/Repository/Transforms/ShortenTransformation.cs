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
    public sealed class ShortenTransformation : ISoundPatternTransformation
    {
        public IEnumerable<IWaveInstruction> Transform(IEnumerable<IWaveInstruction> instructions)
        {
            var original = instructions.ToList();
            if (original.Count < 3)
            {
                return original;
            }

            var random = new Random();
            var index = random.Next(1, original.Count);


            original.RemoveAt(index);

            return original;
        }
    }
}
