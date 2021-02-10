namespace Macerus.Api.Behaviors
{
    public interface IMovementBehavior : IObservableMovementBehavior
    {
        new double ThrottleX { get; set; }

        new double ThrottleY { get; set; }

        void SetThrottle(double throttleX, double throttleY);

        new double VelocityX { get; set; }

        new double VelocityY { get; set; }

        void SetVelocity(double velocityX, double velocityY);
    }
}