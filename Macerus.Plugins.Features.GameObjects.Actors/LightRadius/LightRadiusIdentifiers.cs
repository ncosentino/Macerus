using System;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Stats;

namespace Macerus.Plugins.Features.GameObjects.Actors.LightRadius
{
    public sealed class LightRadiusIdentifiers : ILightRadiusIdentifiers
    {
        private readonly Lazy<IIdentifier> _lazyRadiusIdentifier;
        private readonly Lazy<IIdentifier> _lazyIntensityIdentifier;
        private readonly Lazy<IIdentifier> _lazyRedIdentifier;
        private readonly Lazy<IIdentifier> _lazyGreenIdentifier;
        private readonly Lazy<IIdentifier> _lazyBlueIdentifier;

        public LightRadiusIdentifiers(IReadOnlyStatDefinitionToTermMappingRepositoryFacade statDefinitionToTermMappingRepository)
        {
            _lazyRadiusIdentifier = new Lazy<IIdentifier>(() => statDefinitionToTermMappingRepository
                .GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_RADIUS")
                .StatDefinitionId);
            _lazyIntensityIdentifier = new Lazy<IIdentifier>(() => statDefinitionToTermMappingRepository
                .GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_INTENSITY")
                .StatDefinitionId);
            _lazyRedIdentifier = new Lazy<IIdentifier>(() => statDefinitionToTermMappingRepository
                .GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_RED")
                .StatDefinitionId);
            _lazyGreenIdentifier = new Lazy<IIdentifier>(() => statDefinitionToTermMappingRepository
                .GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_GREEN")
                .StatDefinitionId);
            _lazyBlueIdentifier = new Lazy<IIdentifier>(() => statDefinitionToTermMappingRepository
                .GetStatDefinitionToTermMappingByTerm("LIGHT_RADIUS_BLUE")
                .StatDefinitionId);
        }

        public IIdentifier RadiusStatIdentifier => _lazyRadiusIdentifier.Value;

        public IIdentifier IntensityStatIdentifier => _lazyIntensityIdentifier.Value;

        public IIdentifier RedStatIdentifier => _lazyRedIdentifier.Value;

        public IIdentifier GreenStatIdentifier => _lazyGreenIdentifier.Value;

        public IIdentifier BlueStatIdentifier => _lazyBlueIdentifier.Value;
    }
}
