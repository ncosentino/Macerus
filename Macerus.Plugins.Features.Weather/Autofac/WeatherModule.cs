using Autofac;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Framework.Autofac;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.Weather;
using ProjectXyz.Plugins.Features.Weather.Api;
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
                    var weatherTables = new IWeatherTable[]
                    {
                        new WeatherTable(
                            new StringIdentifier("sample weather"),
                            new[]
                            {
                                new WeightedWeatherTableEntry(
                                    new StringIdentifier("rain"),
                                    1,
                                    new Interval<double>(10000),
                                    new Interval<double>(20000)),
                                new WeightedWeatherTableEntry(
                                    new StringIdentifier("clear"),
                                    3,
                                    new Interval<double>(10000),
                                    new Interval<double>(20000))
                            },
                            new IFilterAttribute[]
                            {
                                new FilterAttribute(
                                    new StringIdentifier("id"),
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
        }
    }
}
