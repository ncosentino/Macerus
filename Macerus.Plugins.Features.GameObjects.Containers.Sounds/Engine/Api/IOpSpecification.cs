namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds.Engine.Api
{
    public interface IOpSpecification
    {
        string Name { get; }

        string Description { get; }

        int Min { get; }

        int Max { get; }
    }
}
