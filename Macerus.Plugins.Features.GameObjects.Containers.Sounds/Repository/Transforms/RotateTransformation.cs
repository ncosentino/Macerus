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
