namespace Macerus.Api.Behaviors
{
    public interface IMovementBehavior : IReadOnlyMovementBehavior
    {
        new double ThrottleX { get; set; }

        new double ThrottleY { get; set; }

        new double RateOfDeceleration { get; set; }
    }
}