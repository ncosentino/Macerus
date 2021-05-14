namespace Macerus.Plugins.Features.Mapping.Default
{
    public interface IMapResourceIdConverter
    {
        string ConvertToMapResourcePath(string mapResourceId);

        string ConvertToGameObjectResourcePath(string mapResourceId);
    }
}