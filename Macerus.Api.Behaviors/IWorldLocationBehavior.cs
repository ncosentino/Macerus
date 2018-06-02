namespace Macerus.Api.Behaviors
{
    public interface IWorldLocationBehavior : IObservableWorldLocationBehavior
    {
        new double X { get; set; }

        new double Y { get; set; }
    }
}
