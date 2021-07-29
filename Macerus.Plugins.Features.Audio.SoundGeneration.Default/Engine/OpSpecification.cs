using Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine.Api;

namespace Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine
{
    public sealed class OpSpecification : IOpSpecification
    {
        public OpSpecification(
            string name,
            string description,
            int min,
            int max)
        {
            Name = name;
            Description = description;
            Min = min;
            Max = max;
        }

        public string Name { get; }

        public string Description { get; }

        public int Min { get; }

        public int Max { get; }
    }
}
