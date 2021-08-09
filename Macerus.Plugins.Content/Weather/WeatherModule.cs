using System.Collections.Generic;

using Autofac;

using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.Stats.Bounded;
using ProjectXyz.Plugins.Features.Stats.Bounded.Default;
using ProjectXyz.Plugins.Features.Stats.Default;
using ProjectXyz.Plugins.Features.Weather;
using ProjectXyz.Plugins.Features.Weather.Default;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Content.Weather
{
    public sealed class WeatherModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var weatherIdentifiers = c.Resolve<IWeatherIdentifiers>();
                    var weatherTables = new IWeatherTable[]
                    {
                        new WeatherTable(
                            new StringIdentifier("sample weather"),
                            new[]
                            {
                                new WeightedWeatherTableEntry(
                                    WeatherIds.Rain,
                                    1,
                                    10,
                                    20),
                                new WeightedWeatherTableEntry(
                                    WeatherIds.Clear,
                                    3,
                                    10,
                                    20)
                            },
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    weatherIdentifiers.WeatherIdentifier,
                                    new IdentifierFilterAttributeValue(new StringIdentifier("sample weather")),
                                    false),
                            })
                    };
                    var weatherTableRepository = new InMemoryWeatherTableRepository(
                        weatherTables,
                        c.Resolve<IAttributeFilterer>());
                    return weatherTableRepository;
                })
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<WeatherStateIdToTermRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(c =>
                {
                    var mapping = new Dictionary<IIdentifier, string>();
                    foreach (var weatherId in WeatherIds.All)
                    {
                        mapping.Add(
                            new StringIdentifier($"{weatherId}-duration-maximum"),
                            $"{weatherId.ToString().ToUpperInvariant()}_DURATION_MAXIMUM");
                        mapping.Add(
                            new StringIdentifier($"{weatherId}-duration-minimum"),
                            $"{weatherId.ToString().ToUpperInvariant()}_DURATION_MINIMUM");
                        mapping.Add(
                            new StringIdentifier($"{weatherId}-weight"),
                            $"{weatherId.ToString().ToUpperInvariant()}_WEIGHT");
                    }

                    return new InMemoryStatDefinitionToTermMappingRepository(mapping);
                })
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(c =>
                {
                    var mapping = new Dictionary<IIdentifier, IStatBounds>();
                    foreach (var weatherId in WeatherIds.All)
                    {
                        mapping.Add(
                            new StringIdentifier($"{weatherId}-duration-maximum"),
                            new StatBounds("0", null));
                        mapping.Add(
                            new StringIdentifier($"{weatherId}-duration-minimum"),
                            new StatBounds("0", $"{weatherId.ToString().ToUpperInvariant()}_DURATION_MAXIMUM"));
                    }

                    return new InMemoryStatDefinitionIdToBoundsMappingRepository(mapping);
                })
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
