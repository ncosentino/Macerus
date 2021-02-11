namespace Macerus.Api.Behaviors
{
    public interface IWorldLocationBehavior : IObservableWorldLocationBehavior
    {
        new double X { get; set; }

        new double Y { get; set; }

        new double Width { get; set; }

        new double Height { get; set; }

        void SetLocation(double x, double y);

        void SetSize(double width, double height);
    }
}
