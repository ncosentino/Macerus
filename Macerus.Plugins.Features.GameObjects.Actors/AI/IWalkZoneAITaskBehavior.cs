namespace Macerus.Plugins.Features.GameObjects.Actors.Default.AI
{
    public interface IWalkZoneAITaskBehavior : IAITaskBehavior
    {
        double X { get; }

        double Y { get; }

        double Width { get; }

        double Height { get; }
    }
}
