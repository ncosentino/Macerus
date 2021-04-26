namespace Macerus.Plugins.Features.StatusBar.Api
{
    public interface IStatusBarResourceViewModel
    {
        string Name { get; }

        double Current { get; }

        double Maximum { get; }
    }
}
