using ProjectXyz.Data.Resources.Interface;

namespace Assets.Scripts.Behaviours.Data
{
    public interface IDataManagerBehaviour
    {
        IResourcesDataManager ResourcesDataManager { get; }
    }
}