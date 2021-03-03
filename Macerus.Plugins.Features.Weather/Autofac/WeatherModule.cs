using System.Collections.Generic;

using Autofac;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.BoundedStats;
using ProjectXyz.Plugins.Features.BoundedStats.Api;
using ProjectXyz.Plugins.Features.Weather;
using ProjectXyz.Plugins.Features.Weather.Api;
using ProjectXyz.Plugins.Stats;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.Weather.Autofac
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
                                    new Interval<double>(10000),
                                    new Interval<double>(20000)),
                                new WeightedWeatherTableEntry(
                                    WeatherIds.Clear,
                                    3,
                                    new Interval<double>(10000),
                                    new Interval<double>(20000))
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
                .RegisterType<WeatherIdentifiers>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<WeatherModifiers>()
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
            builder
                .RegisterType<WeatherAmenity>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
