using Macerus.Plugins.Features.Gui.SceneTransitions;

namespace Macerus.Plugins.Features.Gui.Default.SceneTransitions
{
    public sealed class FaderSceneTransitionViewModel :
        NotifierBase,
        IFaderSceneTransitionViewModel
    {
        private double _opacity = 1;

        public double Opacity
        {
            get => _opacity;
            set
            {
                if (_opacity == value)
                {
                    return;
                }

                _opacity = value;
                OnPropertyChanged();
            }
        }
    }
}
