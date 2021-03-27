using Macerus.Api.Behaviors.Filtering;
using Macerus.Plugins.Features.Encounters.SpawnTables.Api;

using ProjectXyz.Game.Interface.Engine;
using ProjectXyz.Plugins.Features.Mapping.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Headless
{
    class Program
    {
        public static void Main(string[] args)
        {
            var container = new MacerusContainer();
            var gameEngine = container.Resolve<IGameEngine>();

            var actorSpawner = container.Resolve<IActorSpawner>();
            var spawnTableIdentifiers = container.Resolve<ISpawnTableIdentifiers>();
            var mapGameObjectManager = container.Resolve<IMapGameObjectManager>();
            var filterContextAmenity = container.Resolve<IFilterContextAmenity>();
            var filterContext = filterContextAmenity.CreateFilterContextForSingle(
                filterContextAmenity.CreateRequiredAttribute(
                    spawnTableIdentifiers.FilterContextSpawnTableIdentifier,
                    new StringIdentifier("test-multi-skeleton")));
            filterContext = filterContextAmenity.CopyWithRange(filterContext, 100, 100);

            mapGameObjectManager.MarkForAddition(actorSpawner.SpawnActors(filterContext));

            while (true)
            {
                gameEngine.Update();
            }
        }
    }
}
