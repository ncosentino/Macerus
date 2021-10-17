
using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework;

namespace Macerus.Content.TimeOfDay
{
    public static class TimesOfDay
    {
        public static IIdentifier Dawn { get; } = new StringIdentifier("dawn");

        public static IIdentifier Day { get; } = new StringIdentifier("day");

        public static IIdentifier Dusk { get; } = new StringIdentifier("dusk");

        public static IIdentifier Night { get; } = new StringIdentifier("night");
    }
}
