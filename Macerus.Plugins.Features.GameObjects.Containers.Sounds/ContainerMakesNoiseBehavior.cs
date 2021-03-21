using Macerus.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Shared.Game.Behaviors;

namespace Macerus.Plugins.Features.GameObjects.Containers.Sounds
{
    public sealed class ContainerMakesNoiseBehavior : 
        BaseBehavior,
        IMakeNoiseBehaviour
    {
        private readonly ISoundGenerator _noiseGenerator;
        private readonly IFilterContextProvider _filterContextProvider;

        public ContainerMakesNoiseBehavior(
            IFilterContextProvider filterContextProvider)
        {
            _noiseGenerator = new SoundGenerator();
            _filterContextProvider = filterContextProvider;
        }

        public delegate ContainerMakesNoiseBehavior Factory();

        public float[] GetNoise()
        {
            var baseGeneratorContext = _filterContextProvider.GetContext();

            var filterContext = baseGeneratorContext;

            return _noiseGenerator.GenerateSound(filterContext);
        }
    }
}
