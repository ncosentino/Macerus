namespace Macerus.Plugins.Features.Gui.Api
{
    public interface IDiscoverableUserInterfaceUpdate : IUserInterfaceUpdate
    {
        double UpdateIntervalInSeconds { get; }
    }
}
