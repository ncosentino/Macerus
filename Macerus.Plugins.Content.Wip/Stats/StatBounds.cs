using ProjectXyz.Plugins.Features.BoundedStats.Api;

namespace Macerus.Plugins.Content.Wip.Stats
{
    public sealed class StatBounds : IStatBounds
    {
        public StatBounds(
            string minimumExpression,
            string maximumExpression)
        {
            MinimumExpression = minimumExpression;
            MaximumExpression = maximumExpression;
        }

        public string MinimumExpression { get; }

        public string MaximumExpression { get; }

        public static IStatBounds Max(string maximumExpression)
        {
            return new StatBounds(null, maximumExpression);
        }

        public static IStatBounds Min(string minimumExpression)
        {
            return new StatBounds(minimumExpression, null);
        }
    }
}