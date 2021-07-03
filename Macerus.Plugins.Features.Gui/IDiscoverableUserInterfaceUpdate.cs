namespace Macerus.Plugins.Features.Gui
{
    public interface IDiscoverableUserInterfaceUpdate : IUserInterfaceUpdate
    {
        double UpdateIntervalInSeconds { get; }
    }
}
