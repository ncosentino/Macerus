using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;
using ProjectXyz.Shared.Framework;

namespace Macerus.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorIdentifiers : IActorIdentifiers
    {
        public IIdentifier FilterContextActorStatsIdentifier { get; } = new StringIdentifier("actor-stats");

        public IIdentifier ActorTypeIdentifier { get; } = new StringIdentifier("actor");

        public IIdentifier AnimationStandBack { get; } = new StringIdentifier("$actor$_stand_back");

        public IIdentifier AnimationStandForward { get; } = new StringIdentifier("$actor$_stand_forward");
        
        public IIdentifier AnimationStandLeft { get; } = new StringIdentifier("$actor$_stand_left");
        
        public IIdentifier AnimationStandRight { get; } = new StringIdentifier("$actor$_stand_right");

        public IIdentifier AnimationWalkBack { get; } = new StringIdentifier("$actor$_walk_back");

        public IIdentifier AnimationWalkForward { get; } = new StringIdentifier("$actor$_walk_forward");

        public IIdentifier AnimationWalkLeft { get; } = new StringIdentifier("$actor$_walk_left");

        public IIdentifier AnimationWalkRight { get; } = new StringIdentifier("$actor$_walk_right");
    }
}
