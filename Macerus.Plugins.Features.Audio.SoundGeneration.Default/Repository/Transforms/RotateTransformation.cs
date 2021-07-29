using System;
using System.Collections.Generic;
using System.Linq;

using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine.Api;
using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Repository.Api;

namespace Macerus.Plugins.Features.Audio.SoundGeneration.Default.Repository.Transforms
{
    public sealed class RotateTransformation : ISoundPatternTransformation
    {
        public IEnumerable<IWaveInstruction> Transform(IEnumerable<IWaveInstruction> instructions)
        {
            var random = new Random();
            var original = instructions.ToArray();
            var rotationDistance = random.Next(0, original.Length);

            var queue = new Queue<IWaveInstruction>(instructions);
            var stack = new Stack<IWaveInstruction>();

            while (rotationDistance > 0)
            {
                stack.Push(queue.Dequeue());
                queue.Enqueue(stack.Pop());
                rotationDistance--;
            }

            return queue.ToArray();
        }
    }
}
