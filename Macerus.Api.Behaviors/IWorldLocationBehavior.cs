namespace Macerus.Api.Behaviors
{
    public interface IWorldLocationBehavior : IReadOnlyWorldLocationBehavior
    {
        new double X { get; set; }

        new double Y { get; set; }
    }
}
