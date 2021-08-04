namespace Macerus.Plugins.Features.Gui
{
    public interface IDiscoverableModalContentConverter : IModalContentConverter
    {
        bool CanConvert(object content);
    }
}
