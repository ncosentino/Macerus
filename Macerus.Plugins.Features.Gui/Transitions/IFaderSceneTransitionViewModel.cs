using System.ComponentModel;

namespace Macerus.Plugins.Features.Gui.SceneTransitions
{
    public interface IFaderSceneTransitionViewModel : INotifyPropertyChanged
    {
        double Opacity { get; set; }
    }
}
