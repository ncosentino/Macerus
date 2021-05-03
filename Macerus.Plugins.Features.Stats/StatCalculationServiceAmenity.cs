using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api;

namespace Macerus.Plugins.Features.Stats
{
    public sealed class StatCalculationServiceAmenity : IStatCalculationServiceAmenity
    {
        private readonly IStatCalculationService _statCalculationService;
        private readonly Lazy<IStatCalculationContext> _lazyEmptyStatCalculationContext;

        public StatCalculationServiceAmenity(
            IStatCalculationService statCalculationService,
            IStatCalculationContextFactory statCalculationContextFactory)
        {
            _statCalculationService = statCalculationService;
            _lazyEmptyStatCalculationContext = new Lazy<IStatCalculationContext>(() =>
             {
                 var context = statCalculationContextFactory.Create(
                    new IComponent[] { },
                    new IGameObject[] { });
                 return context;
             });
        }

        private IStatCalculationContext EmptyStatCalculationContext => _lazyEmptyStatCalculationContext.Value;

        public double GetStatValue(
            IGameObject gameObject,
            IIdentifier statDefinitionId)
        {
            var value = GetStatValue(
                gameObject,
                statDefinitionId,
                EmptyStatCalculationContext);
            return value;
        }

        public double GetStatValue(
            IGameObject gameObject,
            IIdentifier statDefinitionId,
            IStatCalculationContext statCalculationContext)
        {
            var value = _statCalculationService.GetStatValue(
                gameObject,
                statDefinitionId,
                statCalculationContext);
            return value;
        }

        public IReadOnlyDictionary<IIdentifier, double> GetStatValues(
            IGameObject gameObject,
            IEnumerable<IIdentifier> statDefinitionIds)
        {
            var results = GetStatValuesAsync(
                gameObject,
                statDefinitionIds).Result;
            return results;
        }

        public async Task<IReadOnlyDictionary<IIdentifier, double>> GetStatValuesAsync(
            IGameObject gameObject,
            IEnumerable<IIdentifier> statDefinitionIds)
        {
            var tasks = new List<Task<Tuple<IIdentifier, double>>>();

            foreach (var statDefinitionId in statDefinitionIds)
            {
                var statTast = Task.Run(() =>
                {
                    var statResult = Tuple.Create(
                        statDefinitionId,
                        GetStatValue(
                            gameObject,
                            statDefinitionId));
                    return statResult;
                });
                tasks.Add(statTast);
            }

            await Task.WhenAll(tasks);

            var results = tasks.ToDictionary(
                x => x.Result.Item1,
                x => x.Result.Item2);
            return results;
        }
    }
}
