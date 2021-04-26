using Macerus.Plugins.Features.StatusBar.Api;

namespace Macerus.Plugins.Features.StatusBar.Default
{
    public sealed class StatusBarResourceViewModel : IStatusBarResourceViewModel
    {
        public StatusBarResourceViewModel(
            string name,
            double current,
            double maximum)
        {
            Name = name;
            Current = current;
            Maximum = maximum;
        }

        public string Name { get; }

        public double Current { get; }

        public double Maximum { get; }
    }
}
