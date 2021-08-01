using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Macerus.Plugins.Features.Gui.Default
{
    public abstract class NotifierBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected async Task OnPropertyChangedAsync([CallerMemberName] string propertyName = null)
        {
            await PropertyChanged
                .InvokeOrderedAsync(this, new PropertyChangedEventArgs(propertyName))
                .ConfigureAwait(false);
        }
    }
}
