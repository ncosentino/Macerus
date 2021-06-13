using System.ComponentModel;

namespace Macerus.Plugins.Features.Gui.Api.SceneTransitions
{
    public interface IFaderSceneTransitionViewModel : INotifyPropertyChanged
    {
        double Opacity { get; set; }
    }
}
