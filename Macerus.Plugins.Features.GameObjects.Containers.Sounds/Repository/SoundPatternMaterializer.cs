using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine;
using Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Repository
{
    public sealed class SoundPatternMaterializer
    {
        public IWaveInstruction Materialize(
            IWaveInstruction original,
            IWaveInstruction transform,
            IReadOnlyCollection<IOpSpecification> opSpecifications)
        {
            var transformedOps = original
                .Ops
                .Select((x, index) => TransformOp(x, transform.Ops[index], opSpecifications.ElementAt(index)))
                .ToArray();

            return new WaveInstruction(transformedOps);
        }

        private int TransformOp(int a, int b, IOpSpecification spec)
        {
            var result = a + b;

            if (result < spec.Min)
            {
                return spec.Max - Math.Abs(spec.Min - result);
            }

            if (result > spec.Max)
            {
                return spec.Min + Math.Abs(spec.Max - result);
            }

            return result;
        }
    }
}
