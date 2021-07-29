namespace Macerus.Plugins.Features.Audio.SoundGeneration.Default.Engine.Api
{
    public interface IOpSpecification
    {
        string Name { get; }

        string Description { get; }

        int Min { get; }

        int Max { get; }
    }
}
